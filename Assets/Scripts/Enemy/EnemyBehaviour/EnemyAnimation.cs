using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private AnimationClip _deathAnimation;
    [SerializeField] private AnimationClip _ressurectionAnimation;
    [SerializeField] private AnimationClip _attackAnimation;
    [SerializeField] private string _idleParameter;
    [SerializeField] private string _attackingParameter;
    [SerializeField] private string _deathParameter;

    private Animator _animator;
    private int _idleParameterHashed;
    private int _attackingParameterHashed;
    private int _deathParameterHashed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _attackingParameterHashed = Animator.StringToHash(_attackingParameter);
        _deathParameterHashed = Animator.StringToHash(_deathParameter);
        _idleParameterHashed = Animator.StringToHash(_idleParameter);
    }

    public float GetAnimationsLength()
    {
        float commonLength = Mathf.Abs(_deathAnimation.length) +
                            Mathf.Abs(_ressurectionAnimation.length);

        return commonLength;
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

    public void TurnOffDeathAnimation()
    {
        _animator.SetBool(_deathParameterHashed, false);
        _animator.SetBool(_idleParameterHashed, true);
    }
}
