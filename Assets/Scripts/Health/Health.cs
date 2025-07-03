using System.Collections;
using UnityEngine;

public abstract class Health : MonoBehaviour, IDamageable
{
    [SerializeField] protected int _healthValue;
    
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
        }
    }

    protected abstract IEnumerator MonitoringHealth();
}
