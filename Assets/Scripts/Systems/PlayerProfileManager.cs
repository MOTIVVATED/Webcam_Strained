using System.IO;
using UnityEngine;

public class PlayerProfileManager : MonoBehaviour
{
	public static PlayerProfileManager Instance { get; private set; }

	private PlayerProfile _profile = new PlayerProfile();
	private string _savePath;

	private void Awake()
	{
		if (Instance != null && Instance != this) { Destroy(gameObject); return; }
		
		Instance = this;
		
		transform.SetParent(null);

		DontDestroyOnLoad(gameObject);

		_savePath = Path.Combine(Application.persistentDataPath, "player_profile.json");
		
		Load();

		Debug.Log(Application.persistentDataPath);
	}

	public PlayerProfile GetProfile() => _profile;

	public void SetName(string playerName)
	{
		_profile.playerName = playerName;
		Save();
	}

	public void RecordGame(int score, string sceneName, bool unlockNext)
	{
		_profile.gamesPlayed++;
		_profile.totalScore += score;
		_profile.money += CalculateMoney(score);

		bool isFirstCompletion = false;
		if (unlockNext)
		{
			isFirstCompletion = TryMarkSceneCompleted(sceneName);
			TryAdvanceUnlock(sceneName);
		}

		Save();
		Debug.Log($"Profile updated: avg={_profile.GetAverageScore()}, rank={_profile.GetRank()}, highestSceneUnlocked={_profile.highestSceneUnlocked}");

		if (unlockNext)
			GameEvents.LevelCompleted(sceneName, isFirstCompletion);

		GameEvents.ProfileUpdated();
	}

	public void SpendMoney(int amount)
	{
		_profile.money = Mathf.Max(0, _profile.money - amount);
		Save();
	}

	public bool CanPurchaseTier(UpgradeBranch branch, int tierIndex, int cost)
	{
		var progress = _profile.GetBranchProgress(branch);
		if (progress == null) return false;
		if (tierIndex < 0 || tierIndex >= progress.ownedTiers.Length) return false;
		if (progress.ownedTiers[tierIndex]) return false;

		int requiredRank = tierIndex + 1;
		if (_profile.GetRankNumber() < requiredRank) return false;
		if (_profile.money < cost) return false;

		return true;
	}

	public void PurchaseTier(UpgradeBranch branch, int tierIndex, int cost)
	{
		var progress = _profile.GetBranchProgress(branch);
		if (progress == null || tierIndex < 0 || tierIndex >= progress.ownedTiers.Length)
		{
			Debug.LogWarning($"PlayerProfileManager: Invalid tier {tierIndex} for branch {branch}.");
			return;
		}

		progress.ownedTiers[tierIndex] = true;
		_profile.money = Mathf.Max(0, _profile.money - cost);
		Save();
		GameEvents.UpgradesChanged();
	}

	public void EquipTier(UpgradeBranch branch, int tierIndex)
	{
		var progress = _profile.GetBranchProgress(branch);
		if (progress == null || tierIndex < 0 || tierIndex >= progress.ownedTiers.Length)
		{
			Debug.LogWarning($"PlayerProfileManager: Invalid tier {tierIndex} for branch {branch}.");
			return;
		}

		if (!progress.ownedTiers[tierIndex])
		{
			Debug.LogWarning($"PlayerProfileManager: Attempted to equip un-owned tier {tierIndex} on {branch}.");
			return;
		}

		progress.equippedTier = tierIndex;
		Save();
		GameEvents.UpgradesChanged();
	}

	public bool CanPurchaseBan(int cost, int requiredRank)
	{
		if (_profile.banOwned) return false;
		if (_profile.GetRankNumber() < requiredRank) return false;
		if (_profile.money < cost) return false;
		return true;
	}

	public void PurchaseBan(int cost)
	{
		_profile.banOwned = true;
		_profile.money = Mathf.Max(0, _profile.money - cost);
		Save();
		GameEvents.UpgradesChanged();
	}

	public void SetBanEquipped(bool equipped)
	{
		if (equipped && !_profile.banOwned)
		{
			Debug.LogWarning("PlayerProfileManager: Attempted to equip Ban before owning it.");
			return;
		}
		_profile.banEquipped = equipped;
		Save();
		GameEvents.UpgradesChanged();
	}

	public bool CanPurchaseEnlarge(int cost, int requiredRank)
	{
		if (_profile.enlargeOwned) return false;
		if (_profile.GetRankNumber() < requiredRank) return false;
		if (_profile.money < cost) return false;
		return true;
	}

	public void PurchaseEnlarge(int cost)
	{
		_profile.enlargeOwned = true;
		_profile.money = Mathf.Max(0, _profile.money - cost);
		Save();
		GameEvents.UpgradesChanged();
	}

	public void SetEnlargeEquipped(bool equipped)
	{
		if (equipped && !_profile.enlargeOwned)
		{
			Debug.LogWarning("PlayerProfileManager: Attempted to equip Enlarge before owning it.");
			return;
		}
		_profile.enlargeEquipped = equipped;
		Save();
		GameEvents.UpgradesChanged();
	}

	public void SetGameIntroGuideSeen()
	{
		_profile.hasSeenGameIntroGuide = true;
		Save();
	}

	public void SetModelSelectionGuideSeen()
	{
		_profile.hasSeenModelSelectionGuide = true;
		Save();
	}

	public void SetLevelingPanelGuideSeen()
	{
		_profile.hasSeenLevelingPanelGuide = true;
		Save();
	}

	public void SetHighestRankGuideShown(int rank)
	{
		_profile.highestRankGuideShown = rank;
		Save();
	}

#if UNITY_EDITOR
	[ContextMenu("DEBUG: Reset Player Profile To Zero")]
	private void ResetProfileToZero()
	{
		_profile.money = 0;
		_profile.gamesPlayed = 0;
		_profile.totalScore = 0;
		_profile.highestSceneUnlocked = 0;
		_profile.sceneCompletedOnce = new bool[6];
		_profile.saveForLaterUpgrade = new UpgradeBranchProgress();
		_profile.movementUpgrade = new UpgradeBranchProgress();
		_profile.laneUpgrade = new UpgradeBranchProgress();
		_profile.banOwned = false;
		_profile.banEquipped = false;
		_profile.enlargeOwned = false;
		_profile.enlargeEquipped = false;
		_profile.hasSeenGameIntroGuide = false;
		_profile.hasSeenModelSelectionGuide = false;
		_profile.hasSeenLevelingPanelGuide = false;
		_profile.highestRankGuideShown = 0;

		Save();
		Debug.Log($"PlayerProfileManager: Profile reset to zero. money=0, gamesPlayed=0, totalScore=0, highestSceneUnlocked=0, rank={_profile.GetRank()}.");
	}

	[ContextMenu("DEBUG: Max Out Player Profile")]
	private void MaxOutProfile()
	{
		_profile.money = 88000;
		_profile.gamesPlayed = 10;
		_profile.totalScore = 1000000;
		_profile.highestSceneUnlocked = 5;
		_profile.sceneCompletedOnce = new bool[6] { true, true, true, true, true, true };
		_profile.banOwned = true;
		_profile.banEquipped = true;
		_profile.enlargeOwned = true;
		_profile.enlargeEquipped = true;

		Save();
		Debug.Log($"PlayerProfileManager: Profile maxed out. money={_profile.money}, gamesPlayed={_profile.gamesPlayed}, totalScore={_profile.totalScore}, rank={_profile.GetRank()}, highestSceneUnlocked={_profile.highestSceneUnlocked}.");
	}

	[ContextMenu("Guide/Reset Guide Flags Only")]
	private void ResetGuideFlagsOnly()
	{
		_profile.hasSeenGameIntroGuide = false;
		_profile.hasSeenModelSelectionGuide = false;
		_profile.hasSeenLevelingPanelGuide = false;
		_profile.highestRankGuideShown = 0;

		Save();
		Debug.Log("PlayerProfileManager: Guide flags reset. hasSeenGameIntroGuide=false, hasSeenModelSelectionGuide=false, hasSeenLevelingPanelGuide=false, highestRankGuideShown=0.");
	}

	[ContextMenu("Guide/Set Highest Rank Guide Shown To 0")]
	private void DebugForceRankGuideRetrigger()
	{
		_profile.highestRankGuideShown = 0;

		Save();
		Debug.Log("PlayerProfileManager: highestRankGuideShown reset to 0.");
	}
#endif

	private bool TryMarkSceneCompleted(string sceneName)
	{
		int index = GameSceneOrder.IndexOf(sceneName);
		if (index < 0 || index >= _profile.sceneCompletedOnce.Length)
		{
			Debug.LogWarning($"PlayerProfileManager: '{sceneName}' is not in GameSceneOrder.Scenes. Skipping completion tracking.");
			return false;
		}

		if (_profile.sceneCompletedOnce[index])
			return false;

		_profile.sceneCompletedOnce[index] = true;
		return true;
	}

	private void TryAdvanceUnlock(string sceneName)
	{
		int index = GameSceneOrder.IndexOf(sceneName);
		if (index < 0)
		{
			Debug.LogWarning($"PlayerProfileManager: '{sceneName}' is not in GameSceneOrder.Scenes. Skipping unlock advance.");
			return;
		}

		if (index == _profile.highestSceneUnlocked && index < GameSceneOrder.Scenes.Length - 1)
			_profile.highestSceneUnlocked++;
	}

	private void Save()
	{
		string json = JsonUtility.ToJson(_profile, prettyPrint: true);
		File.WriteAllText(_savePath, json);
	}

	private void Load()
	{
		if (File.Exists(_savePath))
		{
			string json = File.ReadAllText(_savePath);
			_profile = JsonUtility.FromJson<PlayerProfile>(json) ?? new PlayerProfile();
		}
	}

	private int CalculateMoney(int total)
	{
		int moneyEarned = (total * 75 / 10000);

		if (moneyEarned < 20) { moneyEarned = 20; }

		return moneyEarned;
	}
}