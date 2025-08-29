using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class HealthText : HealthView
{
    private TMP_Text _text;
    private string _textPattern;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    protected override void OnMaxHealthValueAssigned(float value)
    {
        SetTextPattern(value);
    }

    protected override void OnHealthValueChanged(float value)
    {
        SetText(value);
    }

    private void SetTextPattern(float maxValue)
    {
        _textPattern = " / " + maxValue.ToString();
        SetText(maxValue);
    }

    protected void SetText(float value)
    {
        _text.text = Convert.ToInt32(value) + _textPattern;
    }
}
