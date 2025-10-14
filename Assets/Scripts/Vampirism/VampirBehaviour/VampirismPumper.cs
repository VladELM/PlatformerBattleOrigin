using System;
using UnityEngine;

public class VampirismPumper : MonoBehaviour
{
    [SerializeField] private VampirismCounter _counter;
    [SerializeField] private float _pumpValue;

    public event Action<float> HealthPumped;

    public void Pump(IDamageable pumpTarget)
    {
        pumpTarget.TakeDamage(_pumpValue);
        HealthPumped?.Invoke(_pumpValue);
    }
}
