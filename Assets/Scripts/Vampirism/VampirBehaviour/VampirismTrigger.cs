using System;
using UnityEngine;

public class VampirismTrigger : MonoBehaviour
{
    public event Action<bool, IDamageable> PampingTargetGot;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out AttackComponent attackComponent))
        {
            if (attackComponent.TryGetComponent(out Health enemyHealth))
                PampingTargetGot?.Invoke(enemyHealth.IsHealthAboveZero, enemyHealth);
        }
    }
}
