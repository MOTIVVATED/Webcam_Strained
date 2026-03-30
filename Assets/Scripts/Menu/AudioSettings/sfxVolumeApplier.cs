using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class sfxVolumeApplier : MonoBehaviour
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

		SettingsManager.Instance.OnSfxChanged += ApplyFromSettings;
		subscribed = true;
	}
	private void OnDisable()
	{
		if (SettingsManager.Instance != null)
		SettingsManager.Instance.OnSfxChanged -= ApplyFromSettings;
	}
	private void ApplyFromSettings()
	{
		audioSource.volume = SettingsManager.Instance.sfxVolume;
	}
	private void ApplyFromPrefs()
	{
		float v = PlayerPrefs.GetFloat(SettingsManager.SfxKey, 1f);
		audioSource.volume = v;
	}
}
