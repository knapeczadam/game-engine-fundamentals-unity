using UnityEngine;
using UnityEngine.Serialization;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject m_spawnTemplate = null;

    private void OnEnable()
    {
        SpawnManager.Instance.RegisterSpawnPoint(this);

        SpawnManager.Instance.SpawnWave();
    }

    private void OnDisable()
    {
        if (SpawnManager.Exists)
        { 
            SpawnManager.Instance.UnregisterSpawnPoint(this);  
        } 
    }

    public GameObject Spawn()
    {
        return Instantiate(m_spawnTemplate, transform.position, transform.rotation);
    }
}
