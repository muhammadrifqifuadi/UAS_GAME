using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public TMP_Text coinText;

    public int coin = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        coinText.text = coin.ToString();
    }

    public void AddCoin(int amount)
    {
        coin += amount;
        coinText.text = coin.ToString();
    }
    public int GetCoin()
    {
        return coin;
    }
}