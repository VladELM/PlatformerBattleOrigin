using static UnityEngine.Random;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemiesSpawner : Spawner
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _maxPoolSize;
    [SerializeField] private int _activeEnemiesAmount;
    [SerializeField] protected int _minDelay;
    [SerializeField] protected int _maxDelay;

    private Queue<Enemy> _enemiesPool;

    private void Awake()
    {
        SpawnOnStart();
    }

    protected override void SpawnOnStart()
    {
        _enemiesPool = new Queue<Enemy>();

        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            if (_spawnPoints[i].TryGetComponent(out EnemySpawnPoint enemySpawnPoint))
            {
                Enemy enemy = Instantiate(_enemyPrefab);
                enemy.Initialize(_spawnPoints[i].transform.position, enemySpawnPoint.GetPatrolPoints());

                if (_enemiesPool.Count < _maxPoolSize)
                {
                    enemy.gameObject.SetActive(false);
                    _enemiesPool.Enqueue(enemy);
                }
                else
                {
                    if (enemy.TryGetComponent(out EnemyKiller enemyKiller))
                    {
                        enemyKiller.Killed += GiveBack;
                        enemy.Subscribe();
                        enemy.RunAfterInitialization();
                    }
                }
            }
        }

        _spawnPoints.Clear();
    }

    private IEnumerator Spawning()
    {
        yield return new WaitForSeconds(Range(_minDelay, _maxDelay + 1));

        TakeFromPool();
    }

    private void TakeFromPool()
    {
        Enemy enemy = _enemiesPool.Dequeue();

        if (enemy.TryGetComponent(out EnemyKiller enemyKiller))
        {
            enemyKiller.Killed += GiveBack;
            enemy.gameObject.SetActive(true);
            enemy.Subscribe();
            //enemy.RunAfterPool();
            enemy.Respawn();
        }
    }

    private void GiveBack(EnemyKiller enemyKiller)
    {
        if (enemyKiller.TryGetComponent(out Enemy enemy))
        {
            enemyKiller.Killed -= GiveBack;
            enemy.Unsubscribe();
            enemy.gameObject.SetActive(false);
            _enemiesPool.Enqueue(enemy);

            StartCoroutine(Spawning());
        }
    }
}
