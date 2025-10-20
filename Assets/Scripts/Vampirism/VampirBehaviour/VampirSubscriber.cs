using UnityEngine;

[RequireComponent(typeof(VampirismArea))]
[RequireComponent(typeof(Vampirism))]
public class VampirSubscriber : MonoBehaviour
{
    [SerializeField] private VampirismTrigger _trigger;
    [SerializeField] private Health _health;
    
    private VampirismArea _area;
    private Vampirism _vampirism;

    private void Awake()
    {
        _area = GetComponent<VampirismArea>();
        _vampirism = GetComponent<Vampirism>();
    }

    private void OnEnable()
    {
        _vampirism.VampirismStarted += _trigger.AssigneActivityState;
        _vampirism.VampirismFinished += _trigger.AssigneActivityState;

        _vampirism.VampirismStarted += _area.SwitchArea;
        _vampirism.VampirismFinished += _area.SwitchArea;

        _trigger.PumpingTargetGot += _vampirism.AssigneCurrentTarget;

        _vampirism.HealthPumped += _health.Heal;
    }

    private void OnDisable()
    {
        _vampirism.VampirismStarted -= _trigger.AssigneActivityState;
        _vampirism.VampirismFinished -= _trigger.AssigneActivityState;

        _vampirism.VampirismStarted -= _area.SwitchArea;
        _vampirism.VampirismFinished -= _area.SwitchArea;

        _trigger.PumpingTargetGot -= _vampirism.AssigneCurrentTarget;

        _vampirism.HealthPumped -= _health.Heal;
    }
}
