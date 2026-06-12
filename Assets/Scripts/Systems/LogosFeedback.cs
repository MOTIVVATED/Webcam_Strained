using UnityEngine;

public class LogosFeedback : MonoBehaviour
{
	[Header("SpawnPoints")]
	[SerializeField] private GameObject[] logos;

	[SerializeField] private Transform player;
	[SerializeField] private float punchScale = 1.1f;
	[SerializeField] private float duration = 0.1f;

	private Vector3 originalScale;

	private void Awake()
	{
		if (logos != null && logos.Length > 0)
		{
			originalScale = logos[0].transform.localScale;
		}
	}

	private void Start()
	{
		StartCoroutine(Init());
	}

	private System.Collections.IEnumerator Init()
	{
		while (ScoreManager.Instance == null)
			yield return null;

		ScoreManager.Instance.OnScoreChanged += OnGood;
	}

	private void OnDestroy()
	{
		if (ScoreManager.Instance != null)
			ScoreManager.Instance.OnScoreChanged -= OnGood;
	}

	private void OnGood(int _)
	{
		StopAllCoroutines();
		SelectRow(player.position);
	}

	private void SelectRow (Vector3 player)
	{
		switch (player.x)
		{
			case >= -3.5f and < -2.5f :
				StartCoroutine(Punch(logos[0]));
					break;
			case >= -2.5f and < -1.5f :
				StartCoroutine(Punch(logos[1]));
				break;
			case >= -1.5f and < -0.5f:
				StartCoroutine(Punch(logos[2]));
				break;
			case >= -0.5f and < 0.5f:
				StartCoroutine(Punch(logos[3]));
				break;
			case >= 0.5f and < 1.5f:
				StartCoroutine(Punch(logos[4]));
				break;
			case >= 1.5f and < 2.5f:
				StartCoroutine(Punch(logos[5]));
				break;
			case >= 2.5f and < 3.5f:
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
