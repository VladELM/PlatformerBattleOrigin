using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampir : MonoBehaviour
{
    [SerializeField] private float _vampirismDamage;

    private List<IDamageable> _enemies;

    private void Start()
    {
        _enemies = new List<IDamageable>();
        StartCoroutine(Pamping());
    }

    public void AddToList(IDamageable enemy)
    {
        _enemies.Add(enemy);
    }

    private IEnumerator Pamping()
    {
        yield return null;

        if (_enemies.Count > 0)
        {
            for (int i = 0; i < _enemies.Count; i++)
                _enemies[i].TakeDamage(_vampirismDamage);
        }
    }
}
