using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : CharacterHealth
{
    public event Action HealthBecameEmpty;

    public void IsHealthAboveZero()
    {
        if (_health <= 0)
            HealthBecameEmpty?.Invoke();
    }
}
