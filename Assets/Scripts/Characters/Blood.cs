using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    [SerializeField]
    private float _minTimeToDestroy = 3f;
    [SerializeField]
    private float _maxTimeToDestroy = 10f;
    void Start()
    {
        // Randomly destroy the blood effect after a random time
        if (Random.Range(0, 1) == 1)
        {
            var randomTime = Random.Range(_minTimeToDestroy, _maxTimeToDestroy);
            Destroy(gameObject, randomTime);
        }
    }
}
