using System;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public event Action HostileTargetDetected;
    public event Action<IDamageable> HostileTargetGot;

    public bool IsCloseToAttack { get; private set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out AttackTrigger attackTrigger))
        {
            HostileTargetDetected?.Invoke();

            if (attackTrigger.TryGetComponent(out CharacterHealth characterHealth))
                HostileTargetGot?.Invoke(characterHealth);

            IsCloseToAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out AttackTrigger component))
        {
            HostileTargetDetected?.Invoke();
            IsCloseToAttack = false;
        }
    }
}
