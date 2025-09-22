using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private List<Transform> _healthBar;

    private void Awake()
    {
        _healthBar = new List<Transform>();

        int childrenAmount = transform.childCount;

        for (int i = 0; i < childrenAmount; i++)
            _healthBar.Add(transform.GetChild(i));
    }

    public void SwitchOnHealthBar()
    {
        for (int i = 0; i < _healthBar.Count; i++)
            _healthBar[i].gameObject.SetActive(true);
    }

    public void SwitchOffHealthBar()
    {
        for (int i = 0; i < _healthBar.Count; i++)
            _healthBar[i].gameObject.SetActive(false);
    }
}
