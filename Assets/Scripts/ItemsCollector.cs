using System;
using UnityEngine;

public class ItemsCollector : MonoBehaviour, ICollectable
{
    public event Action<int> CoinCollected;
    public event Action<float, IHealable> HeallerDetected;

    public void Collect(Coin coin)
    {
        if (this.gameObject.TryGetComponent(out Player player))
        {
            coin.Disactivate();

            if (coin.IsTimeOut)
                CoinCollected?.Invoke(coin.Cost);
        }
    }

    public void Collect(Healler healler)
    {
        HeallerDetected?.Invoke(healler.HealPoints, healler);
    }
}
