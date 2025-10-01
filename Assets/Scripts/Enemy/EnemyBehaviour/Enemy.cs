using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Patroller))]
[RequireComponent(typeof(Haunter))]
[RequireComponent(typeof(EnemyKiller))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyAttacker))]
[RequireComponent(typeof(ItemsCollector))]
[RequireComponent(typeof(ItemsEnterTrigger))]
[RequireComponent(typeof(EnemyAttackCollider))]
[RequireComponent(typeof(AttackComponent))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyAnimation _enemyAnimation;
    [SerializeField] private EnemyAlertSign _enemyAlertSign;
    [SerializeField] private RendererOperator _rendererOperator;
    [SerializeField] private Flipper _enemyRotator;
    [SerializeField] private Flipper _rayOriginRotator;
    [SerializeField] private HealthBar _healthBar;

    private Patroller _patroller;
    private Haunter _haunter;
    private EnemyKiller _enemyKiller;
    private Health _enemyHealth;
    private EnemyAttacker _enemyAttacker;
    private ItemsCollector _itemsCollector;
    private EnemyAttackCollider _enemyAttackCollider;

    private Vector3 _startPosition;

    private void Awake()
    {
        _patroller = GetComponent<Patroller>();
        _haunter = GetComponent<Haunter>();
        _enemyKiller = GetComponent<EnemyKiller>();
        _enemyHealth = GetComponent<Health>();
        _enemyAttacker = GetComponent<EnemyAttacker>();
        _itemsCollector = GetComponent<ItemsCollector>();
        _enemyAttackCollider = GetComponent<EnemyAttackCollider>();

    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    public void Initialize(Vector3 position, List<Transform> targets)
    {
        transform.position = position;
        _startPosition = position;
        _enemyKiller.Initialize(_enemyAnimation.GetAnimationsLength());
        _haunter.Initialize(_patroller.RayDistance);
        _patroller.Initialize(targets);
        _rendererOperator.FillRenderersList();
        _enemyHealth.AssignMaxValue();
    }

    public void RunAfterInitialization()
    {
        _patroller.StartPatrolling();
    }

    public void Subscribe()
    {
        _patroller.PatrolTargetChanged += _enemyRotator.Flip;
        _patroller.PatrolTargetChanged += _rayOriginRotator.Flip;
        _patroller.HauntTargetDetected += _enemyAlertSign.TurnOnAlertSign;
        _patroller.HauntTargetGot += _haunter.StartHaunting;

        _haunter.HauntingTargetPositionChanged += _enemyRotator.Flip;
        _haunter.HauntingTargetPositionChanged += _rayOriginRotator.Flip;
        _haunter.HauntingTargetLost += _enemyAlertSign.TurnOffAlertSign;
        _haunter.HauntingTargetLost += _patroller.StartPatrolling;

        _enemyAttackCollider.AttackTargetDetected += _patroller.StopPatrolling;
        _enemyAttackCollider.AttackTargetDetected += _haunter.StopHaunting;
        _enemyAttackCollider.TargetPositionXGot += _enemyRotator.Flip;
        _enemyAttackCollider.TargetPositionXGot += _rayOriginRotator.Flip;
        _enemyAttackCollider.AttackTargetDetected += _enemyAlertSign.TurnOnAlertSign;
        _enemyAttackCollider.AttackTargetDetected += _enemyAnimation.TurnOnAttackAnimation;
        _enemyAttackCollider.AttackTargetGot += _enemyAttacker.StartAttack;

        _enemyAttackCollider.HostileTargetLeft += _enemyAttacker.StopAttack;
        _enemyAttackCollider.HostileTargetLeft += _enemyAnimation.TurnOffAttackAnimation;
        _enemyAttackCollider.ExitedTargetGot += _haunter.StartHaunting;
        _enemyAttackCollider.ExitedTargetLeft += _patroller.StartPatrolling;
        _enemyAttackCollider.ExitedTargetLeft += _enemyAlertSign.TurnOffAlertSign;

        _enemyHealth.BecameEmpty += _enemyAttackCollider.TurnOnCollision;
        _enemyHealth.BecameEmpty += _enemyAlertSign.TurnOffAlertSign;
        _enemyHealth.BecameEmpty += _enemyAnimation.TurnOnDeathAnimation;
        _enemyHealth.BecameEmpty += _enemyKiller.Kill;
        _enemyHealth.BecameEmpty += SwicthHelthBarState;
        _enemyHealth.BecameEmpty += SwitchOffAttackCollider;
        _enemyHealth.BecameEmpty += _enemyAttacker.StopAttack;

        _itemsCollector.HeallerDetected += _enemyHealth.Heal;
    }

    public void Respawn()
    {
        _enemyAttackCollider.enabled = true;
        transform.position = _startPosition;
        _healthBar.SetState(true);
        _enemyHealth.Restore();
        _rendererOperator.TurnOnRenderer();
        _patroller.StartPatrolling();
    }

    public void Unsubscribe()
    {
        _patroller.PatrolTargetChanged -= _enemyRotator.Flip;
        _patroller.PatrolTargetChanged -= _rayOriginRotator.Flip;
        _patroller.HauntTargetDetected -= _enemyAlertSign.TurnOnAlertSign;
        _patroller.HauntTargetGot -= _haunter.StartHaunting;

        _haunter.HauntingTargetPositionChanged -= _enemyRotator.Flip;
        _haunter.HauntingTargetPositionChanged -= _rayOriginRotator.Flip;
        _haunter.HauntingTargetLost -= _enemyAlertSign.TurnOffAlertSign;
        _haunter.HauntingTargetLost -= _patroller.StartPatrolling;

        _enemyAttackCollider.AttackTargetDetected -= _patroller.StopPatrolling;
        _enemyAttackCollider.AttackTargetDetected -= _haunter.StopHaunting;
        _enemyAttackCollider.TargetPositionXGot -= _enemyRotator.Flip;
        _enemyAttackCollider.TargetPositionXGot -= _rayOriginRotator.Flip;
        _enemyAttackCollider.AttackTargetDetected -= _enemyAlertSign.TurnOnAlertSign;
        _enemyAttackCollider.AttackTargetDetected -= _enemyAnimation.TurnOnAttackAnimation;
        _enemyAttackCollider.AttackTargetGot -= _enemyAttacker.StartAttack;

        _enemyAttackCollider.HostileTargetLeft -= _enemyAttacker.StopAttack;
        _enemyAttackCollider.HostileTargetLeft -= _enemyAnimation.TurnOffAttackAnimation;
        _enemyAttackCollider.ExitedTargetGot -= _haunter.StartHaunting;
        _enemyAttackCollider.ExitedTargetLeft -= _patroller.StartPatrolling;
        _enemyAttackCollider.ExitedTargetLeft -= _enemyAlertSign.TurnOffAlertSign;

        _enemyHealth.BecameEmpty -= _enemyAttackCollider.TurnOnCollision;
        _enemyHealth.BecameEmpty -= _enemyAlertSign.TurnOffAlertSign;
        _enemyHealth.BecameEmpty -= _enemyAnimation.TurnOnDeathAnimation;
        _enemyHealth.BecameEmpty -= _enemyKiller.Kill;
        _enemyHealth.BecameEmpty -= SwicthHelthBarState;
        _enemyHealth.BecameEmpty -= SwitchOffAttackCollider;
        _enemyHealth.BecameEmpty -= _enemyAttacker.StopAttack;

        _itemsCollector.HeallerDetected -= _enemyHealth.Heal;
    }

    private void SwicthHelthBarState()
    {
        _healthBar.SetState(false);
    }

    private void SwitchOffAttackCollider()
    {
        _enemyAttackCollider.enabled = false;
    }
}
