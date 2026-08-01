#if !(UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX || STEAMWORKS_WIN || STEAMWORKS_LIN_OSX)
#define DISABLESTEAMWORKS
#endif

using UnityEngine;
#if !DISABLESTEAMWORKS
using Steamworks;
#endif

[DisallowMultipleComponent]
public class SteamLeaderboardManager : MonoBehaviour
{
	public static SteamLeaderboardManager Instance { get; private set; }

	private const string LeaderboardApiName = "AverageScore";

#if !DISABLESTEAMWORKS
	private SteamLeaderboard_t _leaderboard;
	private bool _leaderboardReady = false;
	private bool _pendingUpload = false;

	private CallResult<LeaderboardFindResult_t> _findLeaderboardCallResult;
	private CallResult<LeaderboardScoreUploaded_t> _uploadScoreCallResult;
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

		SteamAPICall_t handle = SteamUserStats.UploadLeaderboardScore(_leaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest, avg, null, 0);
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
}
