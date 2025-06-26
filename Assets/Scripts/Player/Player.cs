using UnityEngine;

[RequireComponent (typeof(InputReader))]
[RequireComponent (typeof(Rotator))]
[RequireComponent (typeof(Mover))]
[RequireComponent (typeof(Jumper))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent (typeof(PlayerHealth))]
[RequireComponent(typeof(HeallerCollector))]
[RequireComponent(typeof(CoinsCounter))]
[RequireComponent(typeof(CoinCollector))]
[RequireComponent (typeof(PlayerAttackCollider))]
[RequireComponent (typeof(PlayerAttacker))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private Rotator _rotator;
    private Mover _mover;
    private Jumper _jumper;
    private PlayerAnimator _playerAnimator;
    private PlayerHealth _playerHealth;
    private HeallerCollector _heallerCollector;
    private CoinsCounter _coinsCount;
    private CoinCollector _coinCollector;
    private PlayerAttacker _playerAttacker;
    private PlayerAttackCollider _playerAttackCollider;

    private void Start()
    {
        _inputReader = GetComponent<InputReader>();
        _rotator = GetComponent<Rotator>();
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerHealth = GetComponent<PlayerHealth>();
        _heallerCollector = GetComponent<HeallerCollector>();
        _coinsCount = GetComponent<CoinsCounter>();
        _coinCollector = GetComponent<CoinCollector>();
        _playerAttackCollider = GetComponent<PlayerAttackCollider>();
        _playerAttacker = GetComponent<PlayerAttacker>();

        _playerHealth.StartMonitorHealth();

        _heallerCollector.HeallerDetected += _playerHealth.RestoreHealth;
        _coinCollector.CoinCollected += _coinsCount.AddCoin;
        _playerAttackCollider.AttackTargetGot += _playerAttacker.AssignAttackTarget;
        _playerAttackCollider.AttackTargetLost += _playerAttacker.TakeAwayAttackTarget;
    }

    private void OnDestroy()
    {
        _heallerCollector.HeallerDetected -= _playerHealth.RestoreHealth;
        _coinCollector.CoinCollected -= _coinsCount.AddCoin;
        _playerAttackCollider.AttackTargetGot -= _playerAttacker.AssignAttackTarget;
        _playerAttackCollider.AttackTargetLost -= _playerAttacker.TakeAwayAttackTarget;
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
        {
            _rotator.RotatePlayer(_inputReader.Direction);
            _mover.Move(_inputReader.Direction);
        }

        if (_inputReader.IsJump)
            _jumper.Jump();

        if (_inputReader.IsAttack)
        {
            if (_playerAttacker.IsAttackTarget)
                _playerAttacker.Attack();
        }
    }

    private void Update()
    {
        if (_jumper.IsJumping)
        {
            if (_inputReader.IsMoveLeft || _inputReader.IsMoveRight)
                _playerAnimator.TurnOnIdleAnimation();
        }
        else
        {
            if (_inputReader.IsMoveLeft || _inputReader.IsMoveRight)
                _playerAnimator.TurnOnMoveAnimation();

            if (_inputReader.IsIdleLeft || _inputReader.IsIdleRight)
                _playerAnimator.TurnOnIdleAnimation();
        }
    }
}
