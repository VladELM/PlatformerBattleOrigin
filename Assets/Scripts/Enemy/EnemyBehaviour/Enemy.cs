using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Patroller))]
[RequireComponent(typeof(Haunter))]
[RequireComponent(typeof(EnemyAttacker))]
[RequireComponent (typeof(EnemyAttackCollider))]
[RequireComponent(typeof(AttackTrigger))]
[RequireComponent(typeof(EnemyAnimation))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyKiller))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private Patroller _patroller;
    [SerializeField] private Haunter _haunter;
    [SerializeField] private EnemyAttacker _enemyAttacker;
    [SerializeField] private EnemyAnimation _enemyAnimation;
    [SerializeField] private EnemyAlertSign _enemyAlertSign;
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private EnemyKiller _enemyKiller;

    private Rotator _enemyRotator;
    private EnemyAttackCollider _enemyAttackCollider;

    private void OnDestroy()
    {
        _patroller.PatrolTargetChanged -= _enemyRotator.RotateEnemy;
        _patroller.HauntTargetDetected -= _enemyAlertSign.SetActiveAlertTarget;
        _patroller.HauntTargetGot -= _haunter.StartHaunting;

        _haunter.HauntingTargetPositionChanged += _enemyRotator.RotateEnemy;
        _haunter.HauntingTargetLost -= _enemyAlertSign.SetUnactiveAlertTarget;
        _haunter.HauntingTargetLost -= _patroller.StartPatrolling;

        _enemyAttackCollider.AttackTargetDetected -= _patroller.StopPatrolling;
        _enemyAttackCollider.AttackTargetDetected -= _haunter.StopHaunting;
        _enemyAttackCollider.AttackTargetDetected -= _enemyAnimation.ToggleAttackAnimation;
        _enemyAttackCollider.AttackTargetDetected -= _enemyAlertSign.SetActiveAlertTarget;
        _enemyAttackCollider.AttackTargetGot -= _enemyAttacker.StartAttack;

        _enemyAttackCollider.HostileTargetLeft -= _enemyAnimation.ToggleAttackAnimation;
        _enemyAttackCollider.HostileTargetLeft -= _enemyAttacker.StopAttack;
        _enemyAttackCollider.ExitedTargetGot -= _haunter.StartHaunting;

        _enemyHealth.HealthBecameEmpty -= _enemyAttackCollider.TurnOnCollision;
        _enemyHealth.HealthBecameEmpty -= _enemyAnimation.ToggleAttackAnimation;
        _enemyHealth.HealthBecameEmpty -= _enemyAttacker.StopAttack;
        _enemyHealth.HealthBecameEmpty -= _enemyAnimation.PlayDeathAnimation;
        _enemyHealth.HealthBecameEmpty -= _enemyKiller.Kill;
    }

    public void Initialize(Vector3 position, List<Transform> targets)
    {
        transform.position = position;

        _patroller = GetComponent<Patroller>();
        _haunter = GetComponent<Haunter>();
        _enemyAttacker = GetComponent<EnemyAttacker>();
        _enemyAnimation = GetComponent<EnemyAnimation>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyKiller = GetComponent<EnemyKiller>();
        _enemyRotator = GetComponent<Rotator>();
        _enemyAttackCollider = GetComponent<EnemyAttackCollider>();

        _patroller.PatrolTargetChanged += _enemyRotator.RotateEnemy;
        _patroller.HauntTargetDetected += _enemyAlertSign.SetActiveAlertTarget;
        _patroller.HauntTargetGot += _haunter.StartHaunting;

        _haunter.HauntingTargetPositionChanged += _enemyRotator.RotateEnemy;
        _haunter.HauntingTargetLost += _enemyAlertSign.SetUnactiveAlertTarget;
        _haunter.HauntingTargetLost += _patroller.StartPatrolling;

        _enemyAttackCollider.AttackTargetDetected += _patroller.StopPatrolling;
        _enemyAttackCollider.AttackTargetDetected += _haunter.StopHaunting;
        _enemyAttackCollider.AttackTargetDetected += _enemyAnimation.ToggleAttackAnimation;
        _enemyAttackCollider.AttackTargetDetected += _enemyAlertSign.SetActiveAlertTarget;
        _enemyAttackCollider.AttackTargetGot += _enemyAttacker.StartAttack;

        _enemyAttackCollider.HostileTargetLeft += _enemyAnimation.ToggleAttackAnimation;
        _enemyAttackCollider.HostileTargetLeft += _enemyAttacker.StopAttack;
        _enemyAttackCollider.ExitedTargetGot += _haunter.StartHaunting;

        _enemyHealth.HealthBecameEmpty += _enemyAttackCollider.TurnOnCollision;
        _enemyHealth.HealthBecameEmpty += _enemyAnimation.ToggleAttackAnimation;
        _enemyHealth.HealthBecameEmpty += _enemyAttacker.StopAttack;
        _enemyHealth.HealthBecameEmpty += _enemyAnimation.PlayDeathAnimation;
        _enemyHealth.HealthBecameEmpty += _enemyKiller.Kill;

        _enemyHealth.StartMonitorHealth();
        _haunter.Initialize(_patroller.RayDistance);
        _patroller.Initialize(targets);
        _patroller.StartPatrolling();
    }
}
