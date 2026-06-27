using UnityEngine;
using UnityEngine.InputSystem;

public class ExitMenu : MonoBehaviour
{
    [SerializeField] private GameObject exitPopup;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (exitPopup.activeSelf)
                CloseExitPopup();
            else
                OpenExitPopup();
        }
    }

    public void OpenExitPopup()
    {
        exitPopup.SetActive(true);
    }

    public void CloseExitPopup()
    {
        exitPopup.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}