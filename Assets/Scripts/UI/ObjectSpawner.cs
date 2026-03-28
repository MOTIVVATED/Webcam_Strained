using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ObjectSpawner : MonoBehaviour
{
	public GameObject prefab;

	[SerializeField] private Transform parentCanvas;

	[SerializeField] private float minX = -2f;
	[SerializeField] private float maxX = 2f;
	[SerializeField] private float minY = -2f;
	[SerializeField] private float maxY = 2f;

	public static ObjectSpawner Instance;

	//[SerializeField] private GameObject overlayPanel;
	//[SerializeField] private Image overlayImage;

	private void Awake()
	{
		Instance = this;
		// overlayPanel.SetActive(false);
	}

	private void SpawnPopUp()
	{
		GameObject popUp = Instantiate(prefab);

		float randomX = Random.Range(minX, maxX);
		float randomY = Random.Range(minY, maxY);

		popUp.GetComponent<RectTransform>().anchoredPosition = new Vector3(randomX, randomY, 0f);
	}

	//public void SpawnPopUp()
	//{
	//	GameObject pupup = Instantiate(overlayPanel, parentCanvas);

	//	float randomX = Random.Range(minX, maxX);
	//	float randomY = Random.Range(minY, maxY);
		
	//	pupup.GetComponent<RectTransform>().anchoredPosition = new Vector2(randomX, randomY);
	//}

	//public void Show()
	//{
	//	float randomX = Random.Range(minX, maxX);
	//	float randomY = Random.Range(minY, maxY);
	//	overlayImage.transform.position = new Vector2(randomX, randomY);
	//	overlayPanel.SetActive(true);
	//}



	//public void Show(Sprite image)
	//{
	//	overlayImage.sprite = image;
	//	overlayPanel.SetActive(true);
	//}
	public void OnPlayerTap()
	{
		Destroy(prefab);
	}
}
