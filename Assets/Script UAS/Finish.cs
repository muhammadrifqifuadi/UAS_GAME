using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject questionPanel;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            questionPanel.SetActive(true);
        }
    }
}