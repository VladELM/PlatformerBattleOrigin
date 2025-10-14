using System;
using System.Collections;
using UnityEngine;

public class VampirismCounter : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _actionTime;
    [SerializeField] private float _chargeTime;

    private WaitForSeconds _delay = new WaitForSeconds(1);
    private bool _isCharged;
    private bool _IsCoroutineWorking;

    public event Action<float> TimeChanged;
    public event Action VampirismStarted;
    public event Action VampirismFinished;
    public event Action ChargingFinished;

    private void Start()
    {
        _isCharged = true;
        TimeChanged?.Invoke(_actionTime);
        _IsCoroutineWorking = false;
    }

    private void Update()
    {
        if (_isCharged && _IsCoroutineWorking == false)
        {
            if (_inputReader.IsVampirism)
            {
                _IsCoroutineWorking = true;
                StartCoroutine(Vampiring());
            }
        }
        else if (_isCharged == false && _IsCoroutineWorking)
        {
            StartCoroutine(Charging());
        }
    }

    private IEnumerator Vampiring()
    {
        bool isTimeOn = true;
        float remainingTime = _actionTime;
        VampirismStarted?.Invoke();

        while (isTimeOn)
        {
            yield return _delay;

            remainingTime -= 1f;

            if (remainingTime > 0)
                TimeChanged?.Invoke(remainingTime);
            else if (remainingTime <= 0)
                isTimeOn = false;

            if (isTimeOn == false)
            {
                TimeChanged?.Invoke(_chargeTime);
                VampirismFinished?.Invoke();
                _isCharged = false;
            }
        }
    }

    private IEnumerator Charging()
    {
        bool isTimeOn = true;
        float remainingTime = _chargeTime;
        _IsCoroutineWorking = false;

        while (isTimeOn)
        {
            yield return _delay;

            remainingTime -= 1f;

            if (remainingTime > 0)
                TimeChanged?.Invoke(remainingTime);
            else if (remainingTime <= 0)
                isTimeOn = false;

            if (isTimeOn == false)
            {
                TimeChanged?.Invoke(_actionTime);
                ChargingFinished?.Invoke();
                _isCharged = true;
            }
        }
    }
}
