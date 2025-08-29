using UnityEngine;

[RequireComponent(typeof(HealthBarHealth))]
public class Damager : HealthChanger
{
    [SerializeField] private int _damage;

    protected override void HandleClickButton()
    {
        Health.TakeDamage(_damage);
    }
}
