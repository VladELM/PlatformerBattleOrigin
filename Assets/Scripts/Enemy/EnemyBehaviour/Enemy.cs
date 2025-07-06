using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RendererOperator))]
[RequireComponent(typeof(Patroller))]
[RequireComponent(typeof(Haunter))]
[RequireComponent(typeof(EnemyAttacker))]
[RequireComponent (typeof(EnemyAttackCollider))]
[RequireComponent(typeof(AttackComponent))]
[RequireComponent(typeof(EnemyAnimation))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(ItemsCollector))]
[RequireComponent(typeof(EnemyKiller))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Patroller _patroller;
    [SerializeField] private Haunter _haunter;
    [SerializeField] private EnemyAttacker _enemyAttacker;
    [SerializeField] private EnemyAnimation _enemyAnimation;
    [SerializeField] private EnemyAlertSign _enemyAlertSign;
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private ItemsCollector _itemsCollector;
    [SerializeField] private EnemyKiller _enemyKiller;

    private RendererOperator _rendererOperator;
    private Rotator _enemyRotator;
    private EnemyAttackCollider _enemyAttackCollider;
    private Vector3 _startPosition;

    private void OnDestroy()
    {
        _patroller.PatrolTargetChanged -= _enemyRotator.RotateEnemy;
        _patroller.HauntTargetDetected -= _enemyAlertSign.TurnOnAlertSign;
        _patroller.HauntTargetGot -= _haunter.StartHaunting;

        _haunter.HauntingTargetPositionChanged += _enemyRotator.RotateEnemy;
        _haunter.HauntingTargetLost -= _enemyAlertSign.TurnOffAlertSign;
        _haunter.HauntingTargetLost -= _patroller.StartPatrolling;

        _enemyAttackCollider.AttackTargetDetected -= _patroller.StopPatrolling;
        _enemyAttackCollider.AttackTargetDetected -= _haunter.StopHaunting;
        _enemyAttackCollider.TargetPositionXGot -= _enemyRotator.RotateEnemy;
        _enemyAttackCollider.AttackTargetDetected -= _enemyAlertSign.TurnOnAlertSign;
        _enemyAttackCollider.AttackTargetGot -= _enemyAttacker.AssignAttackTarget;
        _enemyAttackCollider.AttackTargetDetected -= _enemyAnimation.TurnOnAttackAnimation;

        _enemyAttackCollider.HostileTargetLeft -= _enemyAnimation.TurnOffAttackAnimation;
        _enemyAttackCollider.HostileTargetLeft -= _enemyAttacker.RemoveAttackTarget;
        _enemyAttackCollider.ExitedTargetGot -= _haunter.StartHaunting;

        _enemyHealth.HealthBecameEmpty -= _enemyAttackCollider.TurnOnCollision;
        _enemyHealth.HealthBecameEmpty -= _enemyAlertSign.TurnOffAlertSign;
        _enemyHealth.HealthBecameEmpty -= _enemyAnimation.TurnOnDeathAnimation;
        _enemyHealth.HealthBecameEmpty -= _enemyKiller.Kill;

        _itemsCollector.HeallerDetected -= _enemyHealth.Heal;
    }

    public void Initialize(Vector3 position, List<Transform> targets)
    {
        transform.position = position;
        _startPosition = position;

        _rendererOperator = GetComponent<RendererOperator>();
        _patroller = GetComponent<Patroller>();
        _haunter = GetComponent<Haunter>();
        _enemyAttacker = GetComponent<EnemyAttacker>();
        _enemyAnimation = GetComponent<EnemyAnimation>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _itemsCollector = GetComponent<ItemsCollector>();
        _enemyKiller = GetComponent<EnemyKiller>();
        _enemyRotator = GetComponent<Rotator>();
        _enemyAttackCollider = GetComponent<EnemyAttackCollider>();

        _enemyKiller.Initialize(_enemyAnimation.GetAnimationsLength());
        _haunter.Initialize(_patroller.RayDistance);
        _patroller.Initialize(targets);
        _rendererOperator.FillRenderersList();
    }

    public void RunAfterInitialization()
    {
        _patroller.StartPatrolling();
    }

    public void Subscribe()
    {
        _patroller.PatrolTargetChanged += _enemyRotator.RotateEnemy;
        _patroller.HauntTargetDetected += _enemyAlertSign.TurnOnAlertSign;
        _patroller.HauntTargetGot += _haunter.StartHaunting;

        _haunter.HauntingTargetPositionChanged += _enemyRotator.RotateEnemy;
        _haunter.HauntingTargetLost += _enemyAlertSign.TurnOffAlertSign;
        _haunter.HauntingTargetLost += _patroller.StartPatrolling;

        _enemyAttackCollider.AttackTargetDetected += _patroller.StopPatrolling;
        _enemyAttackCollider.AttackTargetDetected += _haunter.StopHaunting;
        _enemyAttackCollider.TargetPositionXGot += _enemyRotator.RotateEnemy;
        _enemyAttackCollider.AttackTargetDetected += _enemyAlertSign.TurnOnAlertSign;
        _enemyAttackCollider.AttackTargetGot += _enemyAttacker.AssignAttackTarget;
        _enemyAttackCollider.AttackTargetDetected += _enemyAnimation.TurnOnAttackAnimation;

        _enemyAttackCollider.HostileTargetLeft += _enemyAnimation.TurnOffAttackAnimation;
        _enemyAttackCollider.HostileTargetLeft += _enemyAttacker.RemoveAttackTarget;
        _enemyAttackCollider.ExitedTargetGot += _haunter.StartHaunting;

        _enemyHealth.HealthBecameEmpty += _enemyAttackCollider.TurnOnCollision;
        _enemyHealth.HealthBecameEmpty += _enemyAlertSign.TurnOffAlertSign;
        _enemyHealth.HealthBecameEmpty += _enemyAnimation.TurnOnDeathAnimation;
        _enemyHealth.HealthBecameEmpty += _enemyKiller.Kill;

        _itemsCollector.HeallerDetected += _enemyHealth.Heal;
    }

    public void Respawn()
    {
        transform.position = _startPosition;
        _enemyHealth.Restore();
        _rendererOperator.TurnOnRenderer();
        _patroller.StartPatrolling();
    }

    public void Unsubscribe()
    {
        _patroller.PatrolTargetChanged -= _enemyRotator.RotateEnemy;
        _patroller.HauntTargetDetected -= _enemyAlertSign.TurnOnAlertSign;
        _patroller.HauntTargetGot -= _haunter.StartHaunting;

        _haunter.HauntingTargetPositionChanged += _enemyRotator.RotateEnemy;
        _haunter.HauntingTargetLost -= _enemyAlertSign.TurnOffAlertSign;
        _haunter.HauntingTargetLost -= _patroller.StartPatrolling;

        _enemyAttackCollider.AttackTargetDetected -= _patroller.StopPatrolling;
        _enemyAttackCollider.AttackTargetDetected -= _haunter.StopHaunting;
        _enemyAttackCollider.TargetPositionXGot -= _enemyRotator.RotateEnemy;
        _enemyAttackCollider.AttackTargetDetected -= _enemyAlertSign.TurnOnAlertSign;
        _enemyAttackCollider.AttackTargetGot -= _enemyAttacker.AssignAttackTarget;
        _enemyAttackCollider.AttackTargetDetected -= _enemyAnimation.TurnOnAttackAnimation;

        _enemyAttackCollider.HostileTargetLeft -= _enemyAnimation.TurnOffAttackAnimation;
        _enemyAttackCollider.HostileTargetLeft -= _enemyAttacker.RemoveAttackTarget;
        _enemyAttackCollider.ExitedTargetGot -= _haunter.StartHaunting;

        _enemyHealth.HealthBecameEmpty -= _enemyAttackCollider.TurnOnCollision;
        _enemyHealth.HealthBecameEmpty -= _enemyAlertSign.TurnOffAlertSign;
        _enemyHealth.HealthBecameEmpty -= _enemyAnimation.TurnOnDeathAnimation;
        _enemyHealth.HealthBecameEmpty -= _enemyKiller.Kill;

        _itemsCollector.HeallerDetected -= _enemyHealth.Heal;
    }
}
