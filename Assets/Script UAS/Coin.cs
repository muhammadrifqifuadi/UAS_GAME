using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Menyentuh: " + other.name);

        // Cek tag pada parent jika collider ada di child
        if (other.transform.root.CompareTag("Player"))
        {
            CoinManager.Instance.AddCoin(value);

            TimerManager.Instance.AddTime(20f);
            
            Destroy(gameObject);
        }
    }
}