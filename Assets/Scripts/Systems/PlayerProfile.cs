using System;
using UnityEngine;

[Serializable]
public class UpgradeBranchProgress
{
	public bool[] ownedTiers = new bool[6] { true, false, false, false, false, false };
	public int equippedTier = 0;
}

[Serializable]
public class PlayerProfile
{
	public string playerName = "Player";
	public int money = 0;
	public int gamesPlayed = 0;
	public int totalScore = 0;
	public int highestSceneUnlocked = 0;

	public UpgradeBranchProgress saveForLaterUpgrade = new UpgradeBranchProgress();
	public UpgradeBranchProgress movementUpgrade = new UpgradeBranchProgress();
	public UpgradeBranchProgress laneUpgrade = new UpgradeBranchProgress();

	public bool banOwned = false;
	public bool banEquipped = false;
	public bool enlargeOwned = false;
	public bool enlargeEquipped = false;

	public int GetAverageScore()
	{
		if (gamesPlayed == 0) return 0;
		return Mathf.RoundToInt((float)totalScore / gamesPlayed);
	}

	public int GetRankNumber()
	{
		int avg = GetAverageScore();
		if (avg >= 50000) return 6;
		if (avg >= 25000) return 5;
		if (avg >= 12000) return 4;
		if (avg >= 6000) return 3;
		if (avg >= 3000) return 2;
		return 1;
	}

	public string GetRank() => GetRankNumber().ToString();

	public UpgradeBranchProgress GetBranchProgress(UpgradeBranch branch)
	{
		switch (branch)
		{
			case UpgradeBranch.SaveForLater: return saveForLaterUpgrade;
			case UpgradeBranch.Movement: return movementUpgrade;
			case UpgradeBranch.Lanes: return laneUpgrade;
			default:
				Debug.LogWarning($"PlayerProfile: Unknown branch '{branch}'.");
				return null;
		}
	}

	public bool IsSceneUnlocked(string sceneName)
	{
		int index = GameSceneOrder.IndexOf(sceneName);
		if (index < 0)
		{
			Debug.LogWarning($"PlayerProfile: '{sceneName}' is not in GameSceneOrder.Scenes. Treating as locked.");
			return false;
		}
		Debug.Log($"highestSceneUnlocked={highestSceneUnlocked}");
		Debug.Log($"index of '{sceneName}'={index}");
		return index <= highestSceneUnlocked;
	}
}