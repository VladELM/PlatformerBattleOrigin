using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthBarSmooth : HealthView
{
    [SerializeField] private float _changingTime;
    [SerializeField] private float _changingValue;
    [SerializeField] private float _multiplier;

    private Image _image;
    private bool _isChanging;
    private float _currentHealth;
    private float _targetHealth;
    private WaitForSeconds _changingDelay;
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

        _targetHealth = targetHealth;
        _isChanging = true;
        _coroutine = StartCoroutine(HealthChanging(_changingDelay));
    }

    private IEnumerator HealthChanging(WaitForSeconds delay)
    {
        while (enabled)
        {
            yield return delay;

            if (_targetHealth == 0)
                _image.fillAmount = 0;
            else if (_targetHealth > 0)
                _image.fillAmount = Mathf.MoveTowards(_image.fillAmount * _multiplier, _targetHealth, _changingValue * Time.deltaTime) / _multiplier;

            if (_currentHealth == _targetHealth)
                break;
        }
    }
}
