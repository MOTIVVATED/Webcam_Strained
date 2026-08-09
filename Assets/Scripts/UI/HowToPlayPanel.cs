using UnityEngine;

public class HowToPlayPanel : MonoBehaviour
{
	[SerializeField] private GameObject panelRoot;

	public void Open()
	{
		panelRoot.SetActive(true);
	}

	public void Close()
	{
		panelRoot.SetActive(false);
	}
}
