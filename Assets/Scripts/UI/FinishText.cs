using UnityEngine;

public class FinishText : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private RectTransform _textTransform;

    public void Show()
    {
        _textTransform.position = Camera.main.WorldToScreenPoint(_target.position);
        _textTransform.gameObject.SetActive(true);
    }
}
