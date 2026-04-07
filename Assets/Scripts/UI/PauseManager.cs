using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
	public static PauseManager Instance { get; private set; }

	[Header("UI")]
	[SerializeField] private GameObject pauseMenuPanel;

	public bool IsPaused { get; private set; }

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;

		SetPaused(false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameManager.Instance != null && !GameManager.Instance.IsPlaying)
			return;

			TogglePause();
		}
	}

	public void TogglePause()
	{
		if (GameManager.Instance != null && !GameManager.Instance.IsPlaying)
		return;

		SetPaused(!IsPaused);
	}

	public void SetPaused(bool paused)
	{
		IsPaused = paused;

		if (pauseMenuPanel != null)
		pauseMenuPanel.SetActive(paused);

		Time.timeScale = paused ? 0f : 1f;
	}

	public void Resume()
	{
		SetPaused(false);
	}

	public void Restart()
	{
		Time.timeScale = 1f;

		if (GameManager.Instance != null)
		{
			GameManager.Instance.RestartGame();
			return;
		}

		//var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		//UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
	}
}
