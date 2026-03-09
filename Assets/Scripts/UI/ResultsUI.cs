using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;

public class ResultsUI : MonoBehaviour
{
    [SerializeField] private Text historyText; // Assign a UI Text component in the Inspector
    [SerializeField] private Text bestScoreText; // Assign another UI Text element

    void OnEnable()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        var results = GameResultsManager.Instance.GetAllResults();
        var best = GameResultsManager.Instance.GetBestScore();

        bestScoreText.text = best != null
            ? $"Best Score: {best.score} ({best.dateTime})"
            : "No games played yet.";

        if (results.Count == 0)
        {
            historyText.text = "No history yet.";
            return;
        }

        var sb = new System.Text.StringBuilder();
        // Show most recent 10
        int start = Mathf.Max(0, results.Count - 10);
        for (int i = results.Count - 1; i >= start; i--)
        {
            var r = results[i];
            sb.AppendLine($"{r.dateTime}] Score: {r.score}" +
                        $"Caught: {r.itemsCaught} Dodged: {r.itemsDodged}" +
                        $"Time: {r.timeSurvived:F1}s"); // last two strings I'll delete later :)
        }
        historyText.text = sb.ToString();
    }
}
