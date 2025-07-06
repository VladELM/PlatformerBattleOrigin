using System;
using UnityEngine;

public abstract class Health : MonoBehaviour, IDamageable
{
    [SerializeField] protected int _healthValue;

    public event Action HealthBecameEmpty;

    public int MaxHealthValue {get; private set;}

    private void Awake()
    {
        MaxHealthValue = _healthValue;
    }

    public void Heal(int healPoints, IHealable healable)
    {
        if ((healPoints + _healthValue) <= MaxHealthValue)
        {
            _healthValue += healPoints;
            healable.DeactivateHealler();
        }
    }

    public void TakeDamage(int incomingDamage)
    {
        if (incomingDamage > 0)
        {
            if (_healthValue > 0)
                _healthValue -= incomingDamage;
            else if (_healthValue <= 0)
                HealthBecameEmpty?.Invoke();
        }
    }
}
