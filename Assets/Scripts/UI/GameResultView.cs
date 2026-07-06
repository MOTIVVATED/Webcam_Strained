using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultView : MonoBehaviour
{
	[SerializeField] private TMP_Text resultText;

	[SerializeField] private TMP_Text nameText;
	[SerializeField] private TMP_Text rankText;
	[SerializeField] private TMP_Text moneyText;
	[SerializeField] private TMP_Text avgScoreText;

	[SerializeField] private GameObject restartLabels;

	void Start()
	{
		resultText.gameObject.SetActive(false);

		GameManager.Instance.OnWin += ShowWin;
		GameManager.Instance.OnLose += ShowLose;

		restartLabels.SetActive(false);
	}
	private void OnDestroy()
	{
		if (GameManager.Instance == null) return;

		GameManager.Instance.OnWin -= ShowWin;

		GameManager.Instance.OnLose -= ShowLose;
	}
	private void ShowWin(int total, float timer, float duration)
	{
		if (PlayerProfileManager.Instance == null)
		{
			Debug.LogError("PlayerProfileManager not found in scene.");
			return;
		}

		var profile = PlayerProfileManager.Instance.GetProfile();

		int t = (int)(timer / TimerView.Instance.SecondsInHour);
		int d = (int)(duration / TimerView.Instance.SecondsInHour);

		resultText.text =
		$"W W! \ntotal: {total}tk \nstream time: {t}/{d}h";

		nameText.text = profile.playerName;
		rankText.text = profile.GetRank();
		moneyText.text = $"${profile.money}";
		avgScoreText.text = profile.GetAverageScore().ToString();

		resultText.gameObject.SetActive(true);

		restartLabels.SetActive(true);
	}
	private void ShowLose(int total, float timer, float duration)
	{
		if (PlayerProfileManager.Instance == null)
		{
			Debug.LogError("PlayerProfileManager not found in scene.");
			return;
		}

		var profile = PlayerProfileManager.Instance.GetProfile();

		int t = (int)(timer / TimerView.Instance.SecondsInHour);
		int d = (int)(duration / TimerView.Instance.SecondsInHour);

		resultText.text =
			$"Tilt! \ntotal: {total}tk | stream time: {t}/{d}h";

		nameText.text = $"name: {profile.playerName}";
		rankText.text = $"rank: {profile.GetRank()}";
		moneyText.text = $"net worth: {profile.money}$";
		avgScoreText.text = $"avg Total: {profile.GetAverageScore().ToString()}";

		resultText.gameObject.SetActive(true);

		restartLabels.SetActive(true);
	}
	public void GoToMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainMenu");
	}
	public void Quit()
	{
		Application.Quit();
		Debug.Log("Quit");
	}

	public void Restart()
	{
		restartLabels.SetActive(false);

		TimeScaleController.Instance.Unfreeze();
		var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
	}
}