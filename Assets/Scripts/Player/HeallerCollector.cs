using System;
using UnityEngine;

public class HeallerCollector : MonoBehaviour
{
    public event Action<int, IHealable> HeallerDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Healler healler))
            HeallerDetected?.Invoke(healler.HealPoints, healler);
    }
}
