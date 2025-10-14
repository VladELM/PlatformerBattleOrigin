using UnityEngine;

[RequireComponent(typeof(VampirismCounter))]
[RequireComponent(typeof(VampirismArea))]
[RequireComponent(typeof(VampirismPumper))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private VampirismTrigger _trigger;
    [SerializeField] private Health _health;
    
    private VampirismCounter _counter;
    private VampirismArea _area;
    private VampirismPumper _pumper;

    private void Awake()
    {
        _counter = GetComponent<VampirismCounter>();
        _area = GetComponent<VampirismArea>();
        _pumper = GetComponent<VampirismPumper>();
    }

    private void OnEnable()
    {
        _counter.VampirismStarted += _area.SwitchArea;
        _counter.VampirismFinished += _area.SwitchArea;

        _trigger.PampingTargetGot += PumpHealth;
        _pumper.HealthPumped += _health.Heal;
    }

    private void OnDisable()
    {
        _counter.VampirismStarted -= _area.SwitchArea;
        _counter.VampirismFinished -= _area.SwitchArea;

        _trigger.PampingTargetGot -= PumpHealth;
        _pumper.HealthPumped -= _health.Heal;
    }

    private void PumpHealth(bool isEnemyHealthAboveZero, IDamageable pumpTarget)
    {
        if (isEnemyHealthAboveZero && _health.IsHealthNotFull)
            _pumper.Pump(pumpTarget);
    }
}
