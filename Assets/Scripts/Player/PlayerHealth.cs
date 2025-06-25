using System.Collections;
using UnityEngine;

public class PlayerHealth : CharacterHealth
{
    protected override IEnumerator HealthMonitoring()
    {
        while (enabled)
        {
            yield return null;

            if (_health <= 0)
                Debug.Log("You are undead...");
            else
                Debug.Log("You are alive !");
        }
    }

    public void RestoreHealth(int healPoints, IHealable healable)
    {
        if ((healPoints + _health) <= MaxHealth)
        {
            _health += healPoints;
            healable.DeactivateHealler();
        }
    }
}
