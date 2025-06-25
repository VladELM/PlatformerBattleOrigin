using System;
using System.Collections;

public class EnemyHealth : CharacterHealth
{
    public event Action HealthBecameEmpty;

    protected override IEnumerator HealthMonitoring()
    {
        while (enabled)
        {
            yield return null;

            if (_health <= 0)
            {
                HealthBecameEmpty?.Invoke();
                break;
            }
        }
    }
}
