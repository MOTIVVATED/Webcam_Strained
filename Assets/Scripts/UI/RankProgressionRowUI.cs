using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankProgressionRowUI : MonoBehaviour
{
	[SerializeField] private TMP_Text rankLabelText;
	[SerializeField] private TMP_Text summaryText;
	[SerializeField] private Image background;

	[SerializeField] private Color currentRankColor = new Color(0.9f, 0.8f, 0.2f, 0.5f);
	[SerializeField] private Color defaultColor = new Color(0, 0, 0, 0);

	public void Setup(int rankNumber, string summary)
	{
		if (rankLabelText != null) rankLabelText.text = $"Rank {rankNumber}";
		if (summaryText != null) summaryText.text = summary;
	}

	public void SetCurrent(bool isCurrent)
	{
		if (background != null)
			background.color = isCurrent ? currentRankColor : defaultColor;
	}
}