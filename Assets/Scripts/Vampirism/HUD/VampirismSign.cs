using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class VampirismSign : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;

    [SerializeField] private Sprite _chargedVampirism;
    [SerializeField] private Sprite _unchargedVampirism;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _vampirism.ChargingFinished += SetChargedSprite;
        _vampirism.VampirismFinished += SetUnchargedSprite;
    }

    void Start()
    {
        _image.sprite = _chargedVampirism;
    }

    private void OnDisable()
    {
        _vampirism.ChargingFinished -= SetChargedSprite;
        _vampirism.VampirismFinished -= SetUnchargedSprite;
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

