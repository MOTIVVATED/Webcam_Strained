using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;
public class SmashManager : MonoBehaviour
{
  public static SmashManager Instance { get; private set; }

  [SerializeField] private FloatingTextSpawner floatingTextSpawner;

	public event Action OnSmashed;

	private void OnEnable()
	{
		GameEvents.OnObjectSmashed += HandleSmashed;
	}

	private void OnDisable()
	{
		GameEvents.OnObjectSmashed -= HandleSmashed;
	}

	private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }
    Instance = this;
  }
  public void HandleSmashed(FallingObjectType type, Vector3 position)
  {
		switch (type)
    {
      case FallingObjectType.bad:
      case FallingObjectType.webcam:
        OnSmashed?.Invoke();
        floatingTextSpawner.Spawn(position);
				break;
		}
  }
}
