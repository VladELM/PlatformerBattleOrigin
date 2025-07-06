using System.Collections.Generic;
using UnityEngine;

public class RendererOperator : MonoBehaviour
{
    private List<SpriteRenderer> _spriteRenderers;

    public void TurnOffRenderer()
    {
        int amount = _spriteRenderers.Count;

        for (int i = 0; i < amount; i++)
            _spriteRenderers[i].enabled = false;
    }

    public void TurnOnRenderer()
    {
        int amount = _spriteRenderers.Count;

        for (int i = 0; i < amount; i++)
            _spriteRenderers[i].enabled = true;
    }

    public void FillRenderersList()
    {
        _spriteRenderers = new List<SpriteRenderer>();
        int amount = transform.childCount;

        for (int i = 0; i < amount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out SpriteRenderer spriteRenderer))
                _spriteRenderers.Add(spriteRenderer);
        }
    }
}
