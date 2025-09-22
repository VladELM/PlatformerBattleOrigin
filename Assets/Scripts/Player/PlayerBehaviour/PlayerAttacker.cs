using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private int _damage;

    private IDamageable _attackTarget;

    public bool IsAttackTarget => _attackTarget != null;

    public void AssignAttackTarget(IDamageable attackTarget)
    {
        _attackTarget = attackTarget;
    }

    public void TakeAwayAttackTarget()
    {
        _attackTarget = null;
    }

    public void Attack()
    {
        _attackTarget.TakeDamage(_damage);
    }
}
