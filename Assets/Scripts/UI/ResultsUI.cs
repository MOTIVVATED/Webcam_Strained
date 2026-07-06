using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsUI : MonoBehaviour
{
	[SerializeField] private Text historyText; // Assign a UI Text component in the Inspector
	[SerializeField] private Text bestScoreText; // Assign another UI Text element

	[SerializeField] private TMP_Text newHistoryText;
	[SerializeField] private TMP_Text newTimestampText;
	[SerializeField] private TMP_Text newBestScoreText;

	[Header("Root")]
	[SerializeField] private GameObject panelRoot;

	void OnEnable()
	{
		StartCoroutine(WaitAndRefresh());
	}

	private IEnumerator WaitAndRefresh()
	{
		// Wait a frame to ensure GameResultsManager has saved the latest result
		yield return new WaitUntil(() => GameResultsManager.Instance != null);
		RefreshUI();
	}

	public void RefreshUI()
	{
		var results = GameResultsManager.Instance.GetAllResults();
		var best = GameResultsManager.Instance.GetBestScore();

		bestScoreText.text = best != null
		? $"Best Score: {best.score} \n{best.dateTime}"
		: "No games played yet.";

		newBestScoreText.text = best != null
		? $"Best Score: {best.score} \n{best.dateTime}"
		: "No games played yet.";

		if (results.Count == 0)
		{
			historyText.text = "No history yet.";
			newHistoryText.text = "No history yet.";
			return;
		}

		var sbHistory = new StringBuilder();
		var sbTimeStamp = new StringBuilder();

		int start = Mathf.Max(0, results.Count - 10);
		for (int i = results.Count - 1; i >= start; i--)
		{
			var r = results[i];

			sbHistory.AppendLine($"Total: {r.score}");
			sbTimeStamp.AppendLine(r.dateTime);

			//sb.AppendLine($"Score: {r.score}    {r.dateTime}");
			//
			//+$"Caught: {r.itemsCaught} Dodged: {r.itemsDodged}" +
			//$"Time: {r.timeSurvived:F1}s"); // last two strings I'll delete later :)
		}
		historyText.text = sbHistory.ToString();
		newHistoryText.text = sbHistory.ToString();
		newTimestampText.text = sbTimeStamp.ToString();
	}
	public void Open()
	{
		panelRoot.SetActive(true);
	}
	public void Close()
	{
		panelRoot.SetActive(false);
	}
}
