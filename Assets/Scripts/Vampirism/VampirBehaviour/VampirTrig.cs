using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampirTrig : MonoBehaviour
{
    private Collider2D _collider2D;
    private List<Transform> _enemies;
    private Transform _nearestEnemy;

    public event Action<IDamageable> PumpingTargetGot;

    private void Awake()
    {
        _enemies = new List<Transform>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (_enemies.Count > 0)
            AssigneNearestEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collider2D.enabled)
        {
            if (collision.TryGetComponent(out AttackComponent attackComponent))
                _enemies.Add(attackComponent.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_collider2D.enabled)
        {
            if (collision.TryGetComponent(out AttackComponent attackComponent))
                _enemies.Remove(attackComponent.transform);
        }
    }

    private void AssigneNearestEnemy()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            Vector2 offset = _enemies[i].transform.position - transform.position;
            Vector2 offsetNearestEnemy = _nearestEnemy.transform.position - transform.position;

            if (offset.sqrMagnitude < offsetNearestEnemy.sqrMagnitude)
            {
                _nearestEnemy = _enemies[i].transform;

                if (_nearestEnemy.TryGetComponent(out IDamageable damageable))
                    PumpingTargetGot?.Invoke(damageable);
            }
        }
    }
}
