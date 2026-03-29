using UnityEngine;
using UnityEngine.UI;

public class PopupInstance : MonoBehaviour
{
	[SerializeField] private Button closeButton;

	[SerializeField] private float minX = -200f;
	[SerializeField] private float maxX = 200f;
	[SerializeField] private float minY = -200f;
	[SerializeField] private float maxY = 200f;

	private void Start()
	{
		float randomX = Random.Range(minX, maxX);
		float randomY = Random.Range(minY, maxY);

		GetComponent<RectTransform>().anchoredPosition = new Vector2(randomX, randomY);

		closeButton.onClick.AddListener(Close);
	}
	private void Close()
	{
		Destroy(gameObject);
	}
}
