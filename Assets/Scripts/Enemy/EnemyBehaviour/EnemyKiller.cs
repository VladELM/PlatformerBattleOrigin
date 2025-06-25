using System.Collections;
using UnityEngine;

public class EnemyKiller : MonoBehaviour
{
    [SerializeField] private float _delay;

    public void Kill()
    {
        StartCoroutine(Killing());
    }

    private IEnumerator Killing()
    {
        yield return new WaitForSeconds(_delay);

        Destroy(gameObject);
    }
}
