using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : Health
{
    public event Action HealthBecameEmpty;

    public void Restore()
    {
        _healthValue = MaxHealthValue;
    }

    public void StartMonitorHealth()
    {
        StartCoroutine(MonitoringHealth());
    }

    protected override IEnumerator MonitoringHealth()
    {
        while (enabled)
        {
            yield return null;

            if (_healthValue <= 0)
            {
                HealthBecameEmpty?.Invoke();
                break;
            }
        }
    }
}
