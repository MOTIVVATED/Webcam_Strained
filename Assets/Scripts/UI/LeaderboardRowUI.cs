using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardRowUI : MonoBehaviour
{
	[SerializeField] private Image background;
	[SerializeField] private TMP_Text rankText;
	[SerializeField] private TMP_Text nameText;
	[SerializeField] private TMP_Text scoreText;

	[SerializeField] private Color localPlayerColor = new Color(1f, 0.85f, 0.3f, 0.4f);
	[SerializeField] private Color defaultColor = new Color(0, 0, 0, 0.15f);

	public void Setup(LeaderboardEntryData data)
	{
		rankText.text = data.rank.ToString();
		nameText.text = data.playerName;
		scoreText.text = data.score.ToString();
		background.color = data.isLocalPlayer ? localPlayerColor : defaultColor;
	}
}
