using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupInstance : MonoBehaviour
{
	[SerializeField] private Button closeButton;
	[SerializeField] private Image popupImage;
	[SerializeField] private Text popupText;

	[SerializeField] private Sprite[] possibleSprites;

	[SerializeField] private float minX = -200f;
	[SerializeField] private float maxX = 200f;
	[SerializeField] private float minY = -200f;
	[SerializeField] private float maxY = 200f;

	private void Start()
	{
		float randomX = Random.Range(minX, maxX);
		float randomY = Random.Range(minY, maxY);
		GetComponent<RectTransform>().anchoredPosition = new Vector2(randomX, randomY);

		if (possibleSprites.Length > 0 )
			popupImage.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];

		TextAsset textFile = Resources.Load<TextAsset>("popupTexts");
		if ( textFile != null)
		{
			string[] lines = textFile.text.Split('\n');
			if (lines.Length > 0)
				popupText.text = lines[Random.Range(0, lines.Length)].Trim();
		}

		closeButton.onClick.AddListener(Close);
	}
	private void Close()
	{
		Destroy(gameObject);
	}
}
