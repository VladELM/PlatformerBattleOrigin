using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    public void Move(float direction)
    {
        transform.Translate(direction * _speed * Time.deltaTime, 0, 0);
    }
}
