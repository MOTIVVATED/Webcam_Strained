using System;
using UnityEngine;

public class TiltManager : MonoBehaviour
{
  public static TiltManager Instance { get; private set; }

  public int Tilt { get; private set; }
  public int MaxTilt => maxTilt;

  [SerializeField] private int badCaughtTilt = 5;
  [SerializeField] private int goodMissedTilt = 10;
  [SerializeField] private int badSmashedTilt = -1;
	//[SerializeField] private float tiltPenaltyPerPoint = 0.05f;

	[SerializeField] private float timeScaleFactor = 0.88f;
	
  [SerializeField] private int maxTilt = 100;
  [SerializeField] private GameObject player;
  [SerializeField] FloatingTextSpawner floatingTextSpawner;

  public event Action<int> OnTiltIncreased;
  public event Action<int> OnTiltDecreased;
  public event Action OnMaxTiltReached;

	private void Awake()
	{
		if (Instance != null && Instance != this) { Destroy(gameObject); return; }
		Instance = this;
	}

	private void OnEnable()
	{
		GameEvents.OnObjectCollected += HandleCollected;
		GameEvents.OnObjectSmashed += HandleSmashed;
		GameEvents.OnObjectMissed += HandleMissed;
	}

	private void OnDisable()
	{
		GameEvents.OnObjectCollected -= HandleCollected;
		GameEvents.OnObjectSmashed -= HandleSmashed;
		GameEvents.OnObjectMissed -= HandleMissed;
	}

	public void HandleCollected(FallingObjectType type)
  {
		switch (type)
		{
			case FallingObjectType.bad:
			case FallingObjectType.webcam:
				AddTilt(badCaughtTilt);
				floatingTextSpawner.Spawn(badCaughtTilt, player.transform.position, type);
				break;
		}
	}

  public void HandleSmashed(FallingObjectType type, Vector3 position)
  {
    DecreaseTilt(badSmashedTilt);
  }

	public void HandleMissed(FallingObjectType type)
	{
		switch (type)
		{
			case FallingObjectType.tk15:
			case FallingObjectType.tk25:
			case FallingObjectType.tk111:
			case FallingObjectType.tk222:
			case FallingObjectType.tk555:
			case FallingObjectType.tk666:
			case FallingObjectType.tk1111:
				AddTilt(goodMissedTilt);
				floatingTextSpawner.Spawn(goodMissedTilt, player.transform.position, FallingObjectType.bad);
				break;
		}
	}

  private void AddTilt(int value)
  {
		Tilt = Mathf.Min(Tilt + value, maxTilt);
		OnTiltIncreased?.Invoke(Tilt);
		ApplyPenalty();

		if (Tilt >= maxTilt)
			OnMaxTiltReached?.Invoke();
  }

	private void DecreaseTilt(int value)
	{
		Tilt = Mathf.Max(Tilt + value, 0);
		OnTiltDecreased?.Invoke(Tilt);
		//ApplyPenalty();
	}
	
	private void ApplyPenalty()
	{
		//float penalty = 1f - (tiltPenaltyPerPoint * Tilt);
		//penalty = Mathf.Clamp(penalty, 0.1f, 1f);


		//float penalty = 0.8f;
		TimeScaleController.Instance.ReduceTimeScale();
		Debug.Log("Penalty Applied!");
	}
}
