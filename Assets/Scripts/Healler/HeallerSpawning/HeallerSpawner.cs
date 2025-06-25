using static UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeallerSpawner : MonoBehaviour
{
    [SerializeField] private Healler _heallerPrefab;
    [SerializeField] private int _maxPoolSize;
    [SerializeField] private int _activeHeallersAmount;
    [SerializeField] private int _minDelay;
    [SerializeField] private int _maxDelay;
    [SerializeField] private List<HeallerSpawnPoint> _heallerSpawnPoints;

    private Queue<Healler> _heallersPool;

    private void Awake()
    {
        _heallersPool = new Queue<Healler>();
        int spawnPointsAmount = _heallerSpawnPoints.Count;
        
        for (int i = 0; i < spawnPointsAmount; i++)
            _heallerSpawnPoints[i].Initialize();

        List<Healler> heallers = new List<Healler>();
        int commonAmount = _maxPoolSize + _activeHeallersAmount;

        for (int i = 0; i < commonAmount; i++)
        {
            Healler healler = Instantiate(_heallerPrefab);
            int index = Range(0, spawnPointsAmount);
            healler.Initialize(_heallerSpawnPoints[index].GetRandomValueX(),
                                _heallerSpawnPoints[index].ValueY);
            
            if (_heallersPool.Count <= _activeHeallersAmount)
            {
                healler.HeallerCollected += GiveBackHealler;
            }
            else
            {
                healler.gameObject.SetActive(false);
                _heallersPool.Enqueue(healler);
            }
        }
    }

    private void GiveBackHealler(Healler healler)
    {
        healler.HeallerCollected -= GiveBackHealler;
        healler.gameObject.SetActive(false);
        _heallersPool.Enqueue(healler);

        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        yield return new WaitForSeconds(Range(_minDelay, _maxDelay));

        GetHealler();
    }

    private void GetHealler()
    {
        Healler healler = _heallersPool.Dequeue();

        healler.HeallerCollected += GiveBackHealler;

        int index = Range(0, _heallerSpawnPoints.Count - 1);
        healler.Initialize(_heallerSpawnPoints[index].GetRandomValueX(),
                            _heallerSpawnPoints[index].ValueY);
        healler.gameObject.SetActive(true);
    }

#if UNITY_EDITOR
    [ContextMenu("FillSpawnPointsList")]
    private void FillSpawnPointsList()
    {
        int childAmount = transform.childCount;

        for (int i = 0; i < childAmount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.TryGetComponent(out HeallerSpawnPoint heallerSpawnPoint))
                _heallerSpawnPoints.Add(heallerSpawnPoint);
        }
    }

#endif
}