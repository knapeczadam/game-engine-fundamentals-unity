using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] 
    private GameObject _spawnTemplate = null;

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
        return Instantiate(_spawnTemplate, transform.position, transform.rotation);
    }
}
