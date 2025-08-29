using UnityEngine;

[RequireComponent(typeof(HealthBarHealth))]
public class HeallerThing : HealthChanger
{
    [SerializeField] private int _healPoints;

    protected override void HandleClickButton()
    {
        Health.Heal(_healPoints);
    }
}
