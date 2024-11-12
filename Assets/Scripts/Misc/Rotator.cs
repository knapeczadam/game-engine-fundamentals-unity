using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Vector3 m_rotation;
    private float m_speed = 0.1f;
    
    void Start()
    {
        m_rotation.x = Random.Range(0, 360);
        m_rotation.y = Random.Range(0, 360);
        m_rotation.z = Random.Range(0, 360);
    }
    
    void Update()
    {
        transform.Rotate(m_rotation * (Time.deltaTime * m_speed));
    }
}
