using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthBarHealth))]
public abstract class HealthChanger : MonoBehaviour
{
    [SerializeField] private Button _button;

    protected HealthBarHealth Health;

    private void Awake()
    {
        Health = GetComponent<HealthBarHealth>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleClickButton);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleClickButton);
    }

    protected abstract void HandleClickButton();
}
