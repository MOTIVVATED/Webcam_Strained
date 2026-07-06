using TMPro;
using UnityEngine;

public class ViewersView : MonoBehaviour
{
	[SerializeField] private TMP_Text viewersText;
	[SerializeField] private int viewersPerTimescale = 10;


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
		int viewers = Mathf.FloorToInt(viewersPerTimescale * timeScale) + Random.Range(-5, 5);
		viewersText.text = viewers.ToString();
		// Debug.Log("TimeScale : Viewers: " + timeScale + " : " + viewers);
	}
}