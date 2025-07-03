using System;
using UnityEngine;

public class Healler : MonoBehaviour, IHealable, IEnterable
{
    [SerializeField] private int _healPoints;

    public int HealPoints => _healPoints;

    public event Action<Healler> HeallerCollected;

    public void Take(ICollectable collectable)
    {
        collectable.Collect(this);
    }

    public void Initialize(float valueX, float valueY)
    {
        transform.position = new Vector3(valueX, valueY, 0);
    }

    public void DeactivateHealler()
    {
        HeallerCollected?.Invoke(this);
    }
}
