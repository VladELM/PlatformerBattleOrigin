using UnityEngine;

[RequireComponent(typeof(ItemsCollector))]
public class ItemsEnterTrigger : MonoBehaviour
{
    private ItemsCollector _itemsCollector;

    private void Start()
    {
        _itemsCollector = GetComponent<ItemsCollector>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out IEnterable enterable))
            enterable.Take(_itemsCollector);
    }
}
