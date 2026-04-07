using System.Collections;
using UnityEngine;

public class MusicPlaylist : MonoBehaviour
{
    [Header("Clips (mp3 parts")]
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
    if (PauseManager.Instance != null && PauseManager.Instance.IsPaused)
    {
      if (source.isPlaying)
        source.Pause();
    }
    else
    {
      if (!source.isPlaying)
        source.UnPause();
    }
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
            Debug.LogWarning("MysicPlaylist: No clip assigned.");
            yield break;
        }

        while (PauseManager.Instance != null && !PauseManager.Instance.IsPaused)
        {
            int idx = GetRandomIndex();
            lastIndex = idx;

            source.clip = parts[idx];
            source.Play();

            // ждЄм пока реально доиграет (на случай если Time.timeScale = 0)
            while (source.isPlaying)
                yield return null;

            // на вс€кий Ч один кадр, чтобы следующий стартовал УчистоФ
            yield return null;
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
