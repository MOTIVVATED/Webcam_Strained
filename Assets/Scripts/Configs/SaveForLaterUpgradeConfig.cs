using UnityEngine;

[CreateAssetMenu(fileName = "SaveForLaterUpgradeConfig", menuName = "Upgrades/Save For Later Config")]
public class SaveForLaterUpgradeConfig : ScriptableObject
{
	[System.Serializable]
	public class Tier
	{
		public string label;
		[Tooltip("-1 means unlimited charges.")]
		public int maxCharges;
		public int cost;
	}

	[Tooltip("Index 0 = starting tier (always owned). Rank requirement is index + 1.")]
	public Tier[] tiers = new Tier[6]
	{
		new Tier { label = "0", maxCharges = 0, cost = 0 },
		new Tier { label = "5", maxCharges = 5, cost = 25 },
		new Tier { label = "10", maxCharges = 10, cost = 100 },
		new Tier { label = "15", maxCharges = 15, cost = 150 },
		new Tier { label = "20", maxCharges = 20, cost = 200 },
		new Tier { label = "Unlimited", maxCharges = -1, cost = 250 },
	};
}