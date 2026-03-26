using System;
using UnityEngine;

public class ReducerManager : MonoBehaviour
{
	public static ReducerManager Instance { get; private set; }

	[SerializeField] private GameObject player;

	[SerializeField] private float timeScaleReduction = 0.4f;

	[SerializeField] private float minTimeScale = 1f;

	[SerializeField] private FloatingTextSpawner floatingTextSpawner;

	[SerializeField] private DistractionOverlay distractionOverlay;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
	}
	public void HandleCollected(FallingObjectType type)
	{
		switch (type)
		{
			case FallingObjectType.reduser:
				ReducerManager.Instance.ReduceTimeScale(timeScaleReduction);
				floatingTextSpawner.Spawn(0, player.transform.position, type);
				Debug.Log("Time scale reduced by " + timeScaleReduction);
				break;
			case FallingObjectType.webcam:
				DistractionOverlay.Instance.Show();
				break;

		}
	}
	public void ReduceTimeScale(float value)
	{ 
		if (Time.timeScale - value >= minTimeScale)
		{
			Time.timeScale -= timeScaleReduction;
			return;
		}
		else
		{
			Time.timeScale = minTimeScale;
		}
	}
}
