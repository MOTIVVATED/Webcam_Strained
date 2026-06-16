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

	private void OnEnable()
	{
		GameEvents.OnObjectCollected += HandleCollected;
	}

	private void OnDisable()
	{
		GameEvents.OnObjectCollected -= HandleCollected;
	}

	private void Awake()
	{
		if (Instance != null && Instance != this) { Destroy(gameObject); return; }
		Instance = this;
	}

	public void HandleCollected(FallingObjectType type, Vector3 pos)
	{
		switch (type)
		{
			case FallingObjectType.reduser:
				currentPenalty = Mathf.Max(currentPenalty - penaltyReduction, 0.1f);
				TimeScaleController.Instance.ReduceTimeScale();
				floatingTextSpawner.Spawn(0, player.transform.position, type);
				break;
			case FallingObjectType.webcam:
				DistractionOverlay.Instance.Show();
				break;
		}
	}
}
