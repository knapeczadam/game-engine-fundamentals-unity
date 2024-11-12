using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Dictionary<int, int> m_spawnedObjects = new Dictionary<int, int>();

    private readonly Dictionary<int, int> m_maxSpawnedObjects = new Dictionary<int, int>()
    {
        {1, 5},
        {2, 3},
        {3, 1}
    };
    
    #region SINGLETON INSTANCE
    private static SpawnManager m_instance = null;
    public static SpawnManager Instance
    {
        get
        {
            if (m_instance == null && !ApplicationQuitting)
            {
                m_instance = FindObjectOfType<SpawnManager>();
                if (m_instance == null)
                {
                    GameObject newInstance = new GameObject("Singleton_SpawnManager");
                    m_instance = newInstance.AddComponent<SpawnManager>();
                }
            }
            return m_instance;
        }
    }

    public static bool Exists
    {
        get
        {
            return m_instance;
        }
    }

    public static bool ApplicationQuitting = false;

    protected void OnApplicationQuit()
    {
        ApplicationQuitting = true;
    }

    private void Awake()
    {
        // DontDestroyOnLoad(gameObject);
        if (m_instance == null)
        {
            m_instance = this;
        }
        else if (m_instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected void OnDestroy()
    {
        if (m_instance == this)
        {
            m_instance = null;
        }
    }
    
    private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
    public void RegisterSpawnPoint(SpawnPoint spawnPoint)
    {
        if (!_spawnPoints.Contains(spawnPoint))
        {
            _spawnPoints.Add(spawnPoint);
        }
    }
    
    public void UnregisterSpawnPoint(SpawnPoint spawnPoint)
    {
        _spawnPoints.Remove(spawnPoint);
    }

    private void Update()
    {
        // remove any objects that are null
        _spawnPoints.RemoveAll(spawnPoint => !spawnPoint);
    }

    public void SpawnWave()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            var spawnId = spawnPoint.GetComponent<MyID>().GetID();
            int maxSpawned = m_maxSpawnedObjects[spawnId];
            if (!m_spawnedObjects.ContainsKey(spawnId))
            {
                m_spawnedObjects.Add(spawnId, 0);
            }
            if (m_spawnedObjects[spawnId] >= maxSpawned)
            {
                continue;
            }
            spawnPoint.Spawn();
            m_spawnedObjects[spawnId]++;
        }
    }
    
    public void Despawn(int id)
    {
        if (m_spawnedObjects.ContainsKey(id))
        {
            m_spawnedObjects[id]--;
        }
    }

    #endregion
}
