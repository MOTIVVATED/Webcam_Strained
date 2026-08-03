using UnityEngine;

[CreateAssetMenu(fileName = "LaneUpgradeConfig", menuName = "Upgrades/Lane Config")]
public class LaneUpgradeConfig : ScriptableObject
{
	[System.Serializable]
	public class Tier
	{
		public string label;
		public int activeLaneCount;
		public int cost;
	}

	[Tooltip("Index 0 = starting tier (always owned). Rank requirement is index + 1.")]
	public Tier[] tiers = new Tier[6]
	{
		new Tier { label = "2", activeLaneCount = 2, cost = 0 },
		new Tier { label = "3", activeLaneCount = 3, cost = 25 },
		new Tier { label = "4", activeLaneCount = 4, cost = 50 },
		new Tier { label = "5", activeLaneCount = 5, cost = 100 },
		new Tier { label = "6", activeLaneCount = 6, cost = 200 },
		new Tier { label = "7", activeLaneCount = 7, cost = 500 },
	};
}