using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthBar : HealthView
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    protected override void OnMaxHealthValueAssigned(float value)
    {
        _image.fillAmount = value;
    }

    protected override void OnHealthValueChanged(float value)
    {

    }
}
