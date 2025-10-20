using System;
using TMPro;
using UnityEngine;

public class VampirismText : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _vampirism.TimeChanged += SetText;
    }

    private void OnDisable()
    {
        _vampirism.TimeChanged -= SetText;
    }

    private void SetText(float time)
    {
        _text.text = Convert.ToString(time);
    }
}
