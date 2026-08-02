using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	[SerializeField] private string gameSceneName = "Game";
	[SerializeField] private GameObject ModelSelectionPanel;
	[SerializeField] private LevelingPanelUI levelingPanel;
	[SerializeField] private LeaderboardsPanelUI leaderboardsPanel;

	public void Start()
	{
		//Time.timeScale = 0.4f;
		GuideManager.Instance?.ShowGameLaunchGuide();
	}
	public void Play()
	{
		//Time.timeScale = 0.4f;
		SceneManager.LoadScene(gameSceneName);
	}
	public void SelectModel()
	{
		ModelSelectionPanel.SetActive(true);
		GuideManager.Instance?.OnModelSelectionPanelOpened();
	}
	public void OpenLevelingPanel()
	{
		levelingPanel.Open();
	}
	public void OpenLeaderboardsPanel()
	{
		leaderboardsPanel.Open();
	}
	public void Quit()
	{
		Application.Quit();
		Debug.Log("Quit");
	}
}