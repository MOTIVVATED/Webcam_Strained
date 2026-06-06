using System;
using System.Collections;
using UnityEngine;

public static class WaitForInstance
{
	public static IEnumerator Get<T>(
			Func<T> getter,
			Action<T> onReady,
			float timeoutSeconds = 5f,
			string label = null) where T : class
	{
		float elapsed = 0f;
		while (getter() == null)
		{
			elapsed += Time.deltaTime;
			if (elapsed >= timeoutSeconds)
			{
				Debug.LogWarning(
						$"WaitForInstance: {label ?? typeof(T).Name} not found after {timeoutSeconds}s.");
				yield break;
			}
			yield return null;
		}
		onReady(getter());
	}
}