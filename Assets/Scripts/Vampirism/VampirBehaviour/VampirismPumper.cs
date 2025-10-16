using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class VampirismPumper : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _pumpValue;

    private Coroutine _coroutine;
    private List<IDamageable> _enemies;

    public event Action<float> HealthPumped;

    private void Awake()
    {
        _enemies = new List<IDamageable>();
    }

    public void AddToList(IDamageable enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveFromList(IDamageable enemy)
    {
        _enemies.Remove(enemy);
    }

    public void StartPumping()
    {
        _coroutine = StartCoroutine(Pamping());
    }

    public void StopPumping()
    {
        StopCoroutine(_coroutine);
        _enemies.Clear();
    }

    private IEnumerator Pamping()
    {
        bool isPamping = true;

        while(isPamping)
        {    
            yield return null;

            if (_enemies.Count > 0)
                Pump();
        }
    }

    private void Pump()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_health.IsHealthNotFull && _enemies[i].IsHealthAboveZero())
            {
                _enemies[i].TakeDamage(_pumpValue);
                HealthPumped?.Invoke(_pumpValue);
            }
        }
    }
}
