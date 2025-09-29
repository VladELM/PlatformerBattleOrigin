using System;
using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    private bool _isCollisionPossible;

    public event Action<IDamageable> AttackTargetGot;
    public event Action<float> TargetPositionXGot;
    public event Action AttackTargetDetected;
    public event Action<Transform> ExitedTargetGot;
    public event Action ExitedTargetLeft;
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
                if (attackTrigger.TryGetComponent(out Health playerHealth))
                {
                    AttackTargetGot?.Invoke(playerHealth);
                    TargetPositionXGot?.Invoke(DirectionCalculator.GetDirection(transform.position.x,
                                                                                playerHealth.transform.position.x));
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

                    if (player.gameObject.activeSelf)
                        ExitedTargetGot?.Invoke(attackTrigger.transform);
                    else
                        ExitedTargetLeft?.Invoke();
                }
            }
        }
    }

    public void TurnOnCollision()
    {
        _isCollisionPossible = false;
    }
}
