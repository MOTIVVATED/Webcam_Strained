using UnityEngine;
using UnityEngine.UI;
public class DistractionOverlay : MonoBehaviour
{
	public static DistractionOverlay Instance;

	[SerializeField] private GameObject overlayPanel;
	[SerializeField] private Image overlayImage;
	[SerializeField] private Transform parentCanvas;

	[SerializeField] private float minX = -2f;
	[SerializeField] private float maxX = 2f;
	[SerializeField] private float minY = -2f;
	[SerializeField] private float maxY = 2f;

	private void Awake()
	{
		Instance = this;
		overlayPanel.SetActive(false);
	}
	public void Show()
	{
		float randomX = Random.Range(minX, maxX);
		float randomY = Random.Range(minY, maxY);
		overlayImage.rectTransform.anchoredPosition = new Vector2(randomX, randomY);
		overlayPanel.SetActive(true);
	}
	public void OnPlayerTap()
	{
		overlayPanel.SetActive(false);
	}
}
