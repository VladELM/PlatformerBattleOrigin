using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private LayerMask _detectingLayer;
    [SerializeField] private float _rayDistance;
    [SerializeField] private float _speed;

    private Coroutine _coroutine;
    private List<Transform> _targets;
    private Transform _currentTarget;
    private int _targetIndex;

    public float RayDistance => _rayDistance;

    public event Action<float> PatrolTargetChanged;
    public event Action HauntTargetDetected;
    public event Action<Transform> HauntTargetGot;

    public void Initialize(List<Transform> targets)
    {
        _targets = targets;
        _targetIndex = 0;
        _currentTarget = GetTarget();
        PatrolTargetChanged?.Invoke(DirectionCalculator.GetDirection(transform.position.x,
                                                                    _currentTarget.position.x));
    }

    public void StartPatrolling()
    {
        PatrolTargetChanged?.Invoke(DirectionCalculator.GetDirection(transform.position.x,
                                                                    _currentTarget.position.x));
        _coroutine = StartCoroutine(Patrolling());
    }

    public void StopPatrolling()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator Patrolling()
    {
        while (enabled)
        {
            yield return null;

            if (IsHostileTargetDetected() == false)
                Patrol();
            else
                break;
        }
    }

    private bool IsHostileTargetDetected()
    {
        bool isHostileTarget = false;
        RaycastHit2D hit = Physics2D.Raycast(_rayOrigin.position, _rayOrigin.right,
                                                _rayDistance, _detectingLayer);

        if (hit.collider)
        {
            if (hit.collider.TryGetComponent(out Player component))
            {
                HauntTargetDetected?.Invoke();
                HauntTargetGot?.Invoke(component.transform);
                isHostileTarget = true;
            }
        }

        return isHostileTarget;
    }

    private void Patrol()
    {
        Vector3 targetPosition = new Vector3(_currentTarget.position.x,
                                        GetEqualizedTargetY(),
                                    _currentTarget.position.z);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

        if (IsDistanceZero())
        {
            _currentTarget = GetTarget();
            PatrolTargetChanged?.Invoke(DirectionCalculator.GetDirection(transform.position.x,
                                                                    _currentTarget.position.x));
        }
    }

    private float GetEqualizedTargetY()
    {
        float offsetY = Math.Abs(Math.Abs(_currentTarget.position.y) - Math.Abs(transform.position.y));

        float equalizedTargetY = 0;

        if (_currentTarget.position.y > transform.position.y)
            equalizedTargetY = _currentTarget.position.y - offsetY;
        else if (_currentTarget.position.y < transform.position.y)
            equalizedTargetY = _currentTarget.position.y + offsetY;
        else if (_currentTarget.position.y == transform.position.y)
            equalizedTargetY = offsetY;

        return equalizedTargetY;
    }

    private bool IsDistanceZero()
    {
        bool isZero = false;
        float offset = Math.Abs(_currentTarget.position.x) - Math.Abs(transform.position.x);

        if (offset == 0)
            isZero = true;

        return isZero;
    }

    private Transform GetTarget()
    {
        if (_targetIndex == _targets.Count)
            _targetIndex = 0;

        Transform target = _targets[_targetIndex];
        _targetIndex++;

        return target;
    }
}
