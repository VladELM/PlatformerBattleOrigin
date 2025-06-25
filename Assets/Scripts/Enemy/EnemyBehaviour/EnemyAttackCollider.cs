using System;
using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    private bool _isCollisionPossible;

    public event Action<IDamageable> AttackTargetGot;
    public event Action AttackTargetDetected;
    public event Action<Transform> ExitedTargetGot;
    public event Action HostileTargetLeft;

    private void Start()
    {
        _isCollisionPossible = true;
    }

    public void TurnOnCollision()
    {
        _isCollisionPossible = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isCollisionPossible)
        {
            if (collision.gameObject.TryGetComponent(out AttackTrigger attackTrigger))
            {
                if (attackTrigger.TryGetComponent(out PlayerHealth playerHealth))
                {
                    AttackTargetGot?.Invoke(playerHealth);
                    AttackTargetDetected?.Invoke();
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_isCollisionPossible)
        {
            if (collision.gameObject.TryGetComponent(out AttackTrigger attackTrigger))
            {
                if (attackTrigger.TryGetComponent(out Player player))
                {
                    HostileTargetLeft?.Invoke();
                    ExitedTargetGot?.Invoke(attackTrigger.transform);
                }
            }
        }
    }
}
