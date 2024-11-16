using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GEF
{
    [RequireComponent(typeof(Light))]
    public class WorldLight : MonoBehaviour
    {
        #region Properties
        [SerializeField, Range(0.0f, 360.0f)] private float m_startEulerX = 180.0f;
        [SerializeField] private WorldTime m_worldTime = null;
        [SerializeField] private Gradient m_gradient = null;
        private Light m_light = null;
        #endregion

        #region Lifecycle
        private void Awake()
        {
            m_light = GetComponent<Light>();
            m_worldTime.WorldTimeChanged += OnWorldTimeChanged;
        }
        #endregion

        #region Methods
        private void OnWorldTimeChanged(object sender, TimeSpan newTime)
        {
            // Debug.Log(PercentOfDay(newTime));

            // between 0 and 0.5 = night
            // between 0.5 and 1 = day
            if (PercentOfDay(newTime) < 0.5f)
            {
                m_light.intensity = 0.0f;
            }
            else
            {
                m_light.intensity = 1.0f;
            }

            m_light.transform.rotation = Quaternion.Euler(PercentOfDay(newTime) * 360 + m_startEulerX, 0, 0);
            m_light.color = m_gradient.Evaluate(PercentOfDay(newTime));
        }

        private void OnDestroy()
        {
            m_worldTime.WorldTimeChanged -= OnWorldTimeChanged;
        }

        private float PercentOfDay(TimeSpan timeSpan)
        {
            return (float)timeSpan.TotalMinutes % WorldTimeConstants.MINUTES_IN_DAY / WorldTimeConstants.MINUTES_IN_DAY;
        }
        #endregion
    }
}
