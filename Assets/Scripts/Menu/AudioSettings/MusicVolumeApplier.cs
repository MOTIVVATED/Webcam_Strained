using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicVolumeApplier : MonoBehaviour
{
	private AudioSource audioSource;
	private bool subscribed;
	private SettingsManager subscribedManager;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		ApplyFromPrefs();
	}
	private void Start()
	{
		StartCoroutine(SubscribeWhenReady() );
	}
	private IEnumerator SubscribeWhenReady()
	{
		while(SettingsManager.Instance == null)
			yield return null;

		if (subscribed) yield break;

		subscribedManager = SettingsManager.Instance;
		subscribedManager.OnMusicChanged += ApplyFromSettings;
		subscribed = true;
	}
	private void OnDisable()
	{
	if (subscribedManager != null)
		subscribedManager.OnMusicChanged -= ApplyFromSettings;
	}
	private void ApplyFromSettings()
	{
		if (audioSource == null)
		{
			if (subscribedManager != null)
				subscribedManager.OnMusicChanged -= ApplyFromSettings;
			return;
		}
		audioSource.volume = SettingsManager.Instance.musicVolume;
	}
	private void ApplyFromPrefs()
	{
		float v = PlayerPrefs.GetFloat(SettingsManager.MusicKey, 1f);
		audioSource.volume = v;
	}
}
