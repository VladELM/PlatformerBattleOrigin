using UnityEngine;

public class EnemyAlertSign : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _alertSignSprite;

    public void TurnOnAlertTarget()
    {
        _spriteRenderer.sprite = _alertSignSprite;
    }

    public void TurnOffAlertTarget()
    {
        _spriteRenderer.sprite = null;
    }
}
