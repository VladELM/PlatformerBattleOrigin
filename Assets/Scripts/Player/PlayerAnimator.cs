using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private string _movingParameter;

    private Animator _animator;
    private int _movingParameterHashed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movingParameterHashed =  Animator.StringToHash(_movingParameter);
    }

    public void TurnOnMoveAnimation()
    {
        _animator.SetBool(_movingParameterHashed, true);
    }

    public void TurnOnIdleAnimation()
    {
        _animator.SetBool(_movingParameterHashed, false);
    }
}
