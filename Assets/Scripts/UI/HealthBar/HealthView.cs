using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;

    protected float _maxValue;

    private void OnEnable()
    {
        _health.MaxValueAssigned += OnMaxHealthValueAssigned;
        _health.ValueChanged += OnHealthValueChanged;
        _health.Spawned += Restore;
    }

    private void OnDisable()
    {
        _health.MaxValueAssigned -= OnMaxHealthValueAssigned;
        _health.ValueChanged -= OnHealthValueChanged;
        _health.Spawned -= Restore;
    }
    protected abstract void OnMaxHealthValueAssigned(float value);
    protected abstract void OnHealthValueChanged(float value);
    protected abstract void Restore();
}
