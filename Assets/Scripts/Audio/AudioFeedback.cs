using UnityEngine;

public class AudioFeedback : MonoBehaviour
{
	[Header("Sounds")]
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip ding;
	[SerializeField] private AudioClip tip;
	[SerializeField] private AudioClip medium;
	[SerializeField] private AudioClip large;
	[SerializeField] private AudioClip badSound;

	private void Start()
	{
		StartCoroutine(Init());
	}

	private System.Collections.IEnumerator Init()
	{
		while (ScoreManager.Instance == null || TiltManager.Instance == null)
		yield return null;

		ScoreManager.Instance.OnTK_1_15_Collected += () => audioSource.PlayOneShot(ding);
		ScoreManager.Instance.OnTK_25_Collected += () => audioSource.PlayOneShot(ding);
		
		ScoreManager.Instance.OnTK_111_Collected += () => audioSource.PlayOneShot(tip);
		ScoreManager.Instance.OnTK_222_Collected += () => audioSource.PlayOneShot(tip);
		
		ScoreManager.Instance.OnTK_555_Collected += () => audioSource.PlayOneShot(medium);
		ScoreManager.Instance.OnTK_666_Collected += () => audioSource.PlayOneShot(medium);
		
		ScoreManager.Instance.OnTK_1111_Collected += () => audioSource.PlayOneShot(large);

		TiltManager.Instance.OnTiltIncreased += OnBad;
	}

	private void OnDestroy()
	{
		if (ScoreManager.Instance != null)

		ScoreManager.Instance.OnTK_1_15_Collected -= () => audioSource.PlayOneShot(ding);
		ScoreManager.Instance.OnTK_25_Collected -= () => audioSource.PlayOneShot(ding);

		ScoreManager.Instance.OnTK_111_Collected -= () => audioSource.PlayOneShot(tip);
		ScoreManager.Instance.OnTK_222_Collected -= () => audioSource.PlayOneShot(tip);

		ScoreManager.Instance.OnTK_555_Collected -= () => audioSource.PlayOneShot(medium);
		ScoreManager.Instance.OnTK_666_Collected -= () => audioSource.PlayOneShot(medium);
		
		ScoreManager.Instance.OnTK_1111_Collected -= () => audioSource.PlayOneShot(large);

		if (TiltManager.Instance != null)
		TiltManager.Instance.OnTiltIncreased -= OnBad;
	}
	private void OnBad(int _)
	{
		audioSource.PlayOneShot(badSound);
	}
}
