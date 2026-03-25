using System;
using UnityEngine;

public class TiltManager : MonoBehaviour
{
  public static TiltManager Instance { get; private set; }

  public int Tilt { get; private set; }

  [SerializeField] private int badCaughtTilt = 5;

  [SerializeField] private int goodMissedTilt = 10;

  [SerializeField] private int badSmashedTilt = -1;

  [SerializeField] private float timeScalePenalty = 0.8f;

  [SerializeField] private int maxTilt = 100;

  [SerializeField] private GameObject player;

  [SerializeField] FloatingTextSpawner floatingTextSpawner;
  public int MaxTilt => maxTilt;

  public event Action<int> OnTiltIncreased;

  public event Action<int > OnTiltDecreased;

  public event Action OnMaxTiltReached;

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
    case FallingObjectType.Bad:
    AddTilt(badCaughtTilt);
    floatingTextSpawner.Spawn(badCaughtTilt, player.transform.position, type);
    break;        
    }
  }
  public void HandleSmashed(FallingObjectType type)
  {
    DecreaseTilt(badSmashedTilt);
  }
   
  public void HandleMissed(FallingObjectType type)
  {
    switch (type)
    {
      case FallingObjectType.type_15tk:
        AddTilt(goodMissedTilt);
        floatingTextSpawner.Spawn(goodMissedTilt, player.transform.position, FallingObjectType.Bad);
        break;
      case FallingObjectType.type_25tk:
        AddTilt(goodMissedTilt);
        floatingTextSpawner.Spawn(goodMissedTilt, player.transform.position, FallingObjectType.Bad);
        break;
      case FallingObjectType.type_111tk:
        AddTilt(goodMissedTilt);
        floatingTextSpawner.Spawn(goodMissedTilt, player.transform.position, FallingObjectType.Bad);
        break;
      case FallingObjectType.type_222tk:
        AddTilt(goodMissedTilt);
        floatingTextSpawner.Spawn(goodMissedTilt, player.transform.position, FallingObjectType.Bad);
        break;
      case FallingObjectType.type_555tk:
        AddTilt(goodMissedTilt);
        floatingTextSpawner.Spawn(goodMissedTilt, player.transform.position, FallingObjectType.Bad);
        break;
      case FallingObjectType.type_666tk:
        AddTilt(goodMissedTilt);
        floatingTextSpawner.Spawn(goodMissedTilt, player.transform.position, FallingObjectType.Bad);
        break;
      case FallingObjectType.type_1111tk:
        AddTilt(goodMissedTilt);
        floatingTextSpawner.Spawn(goodMissedTilt, player.transform.position, FallingObjectType.Bad);
        break;
		}
  }
  private void DecreaseTilt(int value)
  {
    if (Tilt + value >= 0)
    { 
      Tilt += value;
      OnTiltDecreased?.Invoke(Tilt);
    }
  }
  private void AddTilt(int value)
  {
    if (Tilt + value >= 0)
    {
      Tilt += value;
      OnTiltIncreased?.Invoke(Tilt);
      Time.timeScale = Time.timeScale * timeScalePenalty;
    }
    else
    {
      Tilt = 0;
      OnTiltIncreased?.Invoke(Tilt);
    }
    if (Tilt >= maxTilt)
    {
      OnMaxTiltReached?.Invoke();
    }
  }
}
