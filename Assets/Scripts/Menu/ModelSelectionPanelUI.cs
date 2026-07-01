using UnityEngine;
using UnityEngine.SceneManagement;

public class ModelSelectionPanelUI : MonoBehaviour
{
	[SerializeField] private GameObject panelRoot;
	[SerializeField] private LevelingPanelUI levelingPanel;

	//[Header ("Buttons")]
	//[SerializeField] private GameObject[] buttons;

	//[Header ("Scene Name")]
	//[SerializeField] private string[] gameScenes;

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
}