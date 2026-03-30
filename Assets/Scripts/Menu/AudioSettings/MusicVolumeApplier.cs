using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicVolumeApplier : MonoBehaviour
{
	private AudioSource audioSource;
	private bool subscribed;

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

		SettingsManager.Instance.OnMusicChanged += ApplyFromSettings;
		subscribed = true;
	}
	private void OnDisable()
	{
	if (SettingsManager.Instance != null)
		SettingsManager.Instance.OnMusicChanged -= ApplyFromSettings;
	}
	private void ApplyFromSettings()
	{
		audioSource.volume = SettingsManager.Instance.musicVolume;
	}
	private void ApplyFromPrefs()
	{
		float v = PlayerPrefs.GetFloat(SettingsManager.MusicKey, 1f);
		audioSource.volume = v;
	}
}
