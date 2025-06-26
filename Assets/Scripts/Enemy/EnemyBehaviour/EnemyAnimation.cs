using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private string _attackingParameter;
    [SerializeField] private string _deathParameter;

    private Animator _animator;
    private int _attackingParameterHashed;
    private bool _isAttacking;
    private int _deathParameterHashed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _attackingParameterHashed = Animator.StringToHash(_attackingParameter);
        _isAttacking = false;

        _deathParameterHashed = Animator.StringToHash(_deathParameter);
    }

    public void ToggleAttackAnimation()
    {
        _isAttacking = !_isAttacking;
        _animator.SetBool(_attackingParameterHashed, _isAttacking);
    }

    public void PlayDeathAnimation()
    {
        _animator.SetBool(_deathParameterHashed, true);
    }
}
