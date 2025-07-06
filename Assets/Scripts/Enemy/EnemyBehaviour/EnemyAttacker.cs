using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delayValue;

    private IDamageable _attackTarget;

    public void AssignAttackTarget(IDamageable attackTarget)
    {
        _attackTarget = attackTarget;
    }

    public void RemoveAttackTarget()
    {
        _attackTarget = null;
    }

    public void Attack()
    {
        _attackTarget.TakeDamage(_damage);
    }
}
