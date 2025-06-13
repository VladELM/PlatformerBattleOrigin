using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private float _damage;

    public void Attack(IDamageable attackTarget)
    {
        attackTarget.TakeDamage(_damage);
    }
}
