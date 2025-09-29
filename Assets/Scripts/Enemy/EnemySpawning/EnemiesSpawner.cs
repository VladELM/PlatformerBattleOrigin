using static UnityEngine.Random;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _maxPoolSize;
    [SerializeField] private int _activeEnemiesAmount;
    [SerializeField] protected int _minDelay;
    [SerializeField] protected int _maxDelay;
    [SerializeField] protected List<Transform> SpawnPoints;

    private Queue<Enemy> _enemiesPool;

    public int MaxPoolSize { get; private set; }
    public int ActiveEnemiesAmount { get; private set; }

    private void Awake()
    {
        MaxPoolSize = _maxPoolSize;
        ActiveEnemiesAmount = _activeEnemiesAmount;
    }

    private void Start()
    {
        SpawnOnStart();
    }

    private void SpawnOnStart()
    {
        _enemiesPool = new Queue<Enemy>();

        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            if (SpawnPoints[i].TryGetComponent(out EnemySpawnPoint enemySpawnPoint))
            {
                Enemy enemy = Instantiate(_enemyPrefab);
                enemy.Initialize(SpawnPoints[i].transform.position, enemySpawnPoint.GetPatrolPoints());

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

        SpawnPoints.Clear();
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
