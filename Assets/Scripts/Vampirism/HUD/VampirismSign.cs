using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class VampirismSign : MonoBehaviour
{
    [SerializeField] private VampirismCounter _vampirismCounter;

    [SerializeField] private Sprite _chargedVampirism;
    [SerializeField] private Sprite _unchargedVampirism;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _vampirismCounter.ChargingFinished += SetChargedSprite;
        _vampirismCounter.VampirismFinished += SetUnchargedSprite;
    }

    void Start()
    {
        _image.sprite = _chargedVampirism;
    }

    private void OnDisable()
    {
        _vampirismCounter.ChargingFinished -= SetChargedSprite;
        _vampirismCounter.VampirismFinished -= SetUnchargedSprite;
    }

    private void SetChargedSprite()
    {
        _image.sprite = _chargedVampirism;
    }

    private void SetUnchargedSprite()
    {
        _image.sprite = _unchargedVampirism;
    }
}

