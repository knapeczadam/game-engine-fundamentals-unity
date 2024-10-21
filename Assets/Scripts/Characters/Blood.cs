using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    [SerializeField] private float _minTimeToDestroy = 3.0f;
    [SerializeField] private float _maxTimeToDestroy = 10.0f;

    private void Start()
    {
        // If the random number is 1, destroy the object after a random time between _minTimeToDestroy and _maxTimeToDestroy
        if (Random.Range(0, 1) == 1)
        {
            var randomTime = Random.Range(_minTimeToDestroy, _maxTimeToDestroy);
            Destroy(gameObject, randomTime);
        }
    }
}
