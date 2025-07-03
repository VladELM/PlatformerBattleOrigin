using System;
using UnityEngine;

public class ItemsCollector : MonoBehaviour, ICollectable
{
    private string _coinCollectorLayer = "Player";
    private string _objectLayer;

    public event Action<int> CoinCollected;
    public event Action<int, IHealable> HeallerDetected;

    private void Start()
    {
        _objectLayer = gameObject.tag;
    }

    public void Collect(Coin coin)
    {
        if (_objectLayer == _coinCollectorLayer)
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
