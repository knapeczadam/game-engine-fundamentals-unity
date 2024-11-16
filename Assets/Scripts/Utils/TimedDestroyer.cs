using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GEF
{
    public class TimedDestroyer : MonoBehaviour
    {
        #region Properties
        [SerializeField, Range(0.1f, 10.0f)] private float m_time = 1.0f;
        #endregion

        #region Lifecycle
        private void Start()
        {
            Destroy(gameObject, m_time);
        }
        #endregion
    }
}