using System;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    public event Action<IDamageable> AttackTargetGot;
    public event Action AttackTargetLost;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out AttackTrigger attackTrigger))
        {
            if (attackTrigger.TryGetComponent(out EnemyHealth enemyHealth))
                AttackTargetGot?.Invoke(enemyHealth);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out AttackTrigger attackTrigger))
            AttackTargetLost?.Invoke();
    }
}