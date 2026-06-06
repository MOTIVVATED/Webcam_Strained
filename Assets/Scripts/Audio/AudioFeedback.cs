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

		ScoreManager.Instance.OnTK_1_15_Collected += PlayDing;
		ScoreManager.Instance.OnTK_25_Collected += PlayDing;
		ScoreManager.Instance.OnTK_111_Collected += PlayTip;
		ScoreManager.Instance.OnTK_222_Collected += PlayTip;
		ScoreManager.Instance.OnTK_555_Collected += PlayMedium;
		ScoreManager.Instance.OnTK_666_Collected += PlayMedium;
		ScoreManager.Instance.OnTK_1111_Collected += PlayLarge;
		TiltManager.Instance.OnTiltIncreased += OnBad;
	}

	private void OnDestroy()
	{
		if (ScoreManager.Instance != null)
		{
			ScoreManager.Instance.OnTK_1_15_Collected -= PlayDing;
			ScoreManager.Instance.OnTK_25_Collected -= PlayDing;
			ScoreManager.Instance.OnTK_111_Collected -= PlayTip;
			ScoreManager.Instance.OnTK_222_Collected -= PlayTip;
			ScoreManager.Instance.OnTK_555_Collected -= PlayMedium;
			ScoreManager.Instance.OnTK_666_Collected -= PlayMedium;
			ScoreManager.Instance.OnTK_1111_Collected -= PlayLarge;
		}

		if (TiltManager.Instance != null)
			TiltManager.Instance.OnTiltIncreased -= OnBad;
	}
	private void PlayDing() => audioSource.PlayOneShot(ding);
	private void PlayTip() => audioSource.PlayOneShot(tip);
	private void PlayMedium() => audioSource.PlayOneShot(medium);
	private void PlayLarge() => audioSource.PlayOneShot(large);

	private void OnBad(int _) => audioSource.PlayOneShot(badSound);
}
