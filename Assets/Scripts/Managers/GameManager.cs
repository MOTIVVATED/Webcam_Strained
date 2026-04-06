using System;
using UnityEngine;
using UnityEngine.Android;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; }

  public event Action<int, float, float> OnWin;
  public event Action<int, float, float> OnLose;

  public event Action OnGameStarted;
  public event Action OnGameEnded;

  public bool IsPlaying => state == GameState.playing;

  [SerializeField] private float gameDuration = 60f;

  [SerializeField] private float initialTimeScale = 0.6f;
  [SerializeField] private float timeScaleIncrement = 0.2f;
  [SerializeField] private float maxTimeScale = 1f;

  public event Action<float, float> OnTimeChanged;

  public event Action<float> OnViewersChanged;

  private float timer;

  private GameState state = GameState.playing;

  public float Timer => timer;
  public float GameDuration => gameDuration;

  private enum GameState
  {
    playing,
    paused,
    won,
    lost
  }
  private void Awake()
  {
    Time.timeScale = initialTimeScale;

    OnGameStarted?.Invoke();

    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }
    Instance = this;
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
    if (state != GameState.playing) return;

    timer += Time.deltaTime;

    // this is to make sure the time and viewers are updated only when the seconds change
    // not every frame
        
    int sec = Mathf.FloorToInt(timer);

    if(sec != Mathf.FloorToInt(timer - Time.deltaTime))
    {
      OnTimeChanged?.Invoke(timer, gameDuration);
      OnViewersChanged?.Invoke(Time.timeScale);
      if (Time.timeScale < maxTimeScale)
      Time.timeScale += timeScaleIncrement;
    }

    if (timer >= gameDuration)
    {
      timer = gameDuration;

      OnTimeChanged?.Invoke(timer, gameDuration);
            
      WinGame(timer);
    }
  }
  private void WinGame(float timer)
  {
    if (state != GameState.playing) return;

    state = GameState.won;        
    Time.timeScale = 0f;

    OnGameEnded?.Invoke();

    Debug.Log("the time is out! you won!");

    int total = ScoreManager.Instance != null 
    ? ScoreManager.Instance.Total : 0;

    OnWin?.Invoke(total, timer, gameDuration);
  }
  private void LoseGame()
  {
    if (state != GameState.playing) return;
        
    state = GameState.lost;
    Time.timeScale = 0f;

    OnGameEnded?.Invoke();

    Debug.Log("the tilt level is max! you lost! 8===D");

    int total = ScoreManager.Instance != null
    ? ScoreManager.Instance.Total : 0;

    OnLose?.Invoke(total, timer, gameDuration);
  }
  public void RestartGame()
  {
    Time.timeScale = 1f;
    var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
  }
}


