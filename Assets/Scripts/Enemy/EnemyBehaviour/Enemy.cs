using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rotator))]
[RequireComponent(typeof(Patroller))]
[RequireComponent (typeof(BattleModeOperator))]
[RequireComponent (typeof(AttackCollider))]

public class Enemy : MonoBehaviour
{
    private Rotator _enemyRotator;
    private Patroller _patroller;
    private BattleModeOperator _battleModeOperator;
    private AttackCollider _attackCollider;

    private bool _isInBattleMode;

    private void OnDestroy()
    {
        _patroller.PatrolTargetChanged -= _enemyRotator.RotateEnemy;
        _battleModeOperator.Lost -= ToggleBattleMode;
        _battleModeOperator.HostileTargetDetected -= _enemyRotator.RotateEnemy;
        _attackCollider.HostileTargetDetected -= ToggleBattleMode;
    }

    public void Initialize(Vector3 position, List<Transform> targets)
    {
        transform.position = position;

        _enemyRotator = GetComponent<Rotator>();
        _patroller = GetComponent<Patroller>();
        _battleModeOperator = GetComponent<BattleModeOperator>();
        _attackCollider = GetComponent<AttackCollider>();

        _patroller.PatrolTargetChanged += _enemyRotator.RotateEnemy;
        _patroller.Initialize(targets);

        _battleModeOperator.Initialize();
        _battleModeOperator.Lost += ToggleBattleMode;
        _battleModeOperator.HostileTargetDetected += _enemyRotator.RotateEnemy;

        _attackCollider.HostileTargetDetected += ToggleBattleMode;
        
        _isInBattleMode = false;

        StartCoroutine(Operating());
    }

    private void ToggleBattleMode()
    {
        _isInBattleMode = !_isInBattleMode;
    }

    private IEnumerator Operating()
    {
        while (enabled)
        {
            yield return null;

            if (_isInBattleMode)
            {
                _battleModeOperator.OperateBattleMode();
            }
            else
            {
                _patroller.Patrol();
                
                if (_battleModeOperator.IsHostileTargetDetected())
                    _isInBattleMode = true;
            }
        }
    }
}
