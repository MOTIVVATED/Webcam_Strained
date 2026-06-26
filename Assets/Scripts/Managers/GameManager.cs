using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public event Action<int, float, float> OnWin;
	public event Action<int, float, float> OnLose;
	public event Action OnGameStarted;
	public event Action OnGameEnded;
	public event Action<float, float> OnTimeChanged;
	public event Action<float> OnViewersChanged;

	public bool IsPlaying => state == GameState.Playing;
	public float Timer => timer;
	public float GameDuration => gameDuration;

	[Header("Fallback / Tuning")]
	[Tooltip("Used only if no music clip length is available yet.")]
	[SerializeField] private float fallbackGameDuration = 60f;

	[Tooltip("Minimum allowed round duration, in case a clip is very short.")]
	[SerializeField] private float minGameDuration = 10f;

	[SerializeField] private MusicPlaylist musicPlaylist;

	private float gameDuration;
	private float timer;
	private bool roundConfigured = false;

	private enum GameState { Playing, Paused, Won, Lost }
	private GameState state = GameState.Playing;

	private void Awake()
	{
		if (Instance != null && Instance != this) { Destroy(gameObject); return; }
		Instance = this;

		gameDuration = fallbackGameDuration;
	}

	private void Start()
	{
		if (musicPlaylist != null)
			musicPlaylist.StartRound();
		else
			Debug.LogWarning("GameManager: No MusicPlaylist assigned, falling back to fixed duration.");
	}

	private void OnEnable()
	{
		if (TiltManager.Instance != null)
			TiltManager.Instance.OnMaxTiltReached += LoseGame;

		GameEvents.OnMusicClipSelected += HandleMusicClipSelected;
	}

	private void OnDisable()
	{
		if (TiltManager.Instance != null)
			TiltManager.Instance.OnMaxTiltReached -= LoseGame;

		GameEvents.OnMusicClipSelected -= HandleMusicClipSelected;
	}

	private void HandleMusicClipSelected(float clipLength, float buffer)
	{
		gameDuration = Mathf.Max(minGameDuration, clipLength - buffer);
		roundConfigured = true;

		TimeScaleController.Instance.SetTimeScale();
		OnGameStarted?.Invoke();
	}

	private void Update()
	{
		if (!roundConfigured) return;
		if (state != GameState.Playing) return;
		if (PauseManager.Instance != null && PauseManager.Instance.IsPaused) return;

		timer += Time.unscaledDeltaTime;

		int sec = Mathf.FloorToInt(timer);
		if (sec != Mathf.FloorToInt(timer - Time.unscaledDeltaTime))
		{
			OnTimeChanged?.Invoke(timer, gameDuration);
			OnViewersChanged?.Invoke(Time.timeScale);

			TimeScaleController.Instance.SetTimeScale();
		}

		if (timer >= gameDuration)
		{
			timer = gameDuration;
			OnTimeChanged?.Invoke(timer, gameDuration);
			WinGame(timer);
		}
	}

	private void WinGame(float t)
	{
		if (state != GameState.Playing) return;
		state = GameState.Won;
		Debug.Log("WinGame");
		TimeScaleController.Instance.SetFrozen();
		OnGameEnded?.Invoke();
		int total = ScoreManager.Instance != null ? ScoreManager.Instance.Total : 0;
		RecordRoundResult(total, GameOutcome.Win, unlockNext: true);
		OnWin?.Invoke(total, t, gameDuration);
	}

	private void LoseGame()
	{
		if (state != GameState.Playing) return;
		state = GameState.Lost;
		TimeScaleController.Instance.SetFrozen();
		OnGameEnded?.Invoke();
		int total = ScoreManager.Instance != null ? ScoreManager.Instance.Total : 0;
		RecordRoundResult(total, GameOutcome.Lose, unlockNext: false);
		OnLose?.Invoke(total, timer, gameDuration);
	}

	private void RecordRoundResult(int score, GameOutcome outcome, bool unlockNext)
	{
		string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

		if (GameResultsManager.Instance != null)
			GameResultsManager.Instance.SaveResult(score, outcome);
		else
			Debug.LogWarning("GameManager: No GameResultsManager instance found, result not saved.");

		if (PlayerProfileManager.Instance != null)
			PlayerProfileManager.Instance.RecordGame(score, sceneName, unlockNext);
		else
			Debug.LogWarning("GameManager: No PlayerProfileManager instance found, profile not updated.");
	}

	public void RestartGame()
	{
		TimeScaleController.Instance.Unfreeze();
		var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
	}
}