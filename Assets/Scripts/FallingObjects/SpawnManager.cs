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
    [SerializeField] private float minX, maxX;

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

    private IEnumerator SpawnRuleLoop(SpawnRule rule)
    {
      int x = (int)Random.Range(minX, maxX);

      while (true)
        {
            float delay = Random.Range(rule.delayMin, rule.delayMax);
            yield return new WaitForSeconds(delay);

            if (Time.time < nextAllowedSpawnTime)
                continue;

            Spawn(rule.prefab, ref x);

            nextAllowedSpawnTime = Time.time + minGlobalSpawnInterval;
        }
    }
    private void Spawn(FallingObject prefab, ref int x)
    {
        FallingObject falling = Instantiate(
            prefab, spawnPoint.position, Quaternion.identity);

      ref int randomX = ref x;

      Debug.Log($"Initial X={randomX}");

      int random = Random.Range(1488, 1490);
        Debug.Log($"random={random}");
      if (random == 1488)
      {
        randomX += spawnShiftX;
        Debug.Log($"X after increase={randomX}");
      }
      else 
      { 
        randomX -= spawnShiftX;
        Debug.Log($"X after decrease={randomX}"); 
      }

      if (randomX < minX)
      {
        randomX += (spawnShiftX * 3);
        Debug.Log($"X after minX correction={randomX}");
      }
      if (randomX > maxX)
      {
        randomX -= (spawnShiftX * 3);
        Debug.Log($"X after maxX correction={randomX}");
      }

      falling.transform.position = new Vector3(randomX, spawnPoint.position.y, 0f);
      Debug.Log($"Spawned {falling.name} at x={randomX}");



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
      }
      x = randomX;
    }
}
