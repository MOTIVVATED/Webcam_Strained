using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[System.Serializable]
	public class SpawnRule
	{
		public FallingObject prefab;
		public float delayMin = 1f;
		public float delayMax = 3f;
		public bool enabled = true;
	}

	[Header("Spawn rules (per prefab)")]
	[SerializeField] private SpawnRule[] rules;

	[Header("Spawn Points")]
	[Tooltip("Ordered so that taking the first N entries gives the correct active-lane subset for lane count N.")]
	[SerializeField] private GameObject[] spawnPoints;

	[Header("Lane Upgrade")]
	[SerializeField] private LaneUpgradeConfig laneUpgradeConfig;

	[Header("Spawnlimits")]
	[SerializeField] private float minGlobalSpawnInterval = 0.1f;

	private Coroutine[] routines;

	private float nextAllowedSpawnTime;
	private int activeSpawnPointCount;

	private void OnEnable()
	{
		if (rules == null || rules.Length == 0)
		{
			enabled = false;
			return;
		}
		nextAllowedSpawnTime = Time.time;
		activeSpawnPointCount = ComputeActiveSpawnPointCount();

		routines = new Coroutine[rules.Length];

		for (int i = 0; i < rules.Length; i++)
		{
			if (rules[i] != null && rules[i].enabled && rules[i] != null)
				routines[i] = StartCoroutine(SpawnRuleLoop(rules[i]));
		}
	}
	private void OnDisable()
	{
		if (routines == null) return;
		for (int i = 0; i < routines.Length; i++)
		{
			if (routines[i] != null) StopCoroutine(routines[i]);
		}
	}
	private IEnumerator SpawnRuleLoop(SpawnRule rule)
	{
		while (true)
		{
			float delay = Random.Range(rule.delayMin, rule.delayMax);

			yield return new WaitForSeconds(delay);

			if (Time.time < nextAllowedSpawnTime)
				continue;

			Spawn(rule.prefab);
			nextAllowedSpawnTime = Time.time + minGlobalSpawnInterval;
		}
	}

	private int ComputeActiveSpawnPointCount()
	{
		if (laneUpgradeConfig == null || laneUpgradeConfig.tiers == null || laneUpgradeConfig.tiers.Length == 0)
			return spawnPoints.Length;

		int tier = 0;
		if (PlayerProfileManager.Instance != null)
			tier = PlayerProfileManager.Instance.GetProfile().laneUpgrade.equippedTier;

		tier = Mathf.Clamp(tier, 0, laneUpgradeConfig.tiers.Length - 1);
		int count = laneUpgradeConfig.tiers[tier].activeLaneCount;
		return Mathf.Clamp(count, 1, spawnPoints.Length);
	}

	private void Spawn(FallingObject prefab)
	{
		int spawnPointNumber = Random.Range(0, activeSpawnPointCount);
		GameObject spawnPoint = spawnPoints[spawnPointNumber];

		FallingObject falling = Instantiate(
			prefab, spawnPoint.transform.position, Quaternion.identity);

		falling.transform.position =
			new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, 0f);

		falling.OnCollected += (type, pos) =>
		{
			if (type == FallingObjectType.saveForLater)
				GameEvents.PowerUpCollected();
			else
				GameEvents.ObjectCollected(type, pos);
		};
		falling.OnSmashed += (type, pos) => GameEvents.ObjectSmashed(type, pos);
		falling.OnMissed += (type) => GameEvents.ObjectMissed(type);
	}
}