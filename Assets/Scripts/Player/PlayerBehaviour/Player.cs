using UnityEngine;

[RequireComponent (typeof(Jumper))]
[RequireComponent(typeof(ItemsCollector))]

public class Player : MonoBehaviour
{
    [SerializeField] private Flipper _rotator;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private Health _playerHealth;

    private Jumper _jumper;
    private InputReader _inputReader;
    private Mover _mover;
    private CoinsCounter _coinsCount;
    private ItemsCollector _itemsCollector;
    private PlayerAttacker _playerAttacker;
    private PlayerAttackCollider _playerAttackCollider;

    public Health PlayerHealth { get; private set; }

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _jumper = GetComponent<Jumper>();
        _mover = GetComponent<Mover>();
        _coinsCount = GetComponent<CoinsCounter>();
        _itemsCollector = GetComponent<ItemsCollector>();
        _playerAttackCollider = GetComponent<PlayerAttackCollider>();
        _playerAttacker = GetComponent<PlayerAttacker>();

        PlayerHealth = _playerHealth;
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
