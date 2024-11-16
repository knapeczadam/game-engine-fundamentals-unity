using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroyer : MonoBehaviour
{
    [SerializeField] private float m_time = 1.0f;
        
    void Start()
    {
        Destroy(gameObject, m_time);
    }
}
