using System;
using TMPro;
using UnityEngine;

public class PowerUpCounterView : MonoBehaviour
{
	[SerializeField] private TMP_Text countText;
	[SerializeField] private GameObject image;

	private void Start()
	{
		if (PowerUpInventory.Instance != null)
			Refresh(PowerUpInventory.Instance.Charges);
		else
			Debug.LogWarning("PowerUpCounterView: No PowerUpInventory instance found in scene.");
		
		if (PowerUpInventory.Instance != null)
			PowerUpInventory.Instance.OnChargesChanged += Refresh;
	}

	//private void OnEnable()
	//{
	//	if (PowerUpInventory.Instance != null)
	//		PowerUpInventory.Instance.OnChargesChanged += Refresh;
	//}

	private void OnDisable()
	{
		if (PowerUpInventory.Instance != null)
			PowerUpInventory.Instance.OnChargesChanged -= Refresh;
	}

	private void Refresh(int charges)
	{
		Debug.Log("refresh called");
		if (countText == null) return;

		if (charges <= 0)
		{
			countText.gameObject.SetActive(false);
			image.gameObject.SetActive(false);
			return;
		}

		countText.gameObject.SetActive(true);
		image.gameObject.SetActive(true);
		// countText.text = $"x{charges}";
		countText.text = charges.ToString();
	}
}