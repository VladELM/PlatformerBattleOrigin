using UnityEngine;

public class CoinsCount : MonoBehaviour
{
    private int _coins;

    private void Start()
    {
        _coins = 0;
    }

    public void AddCoin(int coinsAmount)
    {
        _coins += coinsAmount;
    }
}
