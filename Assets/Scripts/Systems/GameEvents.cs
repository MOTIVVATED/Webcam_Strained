using System;
using UnityEngine;

public static class GameEvents
{
  public static event Action<FallingObjectType> OnObjectCollected;
  public static event Action<FallingObjectType, Vector3> OnObjectSmashed;
  public static event Action<FallingObjectType> OnObjectMissed;

  public static void ObjectCollected(FallingObjectType type)
    => OnObjectCollected?.Invoke(type);

  public static void ObjectSmashed(FallingObjectType type, Vector3 position)
    => OnObjectSmashed?.Invoke(type, position);

  public static void ObjectMissed(FallingObjectType type)
    => OnObjectMissed?.Invoke(type);
}
