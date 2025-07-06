using System;
using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    private bool _isCollisionPossible;

    public event Action<IDamageable> AttackTargetGot;
    public event Action<float> TargetPositionXGot;
    public event Action AttackTargetDetected;
    public event Action<Transform> ExitedTargetGot;
    public event Action HostileTargetLeft;

    private void OnEnable()
    {
        _isCollisionPossible = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isCollisionPossible)
        {
            if (collision.gameObject.TryGetComponent(out AttackComponent attackTrigger))
            {
                if (attackTrigger.TryGetComponent(out PlayerHealth playerHealth))
                {
                    AttackTargetGot?.Invoke(playerHealth);
                    TargetPositionXGot?.Invoke(playerHealth.transform.position.x);
                    AttackTargetDetected?.Invoke();
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_isCollisionPossible)
        {
            if (collision.gameObject.TryGetComponent(out AttackComponent attackTrigger))
            {
                if (attackTrigger.TryGetComponent(out Player player))
                {
                    HostileTargetLeft?.Invoke();
                    ExitedTargetGot?.Invoke(attackTrigger.transform);
                }
            }
        }
    }

    public void TurnOnCollision()
    {
        _isCollisionPossible = false;
    }
}
