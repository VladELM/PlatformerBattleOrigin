using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private FinishText _finishText;

    private Player _player;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        Player player = Instantiate(_playerPrefab, transform.position, Quaternion.identity);
        _cameraMover.AssignTarget(player.transform);

        if (player.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.BecameEmpty += Disable;
            playerHealth.BecameEmpty += _finishText.Show;
        }

        _player = player;
    }

    private void Disable()
    {
        _player.gameObject.SetActive(false);
    }
}
