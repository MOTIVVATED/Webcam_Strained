using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResultView : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;
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
        if ( GameManager.Instance == null) return;
        
        GameManager.Instance.OnWin -= ShowWin;
        
        GameManager.Instance.OnLose -= ShowLose;
    }
    private void ShowWin(int total)
    {
        GameResultsManager.Instance.SaveResult(
            score: total
            //itemsCaught: 0, // I'll add this later
            //itemsDodged: 0, // and this too
            //timeSurvived: 0f // and this too
        );

        resultText.text = $"W W! \nтотал: {total}tk";

        resultText.gameObject.SetActive(true);

        restartLabels.SetActive(true);
    }
    private void ShowLose(int total)
    {
        resultText.text = $"ТИЛЬТ! \nтотпл: {total}tk";

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
}
