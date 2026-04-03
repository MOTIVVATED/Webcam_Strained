using UnityEngine;
using UnityEngine.SceneManagement;

public class ModelSelectionPanelUI : MonoBehaviour
{
	[SerializeField] private GameObject panelRoot;

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
		SceneManager.LoadScene(sceneName);
	}
}
