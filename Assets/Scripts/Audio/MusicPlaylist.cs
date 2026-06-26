using System.Collections;
using UnityEngine;

public class MusicPlaylist : MonoBehaviour
{
	[Header("Clips")]
	[SerializeField] private AudioClip[] parts;

	[Header("Audio Source")]
	[SerializeField] private AudioSource source;

	[Header("Options")]
	[SerializeField] private bool dontRepeatSameTwice = false;

	private int lastIndex = -1;
	private Coroutine routine;

	public float CurrentClipLength { get; private set; }

	private void Reset()
	{
		source = GetComponent<AudioSource>();
	}

	private void OnDisable()
	{
		if (routine != null)
		{
			StopCoroutine(routine);
			routine = null;
		}

		if (source != null)
			source.Stop();
	}

	public void Update()
	{
		if (source == null) return;

		bool paused = PauseManager.Instance != null && PauseManager.Instance.IsPaused;

		if (paused && source.isPlaying)
			source.Pause();
		else if (!paused && !source.isPlaying)
			source.UnPause();
	}

	public void StartRound()
	{
		if (routine != null)
		{
			StopCoroutine(routine);
			routine = null;
		}

		routine = StartCoroutine(PlayOnce());
	}

	private IEnumerator PlayOnce()
	{
		if (source == null)
		{
			Debug.LogError("MusicPlaylist : AudioSource not assigned.");
			yield break;
		}

		if (parts == null || parts.Length == 0)
		{
			Debug.LogWarning("MusicPlaylist: No clip assigned.");
			yield break;
		}

		int idx = GetRandomIndex();
		lastIndex = idx;

		AudioClip chosen = parts[idx];
		
		CurrentClipLength = Mathf.Max(15, chosen.length);
		
		string currentClipName = chosen.name;
		
		float buffer = GetDurationBufferFromClip(currentClipName);
		
		source.clip = chosen;
		
		source.Play();

		GameEvents.MusicClipSelected(CurrentClipLength, buffer);

		while (source.clip != null && (source.isPlaying ||
			(PauseManager.Instance != null && PauseManager.Instance.IsPaused)))
			yield return null;

		routine = null;
	}

	private int GetRandomIndex()
	{
		if (!dontRepeatSameTwice || parts.Length <= 1)
			return Random.Range(0, parts.Length);

		int idx;
		do
		{
			idx = Random.Range(0, parts.Length);
		}
		while (idx == lastIndex);

		return idx;
	}

	private float GetDurationBufferFromClip(string clipName)
	{
		float buffer = 5f;

		switch (clipName)
		{
			case ("stolko serih"): buffer = 7f; break;
			case ("ikslav2"):buffer = 4.5f; break;
			case ("ikslav"): buffer = 9f; break;
			case ("felching"): buffer = 4f; break;
			case ("drooling rulit"): buffer = 3.5f; break;
			case ("drooling"): buffer = 10f; break;
			default: Debug.Log("Audio clip name not found!");
				break;
		}
		return buffer;
	}
}