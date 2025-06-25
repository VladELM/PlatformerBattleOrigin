using UnityEngine;

public class EnemyAlertSign : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _alertSignSprite;

    public void SetActiveAlertTarget()
    {
        _spriteRenderer.sprite = _alertSignSprite;
    }

    public void SetUnactiveAlertTarget()
    {
        _spriteRenderer.sprite = null;
    }
}
