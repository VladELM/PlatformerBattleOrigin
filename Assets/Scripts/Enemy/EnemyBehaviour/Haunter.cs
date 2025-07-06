using System;
using System.Collections;
using UnityEngine;

public class Haunter : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _losingDistance;

    private Coroutine _coroutine;
    private Transform _hauntTarget;

    public event Action<float> HauntingTargetPositionChanged;
    public event Action HauntingTargetLost;

    public void Initialize(float losingDistance)
    {
        if (_losingDistance <= losingDistance)
            _losingDistance = losingDistance + 1;
    }

    public void SetHauntTarget(Transform hautTarget)
    {
        _hauntTarget = hautTarget;
    }

    public void StartHaunting(Transform hauntTarget)
    {
        _hauntTarget = hauntTarget;
        _coroutine = StartCoroutine(Haunting());
    }

    public void StopHaunting()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator Haunting()
    {
        while (enabled)
        {
            yield return null;

            HauntingTargetPositionChanged?.Invoke(_hauntTarget.position.x);

            if (IsLoseDistance())
            {
                StopToHaunt();
                break;
            }
            else
            {
                Haunt();
            }
        }
    }

    private bool IsLoseDistance()
    {
        float offsetX = GetAbsoluteValue(_hauntTarget.position.x, transform.position.x);
        float offsetY = GetAbsoluteValue(_hauntTarget.position.y, transform.position.y);

        int power = 2;
        float offsetSqrt = (float)Math.Pow(offsetX, power) + (float)Math.Pow(offsetY, power);
        float losingDistancePow = (float)Math.Pow(_losingDistance, power);
        bool isLosed = false;

        if (offsetSqrt < losingDistancePow)
            isLosed = false;
        else if (offsetSqrt >= losingDistancePow)
            isLosed = true;

            return isLosed;
    }

    private void Haunt()
    {
        Vector3 targetPosition = new Vector3(_hauntTarget.position.x, GetEqualizedTargetY(), _hauntTarget.position.z);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
    }

    private void StopToHaunt()
    {
        HauntingTargetLost?.Invoke();
        _hauntTarget = null;
    }

    private float GetEqualizedTargetY()
    {
        float deltaY = GetAbsoluteValue(_hauntTarget.position.y, transform.position.y);
        float equalizedTargetY = 0;

        if (_hauntTarget.position.y > transform.position.y)
            equalizedTargetY = _hauntTarget.position.y - deltaY;
        else if (_hauntTarget.position.y < transform.position.y)
            equalizedTargetY = _hauntTarget.position.y + deltaY;
        else if (_hauntTarget.position.y == transform.position.y)
            equalizedTargetY = deltaY;

        return equalizedTargetY;
    }

    private float GetAbsoluteValue(float firstValue, float secondValue)
    {
        return Math.Abs(Math.Abs(firstValue) - Math.Abs(secondValue));
    }
}
