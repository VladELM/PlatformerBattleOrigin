using System;
using System.Collections.Generic;
using UnityEngine;

public class VampirismTrigger : MonoBehaviour
{
    private Collider2D _collider2D;
    private List<Transform> _targets;
    private Transform _nearestTarget;
    private bool _isActive;

    public event Action<IDamageable> PumpingTargetGot;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _targets = new List<Transform>();
        _isActive = false;
    }

    private void Update()
    {
        if (_isActive)
        {
            if (_targets.Count > 1)
                AssigneNearestEnemy();

            if (_targets.Count == 0)
                PumpingTargetGot?.Invoke(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collider2D.enabled)
        {
            if (collision.TryGetComponent(out AttackComponent attackComponent))
            {
                _targets.Add(attackComponent.transform);

                if (_nearestTarget == null && _targets.Count == 0)
                    _nearestTarget = attackComponent.transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_collider2D.enabled)
        {
            if (collision.TryGetComponent(out AttackComponent attackComponent))
                _targets.Remove(attackComponent.transform);
        }
    }

    public void AssigneActivityState()
    {
        _isActive = !_isActive;
    }

    private void AssigneNearestEnemy()
    {
        for (int i = 0; i < _targets.Count; i++)
        {
            Vector2 offset = _targets[i].transform.position - transform.position;
            Vector2 offsetNearestEnemy = _nearestTarget.transform.position - transform.position;

            if (offset.sqrMagnitude < offsetNearestEnemy.sqrMagnitude)
            {
                _nearestTarget = _targets[i].transform;

                if (_nearestTarget.TryGetComponent(out IDamageable damageable))
                    PumpingTargetGot?.Invoke(damageable);
            }
        }
    }
}
