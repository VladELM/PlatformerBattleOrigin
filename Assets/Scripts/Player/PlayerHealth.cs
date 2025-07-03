using System.Collections;
using UnityEngine;

public class PlayerHealth : Health
{
    public void StartMonitorHealth()
    {
        StartCoroutine(MonitoringHealth());
    }

    protected override IEnumerator MonitoringHealth()
    {
        while (enabled)
        {
            yield return null;

            //if (_healthValue <= 0)
            //    Debug.Log("You are undead...");
            //else
            //    Debug.Log("You are alive !");
        }
    }
}
