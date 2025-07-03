using UnityEngine;

public class EnemyAlertSign : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _alertSignSprite;

    public void TurnOnAlertSign()
    {
        _spriteRenderer.sprite = _alertSignSprite;
    }

    public void TurnOffAlertSign()
    {
        _spriteRenderer.sprite = null;
    }
}
