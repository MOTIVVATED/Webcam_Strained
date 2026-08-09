#if !(UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX || STEAMWORKS_WIN || STEAMWORKS_LIN_OSX)
#define DISABLESTEAMWORKS
#endif

using System;
using System.Collections.Generic;
using UnityEngine;
#if !DISABLESTEAMWORKS
using System.Collections;
using Steamworks;
#endif

public class LeaderboardEntryData
{
	public int rank;
	public int score;
	public string playerName;
	public bool isLocalPlayer;
}

[DisallowMultipleComponent]
public class SteamLeaderboardManager : MonoBehaviour
{
	public static SteamLeaderboardManager Instance { get; private set; }

	public event Action<List<LeaderboardEntryData>> OnTopEntriesReady;
	public event Action<LeaderboardEntryData> OnOwnEntryReady;

	private const string LeaderboardApiName = "AverageScore";

#if !DISABLESTEAMWORKS
	private SteamLeaderboard_t _leaderboard;
	private bool _leaderboardReady = false;
	private bool _pendingUpload = false;

	private CallResult<LeaderboardFindResult_t> _findLeaderboardCallResult;
	private CallResult<LeaderboardScoreUploaded_t> _uploadScoreCallResult;

	private CallResult<LeaderboardScoresDownloaded_t> _downloadTopCallResult;
	private CallResult<LeaderboardScoresDownloaded_t> _downloadOwnCallResult;
	private HashSet<CSteamID> _pendingNames = new HashSet<CSteamID>();
	private Dictionary<CSteamID, List<LeaderboardEntryData>> _pendingNameEntries = new Dictionary<CSteamID, List<LeaderboardEntryData>>();
	private List<LeaderboardEntryData> _pendingTopEntries;
	private LeaderboardEntryData _pendingOwnEntry;
	private bool _topDownloadComplete;
	private bool _ownDownloadComplete;
	private Callback<PersonaStateChange_t> _personaStateChangeCallback;
	private Coroutine _nameTimeoutCoroutine;
	private const float NameTimeoutSeconds = 5f;
#endif

	private void Awake()
	{
		if (Instance != null && Instance != this) { Destroy(gameObject); return; }

		Instance = this;

		transform.SetParent(null);

		DontDestroyOnLoad(gameObject);
	}

#if !DISABLESTEAMWORKS
	private void Start()
	{
		if (!SteamManager.Initialized)
		{
			Debug.LogWarning("SteamLeaderboardManager: Steam is not initialized, cannot find leaderboard.");
			return;
		}

		_findLeaderboardCallResult = CallResult<LeaderboardFindResult_t>.Create(OnFindLeaderboard);
		SteamAPICall_t handle = SteamUserStats.FindOrCreateLeaderboard(LeaderboardApiName, ELeaderboardSortMethod.k_ELeaderboardSortMethodDescending, ELeaderboardDisplayType.k_ELeaderboardDisplayTypeNumeric);
		_findLeaderboardCallResult.Set(handle);
	}

	private void OnFindLeaderboard(LeaderboardFindResult_t result, bool ioFailure)
	{
		if (ioFailure || result.m_bLeaderboardFound == 0)
		{
			Debug.LogWarning($"SteamLeaderboardManager: Failed to find or create leaderboard '{LeaderboardApiName}'.");
			return;
		}

		_leaderboard = result.m_hSteamLeaderboard;
		_leaderboardReady = true;
		Debug.Log($"SteamLeaderboardManager: Leaderboard '{LeaderboardApiName}' ready.");

		if (_pendingUpload)
		{
			_pendingUpload = false;
			UploadAverageScore();
		}
	}
#endif

	private void OnEnable()
	{
		GameEvents.OnProfileUpdated += HandleProfileUpdated;
	}

	private void OnDisable()
	{
		GameEvents.OnProfileUpdated -= HandleProfileUpdated;
	}

	private void HandleProfileUpdated()
	{
		if (PlayerProfileManager.Instance == null)
			return;

#if !DISABLESTEAMWORKS
		if (_leaderboardReady)
			UploadAverageScore();
		else
			_pendingUpload = true;
#endif
	}

#if !DISABLESTEAMWORKS
	private void UploadAverageScore()
	{
		if (!SteamManager.Initialized)
		{
			Debug.LogWarning("SteamLeaderboardManager: Steam is not initialized, cannot upload score.");
			return;
		}

		int avg = PlayerProfileManager.Instance.GetProfile().GetAverageScore();

		if (_uploadScoreCallResult == null)
			_uploadScoreCallResult = CallResult<LeaderboardScoreUploaded_t>.Create(OnUploadScore);

		SteamAPICall_t handle = SteamUserStats.UploadLeaderboardScore(_leaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate, avg, null, 0);
		_uploadScoreCallResult.Set(handle);
	}

	private void OnUploadScore(LeaderboardScoreUploaded_t result, bool ioFailure)
	{
		if (ioFailure || result.m_bSuccess == 0)
		{
			Debug.LogWarning("SteamLeaderboardManager: Failed to upload leaderboard score.");
			return;
		}

		Debug.Log($"SteamLeaderboardManager: Uploaded score={result.m_nScore}, scoreChanged={result.m_bScoreChanged != 0}, newRank={result.m_nGlobalRankNew}.");
	}

#endif

	public void RequestTopAndOwnEntries()
	{
#if !DISABLESTEAMWORKS
		if (!SteamManager.Initialized || !_leaderboardReady)
		{
			Debug.LogWarning("SteamLeaderboardManager: Steam is not initialized or leaderboard is not ready, cannot request entries.");
			return;
		}

		_pendingNames.Clear();
		_pendingNameEntries.Clear();
		_pendingTopEntries = new List<LeaderboardEntryData>();
		_pendingOwnEntry = null;
		_topDownloadComplete = false;
		_ownDownloadComplete = false;

		if (_personaStateChangeCallback == null)
			_personaStateChangeCallback = Callback<PersonaStateChange_t>.Create(OnPersonaStateChange);

		if (_nameTimeoutCoroutine != null)
			StopCoroutine(_nameTimeoutCoroutine);
		_nameTimeoutCoroutine = StartCoroutine(NameTimeoutRoutine());

		if (_downloadTopCallResult == null)
			_downloadTopCallResult = CallResult<LeaderboardScoresDownloaded_t>.Create(OnDownloadTop);
		SteamAPICall_t topHandle = SteamUserStats.DownloadLeaderboardEntries(_leaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 1, 100);
		_downloadTopCallResult.Set(topHandle);

		if (_downloadOwnCallResult == null)
			_downloadOwnCallResult = CallResult<LeaderboardScoresDownloaded_t>.Create(OnDownloadOwn);
		SteamAPICall_t ownHandle = SteamUserStats.DownloadLeaderboardEntries(_leaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser, 0, 0);
		_downloadOwnCallResult.Set(ownHandle);
#else
		Debug.LogWarning("SteamLeaderboardManager: Steamworks disabled on this platform.");
#endif
	}

#if !DISABLESTEAMWORKS
	private LeaderboardEntryData ResolveEntry(LeaderboardEntry_t entry, bool isLocal)
	{
		string name = SteamFriends.GetFriendPersonaName(entry.m_steamIDUser);
		LeaderboardEntryData data = new LeaderboardEntryData { rank = entry.m_nGlobalRank, score = entry.m_nScore, playerName = name, isLocalPlayer = isLocal };

		if (name == "[unknown]")
		{
			_pendingNames.Add(entry.m_steamIDUser);
			RegisterPendingName(entry.m_steamIDUser, data);
			SteamFriends.RequestUserInformation(entry.m_steamIDUser, true);
		}

		return data;
	}

	private void RegisterPendingName(CSteamID id, LeaderboardEntryData data)
	{
		if (!_pendingNameEntries.TryGetValue(id, out List<LeaderboardEntryData> list))
		{
			list = new List<LeaderboardEntryData>();
			_pendingNameEntries[id] = list;
		}

		list.Add(data);
	}

	private void OnDownloadTop(LeaderboardScoresDownloaded_t result, bool ioFailure)
	{
		if (ioFailure)
		{
			Debug.LogWarning("SteamLeaderboardManager: Failed to download top leaderboard entries.");
			_topDownloadComplete = true;
			TryFinalize();
			return;
		}

		for (int i = 0; i < result.m_cEntryCount; i++)
		{
			SteamUserStats.GetDownloadedLeaderboardEntry(result.m_hSteamLeaderboardEntries, i, out LeaderboardEntry_t entry, null, 0);
			_pendingTopEntries.Add(ResolveEntry(entry, false));
		}

		_topDownloadComplete = true;
		TryFinalize();
	}

	private void OnDownloadOwn(LeaderboardScoresDownloaded_t result, bool ioFailure)
	{
		if (ioFailure)
		{
			Debug.LogWarning("SteamLeaderboardManager: Failed to download own leaderboard entry.");
			_ownDownloadComplete = true;
			TryFinalize();
			return;
		}

		if (result.m_cEntryCount > 0)
		{
			SteamUserStats.GetDownloadedLeaderboardEntry(result.m_hSteamLeaderboardEntries, 0, out LeaderboardEntry_t entry, null, 0);
			_pendingOwnEntry = ResolveEntry(entry, true);
		}

		_ownDownloadComplete = true;
		TryFinalize();
	}

	private void OnPersonaStateChange(PersonaStateChange_t result)
	{
		CSteamID id = new CSteamID(result.m_ulSteamID);

		if (_pendingNames.Remove(id))
		{
			RefreshNameFor(id);
			TryFinalize();
		}
	}

	private void RefreshNameFor(CSteamID id)
	{
		string name = SteamFriends.GetFriendPersonaName(id);

		if (_pendingNameEntries.TryGetValue(id, out List<LeaderboardEntryData> list))
		{
			foreach (LeaderboardEntryData entryData in list)
				entryData.playerName = name;

			_pendingNameEntries.Remove(id);
		}
	}

	private void TryFinalize()
	{
		if (!_topDownloadComplete || !_ownDownloadComplete || _pendingNames.Count > 0)
			return;

		if (_nameTimeoutCoroutine != null)
		{
			StopCoroutine(_nameTimeoutCoroutine);
			_nameTimeoutCoroutine = null;
		}

		OnTopEntriesReady?.Invoke(_pendingTopEntries);

		if (_pendingOwnEntry != null)
			OnOwnEntryReady?.Invoke(_pendingOwnEntry);
	}

	private IEnumerator NameTimeoutRoutine()
	{
		yield return new WaitForSeconds(NameTimeoutSeconds);

		if (_pendingNames.Count > 0)
		{
			foreach (LeaderboardEntryData entryData in _pendingTopEntries)
			{
				if (entryData.playerName == "[unknown]")
					entryData.playerName = "Игрок";
			}

			if (_pendingOwnEntry != null && _pendingOwnEntry.playerName == "[unknown]")
				_pendingOwnEntry.playerName = "Игрок";

			_pendingNames.Clear();
			_pendingNameEntries.Clear();
		}

		_nameTimeoutCoroutine = null;
		TryFinalize();
	}
#endif
}
