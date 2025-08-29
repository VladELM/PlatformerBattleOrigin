using System;
using UnityEngine;

public abstract class Health : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _value;

    //public event Action<float> MaxValueAssigned;
    public event Action<float> ValueChanged;
    public event Action BecameEmpty;

    public float MaxValue {get; private set;}

    private void Awake()
    {
        MaxValue = _value;
    }

    public void Heal(float healPoints, IHealable healable)
    {
        if (_value < MaxValue)
        {
            float healthValue = _value + healPoints;

            if (healthValue <= MaxValue)
            {
                ValueChanged?.Invoke(_value + healPoints);
                _value += healPoints;
            }
            else if (healthValue > MaxValue)
            {
                ValueChanged?.Invoke(MaxValue);
                _value = MaxValue;
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
}
