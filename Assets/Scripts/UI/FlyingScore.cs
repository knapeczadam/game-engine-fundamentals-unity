using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace GEF
{
    public class FlyingScore : MonoBehaviour
    {
        #region Properties
        [SerializeField, Range(0.01f, 5.0f)] private float m_speed = 0.3f;
        private Vector3 m_endPosition;
        private float m_accuTime = 0.0f;
        #endregion

        #region Lifecycle
        void Start()
        {
            m_endPosition = FindObjectOfType<ScoreDisplay>().transform.position;
        }

        void Update()
        {
            m_accuTime += Time.deltaTime * m_speed;
            transform.position = Vector3.Lerp(transform.position, m_endPosition, m_accuTime);
            if (m_accuTime >= 1.0f)
            {
                Destroy(gameObject);
            }
        }
        #endregion
    }
}
