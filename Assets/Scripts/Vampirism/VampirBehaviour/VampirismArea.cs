using UnityEngine;

public class VampirismArea : MonoBehaviour
{
    [SerializeField] private VampirBorderImage _borderImage;
    [SerializeField] private VampirismTrigger _vampirismTrigger;

    private bool _isVampirism;

    private void Awake()
    {
        _isVampirism = false;
        SwitchBorderImage(_isVampirism);
    }

    public void SwitchArea()
    {
        _isVampirism = !_isVampirism;
        SwitchBorderImage(_isVampirism);
    }

    private void SwitchBorderImage(bool isActive)
    {
        if (isActive)
            _borderImage.gameObject.SetActive(true);
        else
            _borderImage.gameObject.SetActive(false);
    }
}
