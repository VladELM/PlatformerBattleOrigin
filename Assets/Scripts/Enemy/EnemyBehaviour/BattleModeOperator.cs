using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Haunter))]
[RequireComponent(typeof(EnemyAttacker))]
[RequireComponent (typeof(AttackCollider))]

public class BattleModeOperator : MonoBehaviour
{
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private LayerMask _detectingLayer;
    [SerializeField] private float _rayDistance;
    [SerializeField] private AttackCollider _attackCollider;
    [SerializeField] private float _losingDistance;

    private Haunter _haunter;
    private EnemyAttacker _monsterAttacker;
    private Transform _hostileTarget;
    private IDamageable _attackTarget;

    public event Action<float> HostileTargetDetected;
    public event Action Lost;

    private void OnDestroy()
    {
        _attackCollider.HostileTargetGot -= SetHostileTarget;
    }

    public void Initialize()
    {
        _haunter = GetComponent<Haunter>();
        _monsterAttacker = GetComponent<EnemyAttacker>();
        _attackCollider = GetComponent<AttackCollider>();

        _attackCollider.HostileTargetGot += SetHostileTarget;
    }

    public bool IsHostileTargetDetected()
    {
        bool isHostileTarget = false;
        RaycastHit2D hit = Physics2D.Raycast(_rayOrigin.position, _rayOrigin.right,
                                                _rayDistance, _detectingLayer);

        if (hit.collider)
        {
            if (hit.collider.TryGetComponent(out Player component))
            {
                _hostileTarget = component.transform;
                isHostileTarget = true;
            }
        }

        return isHostileTarget;
    }

    public void OperateBattleMode()
    {
        HostileTargetDetected?.Invoke(_hostileTarget.position.x);

        float offset = Vector3.Distance(_hostileTarget.position, transform.position);

        if (_attackCollider.IsCloseToAttack)
        {
            _monsterAttacker.Attack(_attackTarget);
        }
        else
        {
            if (offset < _losingDistance)
                _haunter.Haunt(_hostileTarget);
            else if (offset >= _losingDistance)
                Lost?.Invoke();
        }
    }

    private void SetHostileTarget(IDamageable hostileTarget)
    {
        _attackTarget = hostileTarget;
    }
}
