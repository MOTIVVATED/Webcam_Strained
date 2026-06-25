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

	private void OnEnable()
	{
		// Round no longer auto-starts on enable; GameManager (or whoever
		// owns round flow) should call StartRound() once it's ready.
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

	/// <summary>
	/// Picks one random clip for the round, plays it once, and raises
	/// GameEvents with the clip's length so GameManager can derive the
	/// round duration from it. Call this at round start / restart.
	/// </summary>
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
		CurrentClipLength = chosen.length;

		source.clip = chosen;
		source.Play();

		// Let GameManager know which clip we picked and how long it is,
		// so it can derive gameDuration from it.
		GameEvents.MusicClipSelected(CurrentClipLength);

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
}