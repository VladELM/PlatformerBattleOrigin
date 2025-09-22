using UnityEngine;


[RequireComponent (typeof(SpriteRenderer))]
public class EnemyAlertSign : MonoBehaviour
{
    [SerializeField] private Sprite _alertSignSprite;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TurnOnAlertSign()
    {
        _spriteRenderer.sprite = _alertSignSprite;
    }

    public void TurnOffAlertSign()
    {
        _spriteRenderer.sprite = null;
    }
}
