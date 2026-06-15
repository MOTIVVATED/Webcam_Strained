using System.Runtime.InteropServices;
using UnityEngine;

public class LogosFeedback : MonoBehaviour
{
	public static LogosFeedback Instance { get; private set; }

	[Header("SpawnPoints")]
	[SerializeField] private GameObject[] logos;

	[SerializeField] private float punchScale = 1.1f;
	[SerializeField] private float duration = 0.1f;

	private Vector3 originalScale;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;

		if (logos != null && logos.Length > 0)
		{
			originalScale = logos[0].transform.localScale;
		}
	}

	private void OnEnable()
	{
		GameEvents.OnObjectCollected += SelectRow;
	}

	private void OnDisable()
	{
		GameEvents.OnObjectCollected -= SelectRow;
	}

	private void SelectRow (FallingObjectType type, Vector3 pos)
	{
		Debug.Log("SelectRow Called with pos.x = " + pos.x + " type = " + type);
		switch (pos.x)
		{
			case -3:
				StartCoroutine(Punch(logos[0]));
					break;
			case -2:
				StartCoroutine(Punch(logos[1]));
				break;
			case -1:
				StartCoroutine(Punch(logos[2]));
				break;
			case 0:
				StartCoroutine(Punch(logos[3]));
				break;
			case 1:
				StartCoroutine(Punch(logos[4]));
				break;
			case 2:
				StartCoroutine(Punch(logos[5]));
				break;
			case 3:
				StartCoroutine(Punch(logos[6]));
				break;
		}
	}

	private System.Collections.IEnumerator Punch(GameObject logo)
	{
		logo.transform.localScale = originalScale * punchScale;

		Vector3 currentZpos = logo.transform.localPosition;

		currentZpos.z = -1f;

		logo.transform.localPosition = currentZpos;

		yield return new WaitForSeconds(duration);

		logo.transform.localScale = originalScale;

		currentZpos.z = 0f;

		logo.transform.localPosition = currentZpos;
	}
}
