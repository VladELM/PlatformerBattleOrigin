using UnityEngine;

public class ItemsEnterTrigger : MonoBehaviour
{
    private ItemsCollector _itemsCollector;

    private void Start()
    {
        _itemsCollector = GetComponent<ItemsCollector>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_itemsCollector != null)
        {
            if (collider.TryGetComponent(out IEnterable enterable))
                enterable.Take(_itemsCollector);
        }
    }
}
