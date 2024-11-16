using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace GEF
{
    public class WorldTime : MonoBehaviour
    {
        #region Fields
        public event EventHandler<TimeSpan> WorldTimeChanged;
        #endregion

        #region Properties
        [SerializeField, Range(1.0f, 600.0f)] private float m_dayLength = 30.0f; // in seconds
        private TimeSpan m_currentTime;
        private float m_minuteLength => m_dayLength / WorldTimeConstants.MINUTES_IN_DAY;
        #endregion

        #region Lifecycle
        public void Reset()
        {
            m_currentTime = TimeSpan.Zero;
        }
        
        private void Start()
        {
            StartCoroutine(AddMinute());
        }
        #endregion

        #region Methods
        private IEnumerator AddMinute()
        {
            WorldTimeChanged?.Invoke(this, m_currentTime);
            m_currentTime += TimeSpan.FromMinutes(1);
            yield return new WaitForSeconds(m_minuteLength);
            StartCoroutine(AddMinute());
        }
        #endregion
    }
}
