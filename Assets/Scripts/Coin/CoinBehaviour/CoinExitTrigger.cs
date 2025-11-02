using System;
using UnityEngine;

public class CoinExitTrigger : MonoBehaviour
{
    public event Action Exited;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.parent.TryGetComponent(out ItemsCollector itemsCollector))
            Exited?.Invoke();
    }
}
