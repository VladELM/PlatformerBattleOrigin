using System;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public event Action<int> CoinCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Coin coin))
        {
            coin.HideCoin();

            if (coin.IsTimeOut)
                CoinCollected?.Invoke(coin.CoinCost);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Coin coin))
            coin.GiveBackCoin();
    }
}
