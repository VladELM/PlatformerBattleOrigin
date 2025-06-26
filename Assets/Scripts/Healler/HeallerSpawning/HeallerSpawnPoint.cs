using static UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine;

public class HeallerSpawnPoint : MonoBehaviour
{
    private float _leftValueX;
    private float _rightValueX;

    public float ValueY { get; private set; }

    public void Initialize()
    {
        List<float> values = new List<float>();
        int childAmount = transform.childCount;

        for (int i = 0; i < childAmount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.TryGetComponent(out HeallerSpawnBorder heallerPointBorder))
                values.Add(child.position.x);

            Destroy(child.gameObject);
        }

        if (values[0] < values[1])
        {
            _leftValueX = values[0];
            _rightValueX = values[1];
        }
        else if (values[0] > values[1])
        {
            _leftValueX = values[1];
            _rightValueX = values[0];
        }

        ValueY = transform.position.y;
    }

    public float GetRandomValueX()
    {
        return Range(_leftValueX, _rightValueX);
    }
}
