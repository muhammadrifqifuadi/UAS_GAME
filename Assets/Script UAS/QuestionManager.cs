using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    [Header("Question Panel")]
    public GameObject questionPanel;

    [Header("Win Panel")]
    public GameObject winPanel;

    [Header("Text")]
    public TMP_Text resultText;
    public TMP_Text coinResultText;

    [Header("Scene")]
    public string nextSceneName = "Level 2";

    public void AnswerCorrect()
    {
        // Tutup panel soal
        questionPanel.SetActive(false);

        // Tampilkan panel kemenangan
        winPanel.SetActive(true);

        // Tampilkan jumlah koin
        coinResultText.text = "Koin Terkumpul : " + CoinManager.Instance.GetCoin();

        StartCoroutine(NextLevel());
    }

    public void AnswerWrong()
    {
        resultText.color = Color.red;
        resultText.text = "Jawaban Salah!\nMengulang Level...";

        StartCoroutine(RestartLevel());
    }

    IEnumerator NextLevel()
    {
        Debug.Log("Coroutine Dimulai");

        yield return new WaitForSecondsRealtime(2f);

        Debug.Log("Pindah Scene");

        Time.timeScale = 1f;
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}