using System;
using UnityEngine;

public class HealthBarHealth : MonoBehaviour
{
    [SerializeField] protected float Value;

    private float _maxValue;

    public event Action<float> ValueChanged;
    public event Action<float> MaxValueAssigned;
    public event Action Started;

    private void Awake()
    {
        _maxValue = Value;
    }

    private void Start()
    {
        MaxValueAssigned?.Invoke(_maxValue);
        Started?.Invoke();
    }

    public void Heal(int healPoints)
    {
        if (Value < _maxValue)
        {
            float healthValue = Value + healPoints;

            if (healthValue <= _maxValue)
            {
                ValueChanged?.Invoke(Value + healPoints);
                Value += healPoints;
            }
            else if (healthValue > _maxValue)
            {
                ValueChanged?.Invoke(_maxValue);
                Value = _maxValue;
            }
        }
    }

    public void TakeDamage(int incomingDamage)
    {
        if (incomingDamage > 0)
        {
            if (Value != 0)
            {
                if (Value >= incomingDamage)
                {
                    ValueChanged?.Invoke(Value - incomingDamage);
                    Value -= incomingDamage;
                }
                else if (Value < incomingDamage)
                {
                    ValueChanged?.Invoke(0);
                    Value = 0;
                }
            }
        }
    }
}
