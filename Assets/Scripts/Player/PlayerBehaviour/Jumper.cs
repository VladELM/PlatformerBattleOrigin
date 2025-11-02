using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    public bool IsJumping => !_groundChecker.IsGroundTouched;

    public void Jump()
    {
        if (_groundChecker.IsGroundTouched)
            _rigidbody2D.AddForce(Vector2.up * _jumpForce);
    }
}
