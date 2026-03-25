using System.Collections;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
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

  [Header("Spawn position")]
  [SerializeField] private Transform spawnPoint;
  [SerializeField] private int minX, maxX;

	private int lastSpawnX; // remembers the previous spawn X

	// must be in range of (maxX-minX)/2 to avoid objects spawning outside of the limits
	[SerializeField] private int spawnShiftX = 1;

  [Header("Spawn limits")]
  [SerializeField] private float minGlobalSpawnInterval = 0.5f;

  [SerializeField] private Sprite[] badSprites;

  [SerializeField] private bool subscribeOnEvents;

  private Coroutine[] routines;

  private float nextAllowedSpawnTime;

  private void OnEnable()
  {
    if (rules == null || rules.Length == 0)
    {
      Debug.LogWarning("No spawn rules assigned to SpawnManager.");
      enabled = false;
      return;
    }

    nextAllowedSpawnTime = Time.time;

    routines = new Coroutine[rules.Length];

    for (int i = 0; i < rules.Length; i++)
    {
      if (rules[i] != null && rules[i].enabled && rules[i].prefab != null)
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
	private void Start()
	{
		// Seed with a random position on start
		lastSpawnX = Random.Range(minX, maxX);
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
    FallingObject falling = Instantiate(
    prefab, spawnPoint.position, Quaternion.identity);

		int randomX = Random.Range(lastSpawnX - spawnShiftX, lastSpawnX + spawnShiftX);

    if (randomX < minX) randomX += spawnShiftX*3;

    if (randomX > maxX) randomX -= spawnShiftX*3;

		falling.transform.position = new Vector3(randomX, spawnPoint.position.y, 0f);
		lastSpawnX = randomX; // remember for next spawn

		if (falling.ObjectType == FallingObjectType.Bad
				&& badSprites != null && badSprites.Length > 0)
		{
			falling.SetSprite(badSprites[Random.Range(0, badSprites.Length)]);
		}
		if (subscribeOnEvents)
    {
      falling.OnCollected +=  ScoreManager.Instance.HandleCollected;
      falling.OnCollected +=  TiltManager.Instance.HandleCollected;
      falling.OnMissed +=     TiltManager.Instance.HandleMissed;
      falling.OnSmashed +=    SmashManager.Instance.HandleSmashed;
      falling.OnSmashed +=    TiltManager.Instance.HandleSmashed;
      falling.OnCollected +=  ReducerManager.Instance.HandleCollected;
		}
  }
}
