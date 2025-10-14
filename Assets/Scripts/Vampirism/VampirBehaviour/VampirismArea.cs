using UnityEngine;

public class VampirismArea : MonoBehaviour
{
    [SerializeField] private VampirBorderImage _borderImage;
    [SerializeField] private VampirismTrigger _vampirismTrigger;

    private bool _isVampirism;

    private void Start()
    {
        _isVampirism = false;
        SwitchCollider(_isVampirism);
        SwitchBorderImage(_isVampirism);
    }

    public void SwitchArea()
    {
        _isVampirism = !_isVampirism;

        SwitchCollider(_isVampirism);
        SwitchBorderImage(_isVampirism);
    }

    private void SwitchBorderImage(bool isActive)
    {
        if (isActive)
            _borderImage.gameObject.SetActive(true);
        else
            _borderImage.gameObject.SetActive(false);
    }

    private void SwitchCollider(bool isActive)
    {
        if (isActive)
            _vampirismTrigger.gameObject.SetActive(true);
        else
            _vampirismTrigger.gameObject.SetActive(false);
    }
}
