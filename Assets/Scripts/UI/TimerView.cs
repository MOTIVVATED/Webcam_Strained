using System.Collections;
using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private int seconsdInHour = 20;

    public float SeconsdInHour => seconsdInHour;

    private void Start()
    {
        StartCoroutine(Init());
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
        int e = Mathf.FloorToInt(elapsed)/seconsdInHour;
        int d = Mathf.FloorToInt(duration)/seconsdInHour;
        timerText.text = $"{e}/{d}h";
    }
}
