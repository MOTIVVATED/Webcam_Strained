using UnityEngine;

[CreateAssetMenu(fileName = "MovementUpgradeConfig", menuName = "Upgrades/Movement Config")]
public class MovementUpgradeConfig : ScriptableObject
{
	[System.Serializable]
	public class Tier
	{
		public string label;
		[Tooltip("If true, this tier uses LaneStepMovement instead of PlayerInput/PlayerMovement. Speed is ignored.")]
		public bool useLegacyLaneStep;
		public float speed;
		public int cost;
	}

	[Tooltip("Index 0 = starting tier (always owned). Rank requirement is index + 1.")]
	public Tier[] tiers = new Tier[6]
	{
		new Tier { label = "Lane Step", useLegacyLaneStep = true, speed = 0, cost = 0 },
		new Tier { label = "Speed 10", useLegacyLaneStep = false, speed = 10, cost = 25 },
		new Tier { label = "Speed 11", useLegacyLaneStep = false, speed = 11, cost = 100 },
		new Tier { label = "Speed 12", useLegacyLaneStep = false, speed = 12, cost = 150 },
		new Tier { label = "Speed 13", useLegacyLaneStep = false, speed = 13, cost = 200 },
		new Tier { label = "Speed 14", useLegacyLaneStep = false, speed = 14, cost = 250 },
	};
}