using System;
using UnityEngine;

[Serializable]
public class PlayerProfile
{
	public string playerName = "Player";
	public int money = 0;
	public int gamesPlayed = 0;
	public int totalScore = 0;

	public int GetAverageScore()
	{
		if (gamesPlayed == 0) return 0;
		return Mathf.RoundToInt((float)totalScore / gamesPlayed);
	}

	public string GetRank()
	{
		int avg = GetAverageScore();
		if (avg >= 50000) return "Diamond";
		if (avg >= 25000) return "Platinum";
		if (avg >= 10000) return "Gold";
		if (avg >= 3000) return "Silver";
		return "Bronze";
	}
}
