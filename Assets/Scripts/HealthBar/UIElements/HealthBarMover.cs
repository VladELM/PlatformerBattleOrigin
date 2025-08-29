using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HealthBarMover : MonoBehaviour
{
    [SerializeField] private float _offsetY;

    private Transform _target;

    private void LateUpdate()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y + _offsetY, 0);
    }

    public void Initialize(Transform player)
    {
        _target = player;
    }

    public void CleanTarget()
    {
        _target = null;
    }
}
