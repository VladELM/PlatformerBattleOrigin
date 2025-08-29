using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    private Health _health;

    public event Action<HealthIndicator> Dead;

    private void OnEnable()
    {
        if (_health != null)
            _health.BecameEmpty += Notify;
    }

    private void OnDisable()
    {
        if (_health != null)
            _health.BecameEmpty -= Notify;
    }

    public void Initialize(Health health)
    {
        _health = health;
    }

    public void Notify()
    {
        Dead?.Invoke(this);
    }
}
