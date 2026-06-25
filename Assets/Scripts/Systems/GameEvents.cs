using System;
using UnityEngine;

public static class GameEvents
{
	public static event Action<FallingObjectType, Vector3> OnObjectCollected;
	public static event Action<FallingObjectType, Vector3> OnObjectSmashed;
	public static event Action<FallingObjectType> OnObjectMissed;
	public static event Action<float> OnMusicClipSelected;


	public static void ObjectCollected(FallingObjectType type, Vector3 pos)
		=> OnObjectCollected?.Invoke(type, pos);

	public static void ObjectSmashed(FallingObjectType type, Vector3 position)
		=> OnObjectSmashed?.Invoke(type, position);

	public static void ObjectMissed(FallingObjectType type)
		=> OnObjectMissed?.Invoke(type);

	public static void MusicClipSelected(float clipLength)
		=> OnMusicClipSelected?.Invoke(clipLength);
}