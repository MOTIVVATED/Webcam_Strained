using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	[SerializeField] private string gameSceneName = "Game";
	[SerializeField] private GameObject ModelSelectionPanel;

	public void Start()
	{
		Time.timeScale = 0.4f;
	}
	public void Play()
	{
		Time.timeScale = 0.4f;
		SceneManager.LoadScene(gameSceneName);
	}
	public void SelectModel()
	{
		ModelSelectionPanel.SetActive(true);
	}
	public void Quit()
	{
		Application.Quit();
		Debug.Log("Quit");
	}
}
