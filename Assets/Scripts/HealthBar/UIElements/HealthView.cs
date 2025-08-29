using System;
using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    private Health _health;

    private void OnEnable()
    {
        if (_health != null)
        {
            //_health.MaxValueAssigned += OnMaxHealthValueAssigned;
            _health.ValueChanged += OnHealthValueChanged;
        }
    }

    private void OnDisable()
    {
        if (_health != null)
        {
            //_health.MaxValueAssigned -= OnMaxHealthValueAssigned;
            _health.ValueChanged -= OnHealthValueChanged;
        }
    }
    protected abstract void OnMaxHealthValueAssigned(float value);
    protected abstract void OnHealthValueChanged(float value);

    public void Initialize(Health health, float value)
    {
        _health = health;
        OnMaxHealthValueAssigned(value);
    }
}
