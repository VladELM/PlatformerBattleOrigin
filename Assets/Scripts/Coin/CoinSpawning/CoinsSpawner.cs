using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private List<Transform> SpawnPoints;


    private void Awake()
    {
        SpawnOnStart();
    }

    private void SpawnOnStart()
    {
        int spawnPointsAmount = SpawnPoints.Count;

        for (int i = 0; i < spawnPointsAmount; i++)
        {
            if (SpawnPoints[i].TryGetComponent(out CoinSpawnPoint coinSpawnPoint))
            {
                Coin coin = Instantiate(_coinPrefab);
                coin.transform.position = SpawnPoints[i].transform.position;
            }
        }

        SpawnPoints.Clear();
    }

#if UNITY_EDITOR
    [ContextMenu("FillSpanwPointsList")]
    private void FillSpawnPointsList()
    {
        int childAmount = transform.childCount;

        for (int i = 0; i < childAmount; i++)
            SpawnPoints.Add(transform.GetChild(i));
    }

#endif
}
