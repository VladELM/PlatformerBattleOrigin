using UnityEngine;

public class EnemiesSpawner : Spawner
{
    [SerializeField] private Enemy _enemyPrefab;

    private void Awake()
    {
        Spawn();
    }

    protected override void Spawn()
    {
        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            if (_spawnPoints[i].TryGetComponent(out EnemySpawnPoint enemySpawnPoint))
            {
                Enemy enemy = Instantiate(_enemyPrefab);
                enemy.Initialize(_spawnPoints[i].transform.position, enemySpawnPoint.GetPatrolPoints());
            }
        }

        _spawnPoints.Clear();
    }
}
