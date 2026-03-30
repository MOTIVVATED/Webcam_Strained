using UnityEngine;
using System;
public class SmashManager : MonoBehaviour
{
  public static SmashManager Instance { get; private set; }

  [SerializeField] FloatingTextSpawner floatingTextSpawner;
  [SerializeField] TiltManager tiltManager;

  private GameObject bad;
  private GameObject webcam;

	public event Action OnSmashed;
    
  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }
    Instance = this;
  }
  public void HandleSmashed(FallingObjectType type)
  {
    switch (type)
    {
      case FallingObjectType.Bad:
        OnSmashed?.Invoke();
        bad = GameObject.FindGameObjectWithTag("bad");
        floatingTextSpawner.Spawn(bad.transform.position);
        break;
      case FallingObjectType.webcam:
        OnSmashed?.Invoke();
        webcam = GameObject.FindGameObjectWithTag("bad");
        floatingTextSpawner.Spawn(webcam.transform.position);
				break;
		}
  }
}
