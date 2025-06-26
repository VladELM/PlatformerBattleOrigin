using UnityEngine;

public class CoinsSpawner : Spawner
{
    [SerializeField] private Coin _coinPrefab;

    private void Awake()
    {
        Spawn();
    }

    protected override void Spawn()
    {
        int spawnPointsAmount = _spawnPoints.Count;

        for (int i = 0; i < spawnPointsAmount; i++)
        {
            if (_spawnPoints[i].TryGetComponent(out CoinSpawnPoint coinSpawnPoint))
            {
                Coin coin = Instantiate(_coinPrefab);
                coin.transform.position = _spawnPoints[i].transform.position;
            }
        }

        _spawnPoints.Clear();
    }
}
