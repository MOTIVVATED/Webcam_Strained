using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    public static TimerView Instance { get; private set; }

    [SerializeField] private TMP_Text timerText;
    [SerializeField] private int secondsInHour = 30;
    
    public float SecondsInHour => secondsInHour;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(Init());

        if (secondsInHour == 0)
        {
            Debug.LogWarning("Seconds in hour is set to 0. Defaulting to 30.");
            secondsInHour = 20;
        }
    }
    private IEnumerator Init()
    {
        while (GameManager.Instance == null)
        {
            yield return null;
        }
        GameManager.Instance.OnTimeChanged += UpdateTimer;
        UpdateTimer(GameManager.Instance.Timer, GameManager.Instance.GameDuration);
    }
    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnTimeChanged += UpdateTimer;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnTimeChanged -= UpdateTimer;
    }

    private void UpdateTimer(float elapsed, float duration)
    {
        int e = Mathf.FloorToInt(elapsed)/secondsInHour;
        int d = Mathf.FloorToInt(duration)/secondsInHour;
        timerText.text = $"{e}/{d}h";
    }
}
