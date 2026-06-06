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

  [SerializeField] private float gameDuration = 60f;
  [SerializeField] private float initialTimeScale = 0.6f;
  [SerializeField] private float timeScaleIncrement = 0.2f;
  [SerializeField] private float maxTimeScale = 1f;

  private float timer;
  private float currentProgression;

  private enum GameState { Playing, Paused, Won, Lost }
  private GameState state = GameState.Playing;

  private void Awake()
  {
		if (Instance != null && Instance != this) { Destroy(gameObject); return; }
		Instance = this;

		currentProgression = initialTimeScale;
	}

	private void Start()
	{
		TimeScaleController.Instance.SetProgression(currentProgression);
		OnGameStarted?.Invoke();
	}

	private void OnEnable()
  {
    if (TiltManager.Instance != null)
      TiltManager.Instance.OnMaxTiltReached += LoseGame;
  }
  private void OnDisable()
  {
    if (TiltManager.Instance != null)
      TiltManager.Instance.OnMaxTiltReached -= LoseGame;
  }

  private void Update()
  {
    if (state != GameState.Playing) return;
    if (PauseManager.Instance != null && PauseManager.Instance.IsPaused) return;

		timer += Time.unscaledDeltaTime;
        
    int sec = Mathf.FloorToInt(timer);
    if(sec != Mathf.FloorToInt(timer - Time.unscaledDeltaTime))
    {
      OnTimeChanged?.Invoke(timer, gameDuration);
      OnViewersChanged?.Invoke(currentProgression);

			if (currentProgression < maxTimeScale)
			{
				currentProgression = Mathf.Min(currentProgression + timeScaleIncrement, maxTimeScale);
				TimeScaleController.Instance.SetProgression(currentProgression);
			}
		}

    if (timer >= gameDuration)
    {
      timer = gameDuration;
      OnTimeChanged?.Invoke(timer, gameDuration);
      WinGame(timer);
    }
  }

	private void FixedUpdate()
	{
		Debug.Log("Time.timeScale: " + Time.timeScale);
	}

	private void WinGame(float t)
  {
		if (state != GameState.Playing) return;
		state = GameState.Won;
		TimeScaleController.Instance.SetFrozen();
		OnGameEnded?.Invoke();
		int total = ScoreManager.Instance != null ? ScoreManager.Instance.Total : 0;
		OnWin?.Invoke(total, t, gameDuration);
	}
  private void LoseGame()
  {
		if (state != GameState.Playing) return;
		state = GameState.Lost;
		TimeScaleController.Instance.SetFrozen();
		OnGameEnded?.Invoke();
		int total = ScoreManager.Instance != null ? ScoreManager.Instance.Total : 0;
		OnLose?.Invoke(total, timer, gameDuration);
	}
  public void RestartGame()
  {
    Time.timeScale = 1f;
    var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
  }
}