using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private List<Transform> _healthBarElements;

    private void Awake()
    {
        _healthBarElements = new List<Transform>();

        int childrenAmount = transform.childCount;

        for (int i = 0; i < childrenAmount; i++)
            _healthBarElements.Add(transform.GetChild(i));
    }

    public void SetState(bool value)
    {
        for (int i = 0; i < _healthBarElements.Count; i++)
            _healthBarElements[i].gameObject.SetActive(value);
    }
}
