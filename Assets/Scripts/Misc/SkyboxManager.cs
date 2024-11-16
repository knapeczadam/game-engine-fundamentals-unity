using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace GEF
{
    public class SkyboxManager : MonoBehaviour
    {
        #region Properties
        [SerializeField] private Texture2D m_skyboxNight   = null;
        [SerializeField] private Texture2D m_skyboxSunrise = null;
        [SerializeField] private Texture2D m_skyboxDay     = null;
        [SerializeField] private Texture2D m_skyboxSunset  = null;
        [SerializeField, Range(0.01f, 5.0f)] float m_time = 1.0f;
        #endregion

        #region Lifecycle
        public void Reset()
        {
            RenderSettings.skybox.SetTexture("_Texture1", m_skyboxNight);
            RenderSettings.skybox.SetTexture("_Texture2", m_skyboxNight);
            RenderSettings.skybox.SetFloat("_Blend", 0);
        }
        #endregion

        #region Public Methods
        public IEnumerator LerpSkybox(Texture2D a, Texture2D b, float time)
        {
            RenderSettings.skybox.SetTexture("_Texture1", a);
            RenderSettings.skybox.SetTexture("_Texture2", b);
            RenderSettings.skybox.SetFloat("_Blend", 0);
            for (float i = 0; i < time; i += Time.deltaTime)
            {
                RenderSettings.skybox.SetFloat("_Blend", i / time);
                yield return null;
            }

            RenderSettings.skybox.SetTexture("_Texture1", b);
        }

        public void FromNightToDay()
        {
            StartCoroutine(LerpSkybox(m_skyboxNight, m_skyboxDay, m_time));
        }

        public void FromDayToNight()
        {
            StartCoroutine(LerpSkybox(m_skyboxDay, m_skyboxNight, m_time));
        }

        public void FromNightToSunrise()
        {
            StartCoroutine(LerpSkybox(m_skyboxNight, m_skyboxSunrise, m_time));
        }

        public void FromSunriseToDay()
        {
            StartCoroutine(LerpSkybox(m_skyboxSunrise, m_skyboxDay, m_time));
        }

        public void FromDayToSunset()
        {
            StartCoroutine(LerpSkybox(m_skyboxDay, m_skyboxSunset, m_time));
        }

        public void FromSunsetToNight()
        {
            StartCoroutine(LerpSkybox(m_skyboxSunset, m_skyboxNight, m_time));
        }
        #endregion
    }
}
