using UnityEngine;

public class ItemsEnterTrigger : MonoBehaviour
{
    [SerializeField] private ItemsCollector _itemsCollector;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_itemsCollector != null)
        {
            if (collider.TryGetComponent(out IEnterable enterable))
                enterable.Take(_itemsCollector);
        }
    }
}
