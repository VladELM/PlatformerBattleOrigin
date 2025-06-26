using System.Collections;
using UnityEngine;

public abstract class CharacterHealth : MonoBehaviour, IDamageable
{
    [SerializeField] protected int _health;
    
    public int MaxHealth {get; private set;}

    private void Start()
    {
        MaxHealth = _health;
    }

    public void TakeDamage(int incomingDamage)
    {
        if (_health > 0)
            _health -= incomingDamage;
    }

    public void StartMonitorHealth()
    {
        StartCoroutine(MonitoringHealth());
    }

    protected abstract IEnumerator MonitoringHealth();
}
