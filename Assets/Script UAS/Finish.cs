using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject questionPanel;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Hentikan suara mesin
            Car car = collision.gameObject.GetComponent<Car>();

            if (car != null)
            {
                car.StopEngineAudio();
            }

            // Pause game
            Time.timeScale = 0f;

            // Tampilkan panel soal
            questionPanel.SetActive(true);
        }
    }
}