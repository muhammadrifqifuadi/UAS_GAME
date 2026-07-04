using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceaneload : MonoBehaviour
{
    public void LoadSceneNew(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}