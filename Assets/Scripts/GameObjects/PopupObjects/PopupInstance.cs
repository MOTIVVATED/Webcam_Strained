using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupInstance : MonoBehaviour
{
	[SerializeField] private Button closeButton;

	[SerializeField] private RectTransform memberContainer;
	[SerializeField] private GameObject[] possibleMembers;

	[SerializeField] private Text popupText;

	[SerializeField] private float minX = -200f;
	[SerializeField] private float maxX = 200f;
	[SerializeField] private float minY = -200f;
	[SerializeField] private float maxY = 200f;

	private void Start()
	{
		float randomX = Random.Range(minX, maxX);
		float randomY = Random.Range(minY, maxY);
		GetComponent<RectTransform>().anchoredPosition = new Vector2(randomX, randomY);

		if (possibleMembers.Length > 0 && memberContainer != null)
		{
			GameObject memberPrefab = possibleMembers[Random.Range(0, possibleMembers.Length)];
			GameObject memberInstance = Instantiate(memberPrefab, memberContainer);

			RectTransform memberRect = memberInstance.GetComponent<RectTransform>();
			if (memberRect != null)
			{
				memberRect.anchorMin = new Vector2(0.5f, 0.5f);
				memberRect.anchorMax = new Vector2(0.5f, 0.5f);
				memberRect.pivot = new Vector2(0.5f, 0.5f);
				memberRect.anchoredPosition = Vector2.zero;
				memberRect.sizeDelta = memberContainer.rect.size;
				memberRect.localScale = Vector3.one;
			}
		}

		TextAsset textFile = Resources.Load<TextAsset>("popupTexts");
		if (textFile != null)
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