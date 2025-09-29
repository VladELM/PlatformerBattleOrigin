using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthBarSmooth : HealthView
{
    [SerializeField] private float _changingValue;
    [SerializeField] private float _multiplier;

    private Image _image;
    private bool _isChanging;
    private Coroutine _coroutine;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    protected override void OnMaxHealthValueAssigned(float maxValue)
    {
        _multiplier = maxValue;
        _image.fillAmount = maxValue / _multiplier;
        _maxValue = _image.fillAmount;
    }

    protected override void OnHealthValueChanged(float value)
    {
        StartChangeHealth(value);
    }

    protected override void Restore()
    {
        _image.fillAmount = _maxValue;
    }

    private void StartChangeHealth(float targetHealth)
    {
        if (_isChanging)
        {
            _isChanging = false;
            StopCoroutine(_coroutine);
        }

        _isChanging = true;
        _coroutine = StartCoroutine(ChangingHealth(targetHealth));
    }

    private IEnumerator ChangingHealth(float targetHealth)
    {
        while (enabled)
        {
            yield return null;

            _image.fillAmount = Mathf.MoveTowards(_image.fillAmount * _multiplier, targetHealth, _changingValue * Time.deltaTime) / _multiplier;
        }
    }
}
