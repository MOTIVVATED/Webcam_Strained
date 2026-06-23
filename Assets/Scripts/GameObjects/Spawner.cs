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
	[SerializeField] private GameObject[] spawnPoints;

	[Header("Spawnlimits")]
	[SerializeField] private float minGlobalSpawnInterval = 0.5f;

	private Coroutine[] routines;

	private float nextAllowedSpawnTime;

	private void OnEnable()
	{
		if (rules == null || rules.Length == 0)
		{
			enabled = false;
			return;
		}
		nextAllowedSpawnTime = Time.time;

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

	private void Spawn(FallingObject prefab)
	{
		int spawnPointNumber = Random.Range(0, spawnPoints.Length);
		GameObject spawnPoint = spawnPoints[spawnPointNumber];

		FallingObject falling = Instantiate(
			prefab, spawnPoint.transform.position, Quaternion.identity);

		falling.transform.position = 
			new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, 0f);

		falling.OnCollected += (type, pos) => GameEvents.ObjectCollected(type, pos);
		falling.OnSmashed += (type, pos) => GameEvents.ObjectSmashed(type, pos);
		falling.OnMissed += (type) => GameEvents.ObjectMissed(type);
	}
}
