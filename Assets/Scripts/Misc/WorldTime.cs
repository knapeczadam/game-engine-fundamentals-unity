using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class WorldTime : MonoBehaviour
{
    public event EventHandler<TimeSpan> WorldTimeChanged; 
    
    [SerializeField] private float m_dayLength; // in seconds
    private TimeSpan m_currentTime;
    private float m_minuteLength => m_dayLength / WorldTimeConstants.MINUTES_IN_DAY;

    private void Start()
    {
        StartCoroutine(AddMinute());
    }

    private IEnumerator AddMinute()
    {
        WorldTimeChanged?.Invoke(this, m_currentTime);
        m_currentTime += TimeSpan.FromMinutes(1);
        yield return new WaitForSeconds(m_minuteLength);
        StartCoroutine(AddMinute());
    }

    public void Reset()
    {
        m_currentTime = TimeSpan.Zero;
    }
}
