using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;

public class Haunter : MonoBehaviour
{
    [SerializeField] private float _speed;

    public void Haunt(Transform hauntTarget)
    {
        Vector3 targetPosition = new Vector3(hauntTarget.position.x,
                                            GetEqualizedTargetY(hauntTarget.position.y),
                                            hauntTarget.position.z);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
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
