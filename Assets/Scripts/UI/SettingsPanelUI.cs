using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelUI : MonoBehaviour
{
	[Header("Root")]
	[SerializeField] private GameObject panelRoot;

	[Header("UI")]
	[SerializeField] private Slider musicSlider;
	[SerializeField] private Slider sfxSlider;
	[SerializeField] private Toggle darkModeToggle;

	private bool ignoreEvents;

	private void OnEnable()
	{
		SyncFromSettings();
	}
	private void Awake()
	{
		musicSlider.onValueChanged.RemoveAllListeners();
		musicSlider.onValueChanged.AddListener(OnMusicChanged);

		sfxSlider.onValueChanged.RemoveAllListeners();
		sfxSlider.onValueChanged.AddListener(OnSfxChanged);

		if (darkModeToggle != null)
		{
			darkModeToggle.onValueChanged.RemoveAllListeners();
			darkModeToggle.onValueChanged.AddListener(OnDarkModeChanged);
		}
	}
	public void Open()
	{
		panelRoot.SetActive(true);
		SyncFromSettings();
	}
	public void Close()
	{
		panelRoot.SetActive(false);
	}
	public void OnMusicChanged(float value)
	{
		if (ignoreEvents) return;
		SettingsManager.Instance.SetMusic(value);
	}
	public void OnSfxChanged(float value)
	{
		if (ignoreEvents) return;
		SettingsManager.Instance.SetSfx(value);
	}
	public void OnDarkModeChanged(bool on)
	{
		if (ignoreEvents) return;
		SettingsManager.Instance.SetDarkMode(on);
	}
	public void OnResetClicked()
	{
		SettingsManager.Instance.ResetToDefaults();
		SyncFromSettings();
	}
	private void SyncFromSettings()
	{
		if (SettingsManager.Instance == null) return;

		ignoreEvents = true;

		musicSlider.value = SettingsManager.Instance.musicVolume;
		sfxSlider.value = SettingsManager.Instance.sfxVolume;

		if (darkModeToggle != null)
		{
			darkModeToggle.isOn = SettingsManager.Instance.darkModeOn;
		}
		ignoreEvents = false;
	}
}
