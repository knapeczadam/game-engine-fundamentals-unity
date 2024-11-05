using UnityEngine;
using UnityEngine.Serialization;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private float m_firstWaveStart         = 5.0f;
    [SerializeField] private float m_waveStartFrequency     = 15.0f;
    [SerializeField] private float m_waveEndFrequency       = 7.0f;
    [SerializeField] private float m_waveFrequencyIncrement = 0.5f;

    private float  m_currentFrequency   = 0.0f;
    const   string m_STARTNEWWAVE_METHOD = "StartNewWave";
    
    void Awake()
    {
        m_currentFrequency = m_waveStartFrequency;

        Invoke(m_STARTNEWWAVE_METHOD, m_firstWaveStart);
    }

    void StartNewWave()
    {
        SpawnManager.Instance.SpawnWave();

        m_currentFrequency = Mathf.Clamp(m_currentFrequency - m_waveFrequencyIncrement,
            m_waveEndFrequency, m_waveStartFrequency);

        Invoke(m_STARTNEWWAVE_METHOD, m_currentFrequency);
    }
}