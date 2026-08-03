using UnityEngine;
using UnityEngine.SceneManagement;

public class ModelSelectionPanelUI : MonoBehaviour
{
	[SerializeField] private GameObject panelRoot;
	[SerializeField] private LevelingPanelUI levelingPanel;
	[SerializeField] private GameObject infoButton;

	public void Close()
	{
		panelRoot.SetActive(false);
	}
	public void Play(GameObject button)
	{
		string sceneName = button.name;

		bool unlocked = PlayerProfileManager.Instance != null
			&& PlayerProfileManager.Instance.GetProfile().IsSceneUnlocked(sceneName);

		if (!unlocked)
		{
			// TODO: locked-state feedback (sound, shake, popup) once that UX is designed.
			Debug.Log($"ModelSelectionPanelUI: '{sceneName}' is locked, ignoring click.");
			return;
		}

		SceneManager.LoadScene(sceneName);
	}
	public void OpenLevelingPanel()
	{
		levelingPanel.Open();
	}

	public void OnInfoButtonClicked()
	{
		infoButton.SetActive(false);

		if (GuideManager.Instance == null)
		{
			OnGuideFinished();
			return;
		}

		GuideManager.Instance.ShowModelSelectionGuide(OnGuideFinished);
	}

	private void OnGuideFinished()
	{
		infoButton.SetActive(true);
	}
}