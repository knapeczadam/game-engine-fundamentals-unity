using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GEF
{
    public class WaveManager : MonoBehaviour
    {
        #region Properties
        [SerializeField, Range(1.0f,  10.0f)] private float m_firstWaveStart         = 5.0f;
        [SerializeField, Range(1.0f,  30.0f)] private float m_waveStartFrequency     = 15.0f;
        [SerializeField, Range(1.0f,  20.0f)] private float m_waveEndFrequency       = 7.0f;
        [SerializeField, Range(0.01f, 5.0f)]  private float m_waveFrequencyIncrement = 0.5f;
        [SerializeField] private List<GameObject> m_spawnPointsToEnable = new List<GameObject>();
        [SerializeField] private List<GameObject> m_textAnimationsToEnable = new List<GameObject>();
        private int           m_currentWave      = 0;
        private WeaponManager m_weaponManager    = null;
        private float         m_currentFrequency = 0.0f;
        #endregion

        #region Lifecycle
        void Awake()
        {
            m_currentFrequency = m_waveStartFrequency;

            Invoke(nameof(StartNewWave), m_firstWaveStart);
        }

        private void Start()
        {
            var playerCharacter = FindObjectOfType<PlayerCharacter>();
            if (playerCharacter)
            {
                m_weaponManager = playerCharacter.GetComponent<WeaponManager>();
                if (m_weaponManager)
                {
                    m_weaponManager.OnWeaponChange += OnWeaponChange;
                }
            }
        }

        private void OnDestroy()
        {
            if (m_weaponManager)
            {
                m_weaponManager.OnWeaponChange -= OnWeaponChange;
            }
        }
        #endregion

        #region Methods
        private void OnWeaponChange()
        {
            Invoke(nameof(ActivateNextWave), 2.0f);
        }

        private void ActivateNextWave()
        {
            ++m_currentWave;
            if (m_currentWave < m_spawnPointsToEnable.Count)
            {
                m_spawnPointsToEnable[m_currentWave].SetActive(true);
                m_textAnimationsToEnable[m_currentWave].SetActive(true);
            }
        }

        private void StartNewWave()
        {
            SpawnManager.Instance.SpawnWave();

            m_currentFrequency = Mathf.Clamp(m_currentFrequency - m_waveFrequencyIncrement,
                m_waveEndFrequency, m_waveStartFrequency);

            Invoke(nameof(StartNewWave), m_currentFrequency);
        }
        #endregion
    }
}