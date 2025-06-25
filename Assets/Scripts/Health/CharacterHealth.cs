using System.Collections;
using UnityEngine;

abstract public class CharacterHealth : MonoBehaviour, IDamageable
{
    [SerializeField] protected int _health;

    //private Coroutine _coroutine;
    
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
        StartCoroutine(HealthMonitoring());
    }

    abstract protected IEnumerator HealthMonitoring();
}
