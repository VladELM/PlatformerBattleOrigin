using UnityEngine;

[RequireComponent (typeof(Jumper))]
[RequireComponent (typeof(Flipper))]
public class Player : MonoBehaviour
{
    [SerializeField] private Flipper _rotator;
    [SerializeField] private PlayerAnimator _playerAnimator;

    private Jumper _jumper;
    private InputReader _inputReader;
    private Mover _mover;
    private PlayerHealth _playerHealth;
    private CoinsCounter _coinsCount;
    private ItemsCollector _itemsCollector;
    private PlayerAttacker _playerAttacker;
    private PlayerAttackCollider _playerAttackCollider;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _jumper = GetComponent<Jumper>();
        _mover = GetComponent<Mover>();
        _playerHealth = GetComponent<PlayerHealth>();
        _coinsCount = GetComponent<CoinsCounter>();
        _itemsCollector = GetComponent<ItemsCollector>();
        _playerAttackCollider = GetComponent<PlayerAttackCollider>();
        _playerAttacker = GetComponent<PlayerAttacker>();
    }

    private void Start()
    {
        _playerHealth.AssignMaxValue();
    }

    private void OnEnable()
    {
        _itemsCollector.HeallerDetected += _playerHealth.Heal;
        _itemsCollector.CoinCollected += _coinsCount.AddCoin;
        _playerAttackCollider.AttackTargetGot += _playerAttacker.AssignAttackTarget;
        _playerAttackCollider.AttackTargetLost += _playerAttacker.TakeAwayAttackTarget;
    }

    private void OnDisable()
    {
        _itemsCollector.HeallerDetected -= _playerHealth.Heal;
        _itemsCollector.CoinCollected -= _coinsCount.AddCoin;
        _playerAttackCollider.AttackTargetGot -= _playerAttacker.AssignAttackTarget;
        _playerAttackCollider.AttackTargetLost -= _playerAttacker.TakeAwayAttackTarget;
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
        {
            _rotator.Flip(_inputReader.Direction);
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
