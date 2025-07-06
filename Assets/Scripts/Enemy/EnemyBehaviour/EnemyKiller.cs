using System;
using System.Collections;
using UnityEngine;

public class EnemyKiller : MonoBehaviour
{
    [SerializeField] private float _additionalDelay;

    private float _delay;

    public event Action<EnemyKiller> Killed;

    public void Initialize(float delay)
    {
        _delay = delay + _additionalDelay;
    }

    public void Kill()
    {
        StartCoroutine(Killing());
    }

    private IEnumerator Killing()
    {
        yield return new WaitForSeconds(_delay);

        Killed?.Invoke(this);
    }
}
