using System;
using UnityEngine;

public class VampirismTrigger : MonoBehaviour
{
    private Collider2D _collider2D;

    public event Action<IDamageable> PampingTargetGot;
    public event Action<IDamageable> PampingTargetLost;

    private void Awake()
    {
        
       _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collider2D.enabled)
        {
            if (collision.TryGetComponent(out AttackComponent attackComponent))
            {
                if (attackComponent.TryGetComponent(out Health enemyHealth))
                    PampingTargetGot?.Invoke(enemyHealth);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_collider2D.enabled)
        {
            if (collision.TryGetComponent(out AttackComponent attackComponent))
            {
                if (attackComponent.TryGetComponent(out Health enemyHealth))
                    PampingTargetLost?.Invoke(enemyHealth);
            }
        }
    }
}
