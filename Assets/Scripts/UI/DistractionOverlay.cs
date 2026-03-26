using UnityEngine;
using UnityEngine.UI;
public class DistractionOverlay : MonoBehaviour
{
	public static DistractionOverlay Instance;

	[SerializeField] private GameObject overlayPanel;
	[SerializeField] private Image overlayImage;

	private void Awake()
	{
		Instance = this;
		overlayPanel.SetActive(false);
	}

	public void Show()
	{
		overlayPanel.SetActive(true);
	}

	//public void Show(Sprite image)
	//{
	//	overlayImage.sprite = image;
	//	overlayPanel.SetActive(true);
	//}
	public void OnPlayerTap()
	{
		overlayPanel.SetActive(false);
	}
}
