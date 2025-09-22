using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private Transform _target;

    public void AssignTarget(Transform target)
    {
        _target = target;
    }

    private void LateUpdate()
    {
        if (_target != null)
            transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
    }
}
