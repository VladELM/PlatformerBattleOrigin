using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CharacterHealth : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _health;

    public void TakeDamage(float incomingDamage)
    {
        _health -= incomingDamage;
        Debug.Log(_health);
    }
}
