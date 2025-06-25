using System.Collections;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delayValue;

    private IDamageable _attackTarget;
    private Coroutine _attackCoroutine;
    private WaitForSeconds _delay;

    private void Start()
    {
        _delay = new WaitForSeconds(_delayValue);
    }

    public void StartAttack(IDamageable attackTarget)
    {
        _attackTarget = attackTarget;
        _attackCoroutine = StartCoroutine(Attacking());
    }

    public void StopAttack()
    {
        StopCoroutine(_attackCoroutine);
        _attackTarget = null;
    }

    private IEnumerator Attacking()
    {
        while (enabled)
        {
            yield return _delay;

            _attackTarget.TakeDamage(_damage);
        }
    }
}
