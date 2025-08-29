using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthBarStraight : HealthView
{
    [SerializeField] private float _multiplier;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    protected override void OnMaxHealthValueAssigned(float value)
    {

    }

    protected override void OnHealthValueChanged(float value)
    {
        _image.fillAmount = value * _multiplier;
    }
}
