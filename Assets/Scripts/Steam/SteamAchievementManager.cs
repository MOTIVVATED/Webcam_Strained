#if !(UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX || STEAMWORKS_WIN || STEAMWORKS_LIN_OSX)
#define DISABLESTEAMWORKS
#endif

using System.Collections.Generic;
using UnityEngine;
#if !DISABLESTEAMWORKS
using Steamworks;
#endif

[DisallowMultipleComponent]
public class SteamAchievementManager : MonoBehaviour
{
	public static SteamAchievementManager Instance { get; private set; }

	private const string MafuApiName = "MAFU_COMPLETED";
	private const string ApexApiName = "APEX_COMPLETED";
	private const string PampyApiName = "PAMPY_COMPLETED";
	private const string EnterApiName = "ENTER_COMPLETED";
	private const string SmellyApiName = "SMELLY_COMPLETED";
	private const string MadiApiName = "MADI_COMPLETED";
	private const string BestOperatorApiName = "BEST_OPERATOR";
	private const string WebcamStrainedApiName = "WEBCAM_STRAINED";

	private const int MaxRankNumber = 6;

	private static readonly Dictionary<string, string> SceneAchievementApiNames = new Dictionary<string, string>
	{
		{ "MafuLegenda", MafuApiName },
		{ "ApexFunk", ApexApiName },
		{ "PampyBam", PampyApiName },
		{ "EnterYou", EnterApiName },
		{ "SmellySam", SmellyApiName },
		{ "MadiMeows", MadiApiName }
	};

	// Остальные 7 достижений, от которых зависит WEBCAM_STRAINED.
	private static readonly string[] OtherAchievementApiNames =
	{
		MafuApiName, ApexApiName, PampyApiName, EnterApiName, SmellyApiName, MadiApiName, BestOperatorApiName
	};

	// Все 8 достижений, для debug-логирования статуса.
	private static readonly string[] AllAchievementApiNames =
	{
		MafuApiName, ApexApiName, PampyApiName, EnterApiName, SmellyApiName, MadiApiName, BestOperatorApiName, WebcamStrainedApiName
	};

	private readonly HashSet<string> _unlockedCache = new HashSet<string>();

	private void Awake()
	{
		if (Instance != null && Instance != this) { Destroy(gameObject); return; }

		Instance = this;

		transform.SetParent(null);

		DontDestroyOnLoad(gameObject);
	}

#if UNITY_EDITOR
	private void Start()
	{
		LogAchievementStatuses();
	}

	private void LogAchievementStatuses()
	{
		if (!SteamManager.Initialized)
		{
			Debug.LogWarning("SteamAchievementManager: Steam is not initialized, cannot read achievement statuses.");
			return;
		}

#if !DISABLESTEAMWORKS
		foreach (string apiName in AllAchievementApiNames)
		{
			if (SteamUserStats.GetAchievement(apiName, out bool achieved))
				Debug.Log($"SteamAchievementManager: '{apiName}' achieved={achieved}");
			else
				Debug.LogWarning($"SteamAchievementManager: Could not read status for '{apiName}'.");
		}
#endif
	}

	[ContextMenu("DEBUG: Reset All Steam Stats And Achievements")]
	private void ResetAllStatsAndAchievements()
	{
		if (!SteamManager.Initialized)
		{
			Debug.LogWarning("SteamAchievementManager: Steam is not initialized, cannot reset stats.");
			return;
		}

#if !DISABLESTEAMWORKS
		if (SteamUserStats.ResetAllStats(true))
		{
			SteamUserStats.StoreStats();
			_unlockedCache.Clear();
			Debug.Log("SteamAchievementManager: All Steam stats and achievements have been reset.");
		}
		else
		{
			Debug.LogWarning("SteamAchievementManager: SteamUserStats.ResetAllStats failed.");
		}
#endif
	}
#endif

	private void OnEnable()
	{
		GameEvents.OnLevelCompleted += HandleLevelCompleted;
		GameEvents.OnProfileUpdated += HandleProfileUpdated;
	}

	private void OnDisable()
	{
		GameEvents.OnLevelCompleted -= HandleLevelCompleted;
		GameEvents.OnProfileUpdated -= HandleProfileUpdated;
	}

	private void HandleLevelCompleted(string sceneName, bool isFirstCompletion)
	{
		if (!isFirstCompletion)
			return;

		if (SceneAchievementApiNames.TryGetValue(sceneName, out string apiName))
			UnlockAchievement(apiName);
	}

	private void HandleProfileUpdated()
	{
		if (PlayerProfileManager.Instance == null)
			return;

		if (PlayerProfileManager.Instance.GetProfile().GetRankNumber() >= MaxRankNumber)
			UnlockAchievement(BestOperatorApiName);
	}

	public void UnlockAchievement(string apiName)
	{
		if (!SteamManager.Initialized)
		{
			Debug.LogWarning($"SteamAchievementManager: Steam is not initialized, skipping unlock of '{apiName}'.");
			return;
		}

		if (IsAchievementUnlocked(apiName))
			return;

#if !DISABLESTEAMWORKS
		if (SteamUserStats.SetAchievement(apiName))
		{
			SteamUserStats.StoreStats();
			_unlockedCache.Add(apiName);
			Debug.Log($"SteamAchievementManager: Unlocked '{apiName}'.");

			CheckWebcamStrained();
		}
		else
		{
			Debug.LogWarning($"SteamAchievementManager: SteamUserStats.SetAchievement failed for '{apiName}'.");
		}
#endif
	}

	private void CheckWebcamStrained()
	{
		if (!SteamManager.Initialized) return;
		if (IsAchievementUnlocked(WebcamStrainedApiName)) return;

		foreach (string apiName in OtherAchievementApiNames)
		{
			if (!IsAchievementUnlocked(apiName))
				return;
		}

		UnlockAchievement(WebcamStrainedApiName);
	}

	private bool IsAchievementUnlocked(string apiName)
	{
		if (_unlockedCache.Contains(apiName))
			return true;

#if !DISABLESTEAMWORKS
		if (SteamUserStats.GetAchievement(apiName, out bool achieved) && achieved)
		{
			_unlockedCache.Add(apiName);
			return true;
		}
#endif
		return false;
	}
}
