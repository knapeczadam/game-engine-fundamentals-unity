using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class FlyingScore : MonoBehaviour
{
    
    private Vector3 m_endPosition;
    private float m_accuTime = 0.0f;
    private float m_speed = 0.3f;
    
    // Start is called before the first frame update
    void Start()
    {
        m_endPosition = FindObjectOfType<ScoreDisplay>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        m_accuTime += Time.deltaTime * m_speed;
        transform.position = Vector3.Lerp(transform.position, m_endPosition, m_accuTime);
        if (m_accuTime >= 1.0f)
        {
            Destroy(gameObject);
        }
    }
}
