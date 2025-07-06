using UnityEngine;

public class Flipper : MonoBehaviour
{
    private float _rightRotationY;
    private float _leftRotationY = 180f;

    private void Awake()
    {
        _rightRotationY = transform.rotation.y;
    }

    public void Flip(float direction)
    {
        float rotationY = _rightRotationY;

        if (direction < 0)
            rotationY = _leftRotationY;
        else if (direction > 0)
            rotationY = _rightRotationY;

        transform.rotation = Quaternion.Euler(transform.rotation.x, rotationY, transform.rotation.z);
    }
}
