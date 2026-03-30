using UnityEngine;
public class DistractionOverlay : MonoBehaviour
{
	public static DistractionOverlay Instance;

	[SerializeField] private GameObject popupPrefab;
	[SerializeField] private Transform parentCanvas;

	private void Awake()
	{
		Instance = this;
	}
	public void Show()
	{
		Instantiate(popupPrefab, parentCanvas);
	}
}
