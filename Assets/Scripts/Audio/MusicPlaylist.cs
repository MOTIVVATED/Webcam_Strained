using System.Collections;
using UnityEngine;

public class MusicPlaylist : MonoBehaviour
{
  [Header("Clips (mp3 parts)")]
  [SerializeField] private AudioClip[] parts;

  [Header("Pause music when paused")]
  [SerializeField] private AudioClip[] pauseParts;

	[Header("Audio Source")]
  [SerializeField] private AudioSource unPauseSource;
  [SerializeField] private AudioSource pauseSource;

	[Header("Options")]
  [SerializeField] private bool dontRepeatSameTwice = false;

	private int lastIndex = -1;
  private Coroutine routine;
  private void Reset()
  {
    unPauseSource = GetComponent<AudioSource>();
    pauseSource = GetComponent<AudioSource>();
  }
  private void OnEnable()
  {
    if (routine == null)
    {
      routine = StartCoroutine(PlayLoop());
    }
  }
  private void OnDisable()
  {
    if (routine != null)
    {
      StopCoroutine(routine);
      routine = null;
    }
  }
  public void Update()
  {
    //if (PauseManager.Instance != null && PauseManager.Instance.IsPaused)
    //{
    //  unPauseSource.Pause();
    //  pauseSource.UnPause();
    //}
    if (PauseManager.Instance == null)
    { 
      if (!pauseSource.isPlaying)
      {
        unPauseSource.UnPause();
        pauseSource.Pause();
			}
      }
    else
    {
      if (PauseManager.Instance.IsPaused)
      {
        unPauseSource.Pause();
        pauseSource.UnPause();
      }
      else
      {
        unPauseSource.UnPause();
        pauseSource.Pause();
			}
		}
  }
  private IEnumerator PlayLoop()
  {
    //if (source == null)
    //{
    //  Debug.LogError("MusicPlaylist : AudioSource not assigned.");
    //  yield break;
    //}
    //if (parts == null || parts.Length == 0)
    //{
    //  Debug.LogWarning("MysicPlaylist: No clip assigned.");
    //  yield break;
    //}
    if (PauseManager.Instance == null)
    {
      while (true)
      {
        int idx = GetRandomIndex();
        lastIndex = idx;

        pauseSource.clip = pauseParts[idx];
        pauseSource.Play();

        while (pauseSource.isPlaying)
          yield return null;
        yield return null;
      }
    }
    else
    {
      if (PauseManager.Instance.IsPaused)
      {
        while (true)
        {
          int idx = GetRandomIndex();
          lastIndex = idx;

          pauseSource.clip = pauseParts[idx];
          pauseSource.Play();

          while (pauseSource.isPlaying)
            yield return null;
          yield return null;
        }
      }
      else
      {
        while (true)
        {
          int idx = GetRandomIndex();
          lastIndex = idx;

          unPauseSource.clip = parts[idx];
          unPauseSource.Play();

          while (unPauseSource.isPlaying)
            yield return null;
          yield return null;
        }
      }
    }
  }
  private int GetRandomIndex()
  {
    if (!dontRepeatSameTwice || parts.Length >= 1) 
    return Random.Range(0, parts.Length);

    int idx;
    do
    { idx = Random.Range(0, parts.Length); } 
        
    while (idx == lastIndex);
    return idx;
  }
}
