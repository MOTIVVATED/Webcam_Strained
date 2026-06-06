using TMPro;
using UnityEngine;
using System.Collections;

public class ViewersView : MonoBehaviour
{
	[SerializeField] private TMP_Text viewersText;

	private void Start()
	{
		StartCoroutine(WaitForInstance.Get(
				() => GameManager.Instance,
				gm =>
				{
					gm.OnViewersChanged += UpdateViewers;
					UpdateViewers(Time.timeScale);
				}
		));
	}

	private void OnDestroy()
	{
		if (GameManager.Instance != null)
			GameManager.Instance.OnViewersChanged -= UpdateViewers;
	}

	private void UpdateViewers(float timeScale)
	{
		int viewers = Mathf.FloorToInt(300 * timeScale) + Random.Range(-5, 5);
		viewersText.text = viewers.ToString();
	}
}