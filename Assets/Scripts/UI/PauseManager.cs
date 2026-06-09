using Unity.VisualScripting.Antlr3.Runtime;
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
		if (Instance != null && Instance != this) { Destroy(gameObject); return; }
		Instance = this;

		IsPaused = false;
		if (pauseMenuPanel != null)
			pauseMenuPanel.SetActive(false);
	}

	private void OnDestroy()
	{
		if (Instance == this) Instance = null;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			TryTogglePause();
	}

	private bool CanPause() =>
		GameManager.Instance != null && GameManager.Instance.IsPlaying;


	public void TryTogglePause()
	{
		if (!CanPause()) return;
		SetPaused(!IsPaused);
	}

	public void SetPaused(bool paused)
	{
		IsPaused = paused;
		TimeScaleController.Instance.SetPaused(paused);

		if (pauseMenuPanel != null)
			pauseMenuPanel.SetActive(paused);
	}

	public void Resume() => SetPaused(false);

	public void Restart()
	{
		SetPaused(false);
		if (GameManager.Instance != null)
			GameManager.Instance.RestartGame();
		else
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		
		if (pauseMenuPanel != null)
			pauseMenuPanel.SetActive(false);
	}
}
