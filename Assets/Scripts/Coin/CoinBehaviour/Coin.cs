using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CoinExitTrigger))]
public class Coin : MonoBehaviour, IEnterable
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _timeToDelay;
    [SerializeField] private int _cost;

    private SpriteRenderer _spriteRenderer;
    private CoinExitTrigger _exitTrigger;
    private Coroutine _coroutine;
    private WaitForSeconds _delay;
    private bool _isTimeOut;

    public int Cost => _cost;
    public bool IsTimeOut => _isTimeOut;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _exitTrigger = GetComponent<CoinExitTrigger>();
        _exitTrigger.Exited += Activate;
        _delay = new WaitForSeconds(_timeToDelay);
        _isTimeOut = true;
    }

    public void Take(ICollectable collectable)
    {
        collectable.Collect(this);
    }

    public void Disactivate()
    {
        if (_isTimeOut)
            _spriteRenderer.sprite = null;
        else
            StopCoroutine(_coroutine);
    }

    private void Activate()
    {
        _isTimeOut = false;
        _coroutine = StartCoroutine(Activating());
    }

    private IEnumerator Activating()
    {
        yield return _delay;

        _isTimeOut = true;
        _spriteRenderer.sprite = _sprite;
    }
}
