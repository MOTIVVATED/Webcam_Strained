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

  private void Reset()
  {
    source = GetComponent<AudioSource>();
  }
  private void OnEnable()
  {
    if (routine == null)
      routine = StartCoroutine(PlayLoop());
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

  private IEnumerator PlayLoop()
  {
    if (source == null)
    {
      Debug.LogError("MusicPlaylist : AudioSource not assigned.");
      yield break;
    }

    if (parts == null || parts.Length == 0)
    {
      Debug.LogWarning("MyusicPlaylist: No clip assigned.");
      yield break;
    }

    while (true)
    {
      while (PauseManager.Instance != null && PauseManager.Instance.IsPaused)
        yield return null;

			int idx = GetRandomIndex();
      lastIndex = idx;

      source.clip = parts[idx];
      source.Play();


      while (source.clip != null && (source.isPlaying || 
        (PauseManager.Instance != null && PauseManager.Instance.IsPaused)))
      yield return null;


      yield return null;
    }

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
