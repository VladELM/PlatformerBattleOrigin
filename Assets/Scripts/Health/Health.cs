using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _value;

    private float _maxValue;

    public event Action<float> MaxValueAssigned;
    public event Action<float> ValueChanged;
    public event Action BecameEmpty;
    public event Action Spawned;

    public void Heal(float healPoints, IHealable healable)
    {
        if (_value < _maxValue)
        {
            float healthValue = _value + healPoints;

            if (healthValue <= _maxValue)
            {
                ValueChanged?.Invoke(_value + healPoints);
                _value += healPoints;
            }
            else if (healthValue > _maxValue)
            {
                ValueChanged?.Invoke(_maxValue);
                _value = _maxValue;
            }

            healable.DeactivateHealler();
        }
    }

    public void TakeDamage(float incomingDamage)
    {
        if (incomingDamage > 0)
        {
            if (_value != 0)
            {
                if (_value >= incomingDamage)
                {
                    ValueChanged?.Invoke(_value - incomingDamage);
                    _value -= incomingDamage;

                    if (_value == 0)
                        BecameEmpty?.Invoke();
                }
                else if (_value < incomingDamage)
                {
                    _value = 0;
                    ValueChanged?.Invoke(_value);
                    BecameEmpty?.Invoke();
                }
            }
        }
    }

    public void AssignMaxValue()
    {
        _maxValue = _value;
        MaxValueAssigned?.Invoke(_maxValue);
    }

    public void Restore()
    {
        _value = _maxValue;
        Spawned?.Invoke();
    }
}
