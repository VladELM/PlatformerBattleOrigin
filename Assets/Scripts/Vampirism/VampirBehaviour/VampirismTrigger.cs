using System;
using System.Collections.Generic;
using UnityEngine;

public class VampirismTrigger : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private List<Transform> _targets;
    private bool _isActive;

    public event Action<IDamageable> PumpingTargetGot;

    private void Awake()
    {
        _targets = new List<Transform>();
        _isActive = true;
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
        if (collision.TryGetComponent(out AttackComponent attackComponent))
        {
            _targets.Add(attackComponent.transform);

            if (_targets.Count == 1)
            {
                if (attackComponent.TryGetComponent(out IDamageable damageable))
                    PumpingTargetGot?.Invoke(damageable);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out AttackComponent attackComponent))
            _targets.Remove(attackComponent.transform);
    }

    public void AssigneActivityState()
    {
        _isActive = !_isActive;
    }

    private void AssigneNearestEnemy()
    {
        Transform nearestTarget = _targets[0];

        for (int i = 1; i < _targets.Count; i++)
        {
            Vector2 offset = _targets[i].position - _player.position;
            Vector2 offsetNearestTarget = nearestTarget.position - _player.position;

            if (offset.sqrMagnitude < offsetNearestTarget.sqrMagnitude)
                nearestTarget = _targets[i].transform;
        }

        if (nearestTarget.TryGetComponent(out IDamageable damageable))
            PumpingTargetGot?.Invoke(damageable);
    }
}
