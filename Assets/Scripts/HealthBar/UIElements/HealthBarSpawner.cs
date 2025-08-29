using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarSpawner : MonoBehaviour
{
    [SerializeField] private EnemiesSpawner _enemiesSpawner;
    [SerializeField] private HealthIndicator _healthBarPrefab;

    private Queue<HealthIndicator> _healthBarsPool;

    private void Awake()
    {
        _healthBarsPool = new Queue<HealthIndicator>();
        SpawnOnStart();
    }

    private void OnEnable()
    {
        _enemiesSpawner.Spawned += TakeFromPool;
    }

    private void OnDisable()
    {
        _enemiesSpawner.Spawned -= TakeFromPool;
    }

    public void SpawnOnStart()
    {
        int amount = _enemiesSpawner.MaxPoolSize + _enemiesSpawner.ActiveEnemiesAmount;

        for (int i = 0; i < amount; i++)
        {
            HealthIndicator healthIndicator = Instantiate(_healthBarPrefab, transform.position, Quaternion.identity);
            _healthBarsPool.Enqueue(healthIndicator);
            healthIndicator.gameObject.SetActive(false);
        }
    }

    public void TakeFromPool(EnemyHealth enemyHealth)
    {
        HealthIndicator healthIndicator = _healthBarsPool.Dequeue();
        healthIndicator.Initialize(enemyHealth);
        InitializeHealthBar(healthIndicator, enemyHealth);
        healthIndicator.gameObject.SetActive(true);

        if (healthIndicator.TryGetComponent(out HealthBarMover healthBarMover))
            healthBarMover.Initialize(enemyHealth.transform);

        healthIndicator.Dead += GiveBack;
    }

    private void GiveBack(HealthIndicator healthIndicator)
    {
        healthIndicator.Dead -= GiveBack;
        healthIndicator.transform.SetParent(null);
        healthIndicator.gameObject.SetActive(false);
        _healthBarsPool.Enqueue(healthIndicator);
    }

    private void InitializeHealthBar(HealthIndicator healthIndicator, EnemyHealth enemyHealth)
    {
        int childAmount = healthIndicator.transform.childCount;

        for (int i = 0; i < childAmount; i++)
        {
            if (healthIndicator.transform.GetChild(i).TryGetComponent(out HealthView healthView))
                healthView.Initialize(enemyHealth, enemyHealth.MaxValue);
        }
    }
}
