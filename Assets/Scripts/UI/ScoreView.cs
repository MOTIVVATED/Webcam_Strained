using TMPro;
using UnityEngine;
using System.Collections;

public class ScoreView : MonoBehaviour
{
	[SerializeField] private TMP_Text scoreText;

	private void Start()
	{
		StartCoroutine(WaitForInstance.Get(
				() => ScoreManager.Instance,
				sm =>
				{
					sm.OnScoreChanged += UpdateScore;
					UpdateScore(sm.Total);
				}
		));
	}

	private void OnDestroy()
	{
		if (ScoreManager.Instance != null)
			ScoreManager.Instance.OnScoreChanged -= UpdateScore;
	}

	private void UpdateScore(int score) => scoreText.text = score + "tk";
}