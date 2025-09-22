using UnityEngine;

public class CoinsCounter : MonoBehaviour
{
    private int _coinsCount;

    private void Start()
    {
        _coinsCount = 0;
    }

    public void AddCoin(int coinCost)
    {
        _coinsCount += coinCost;
    }
}
