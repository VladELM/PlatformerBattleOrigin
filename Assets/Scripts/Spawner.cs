using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected List<Transform> _spawnPoints;

    protected abstract void SpawnOnStart();

#if UNITY_EDITOR
    [ContextMenu("FillSpanwPointsList")]
    private void FillSpawnPointsList()
    {
        int childAmount = transform.childCount;

        for (int i = 0; i < childAmount; i++)
            _spawnPoints.Add(transform.GetChild(i));
    }

#endif
}
