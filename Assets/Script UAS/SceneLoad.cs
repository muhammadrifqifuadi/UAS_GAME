using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceaneload : MonoBehaviour
{
    public void LoadSceneNew(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
