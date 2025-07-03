using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private string _attackingParameter;
    [SerializeField] private string _deathParameter;

    private Animator _animator;
    private int _attackingParameterHashed;
    private int _deathParameterHashed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _attackingParameterHashed = Animator.StringToHash(_attackingParameter);
        _deathParameterHashed = Animator.StringToHash(_deathParameter);
    }

    public void TurnOnAttackAnimation()
    {
        _animator.SetBool(_attackingParameterHashed, true);
    }

    public void TurnOffAttackAnimation()
    {
        _animator.SetBool(_attackingParameterHashed, false);
    }

    public void TurnOnDeathAnimation()
    {
        _animator.SetBool(_deathParameterHashed, true);
    }
}
