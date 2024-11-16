using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GEF
{
    public class Rotator : MonoBehaviour
    {
        #region Properties
        [SerializeField, Range(0.01f, 5.0f)] private float m_speed = 0.1f;
        private Vector3 m_rotation;
        #endregion

        #region Lifecycle
        private void Start()
        {
            m_rotation.x = Random.Range(0, 360);
            m_rotation.y = Random.Range(0, 360);
            m_rotation.z = Random.Range(0, 360);
        }

        private void Update()
        {
            transform.Rotate(m_rotation * (Time.deltaTime * m_speed));
        }
        #endregion
    }
}