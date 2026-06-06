using UnityEngine;
using System;

public class ReducerManager : MonoBehaviour
{
	public static ReducerManager Instance { get; private set; }

	[SerializeField] private GameObject player;
	[SerializeField] private float penaltyReduction = 0.1f;
	[SerializeField] private FloatingTextSpawner floatingTextSpawner;
	[SerializeField] private DistractionOverlay distractionOverlay;

	private float currentPenalty = 1f;

	private void Awake()
	{
		if (Instance != null && Instance != this) { Destroy(gameObject); return; }
		Instance = this;
	}

	public void HandleCollected(FallingObjectType type)
	{
		switch (type)
		{
			case FallingObjectType.reduser:
				currentPenalty = Mathf.Max(currentPenalty - penaltyReduction, 0.1f);
				TimeScaleController.Instance.SetPenalty(currentPenalty);
				floatingTextSpawner.Spawn(0, player.transform.position, type);
				break;
			case FallingObjectType.webcam:
				DistractionOverlay.Instance.Show();
				break;
		}
	}
	//One thing to be aware of: both TiltManager and ReducerManager
	//now call SetPenalty independently
	//They're setting the same value, so whichever calls last wins.
	//If we want them to stack properly — tilt penalty and reducer penalty
	//both active at the same time —
	//TimeScaleController needs two separate penalty slots.
}
