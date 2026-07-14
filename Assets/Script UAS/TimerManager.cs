using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    public TMP_Text timerText;

    public float startTime = 90f;

    private float currentTime;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        currentTime = startTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        int minute = Mathf.FloorToInt(currentTime / 60);
        int second = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minute, second);
    }

    public void AddTime(float seconds)
    {
        currentTime += seconds;
    }
}