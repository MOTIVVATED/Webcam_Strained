using TMPro;
using UnityEngine;
using System.Collections;

public class TimerView : MonoBehaviour
{
	public static TimerView Instance { get; private set; }

	[SerializeField] private TMP_Text timerText;
	[SerializeField] private int secondsInHour = 30;

	public float SecondsInHour => secondsInHour;

	private void Awake()
	{
		if (Instance != null && Instance != this) { Destroy(gameObject); return; }
		Instance = this;

		if (secondsInHour == 0)
		{
			Debug.LogWarning("TimerView: secondsInHour is 0, defaulting to 30.");
			secondsInHour = 30;
		}
	}

	private void Start()
	{
		StartCoroutine(WaitForInstance.Get(
				() => GameManager.Instance,
				gm =>
				{
					gm.OnTimeChanged += UpdateTimer;
					UpdateTimer(gm.Timer, gm.GameDuration);
				}
		));
	}

	private void OnDestroy()
	{
		if (GameManager.Instance != null)
			GameManager.Instance.OnTimeChanged -= UpdateTimer;
	}

	private void UpdateTimer(float elapsed, float duration)
	{
		int e = Mathf.FloorToInt(elapsed) / secondsInHour;
		int d = Mathf.FloorToInt(duration) / secondsInHour;
		timerText.text = $"{e}/{d}hrs";
	}
}