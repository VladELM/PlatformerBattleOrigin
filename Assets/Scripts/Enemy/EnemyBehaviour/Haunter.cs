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

    public void AssignHauntTarget(Transform hautTarget)
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

            float offset = Vector3.Distance(_hauntTarget.position, transform.position);

            if (offset < _losingDistance)
            {
                Haunt();
            }
            else if (offset >= _losingDistance)
            {
                StopToHaunt();
                break;
            }
        }
    }

    private void Haunt()
    {
        Vector3 targetPosition = new Vector3(_hauntTarget.position.x,
                        GetEqualizedTargetY(_hauntTarget.position.y),
                        _hauntTarget.position.z);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
    }

    private void StopToHaunt()
    {
        HauntingTargetLost?.Invoke();
        _hauntTarget = null;
    }

    private float GetEqualizedTargetY(float targetY)
    {
        float offsetY = Math.Abs(Math.Abs(targetY) - Math.Abs(transform.position.y));

        float equalizedTargetY = 0;

        if (targetY > transform.position.y)
            equalizedTargetY = targetY - offsetY;
        else if (targetY < transform.position.y)
            equalizedTargetY = targetY + offsetY;
        else if (targetY == transform.position.y)
            equalizedTargetY = offsetY;

            return equalizedTargetY;
    }
}
