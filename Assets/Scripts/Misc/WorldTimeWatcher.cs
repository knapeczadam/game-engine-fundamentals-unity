using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class WorldTimeWatcher : MonoBehaviour
{
    [SerializeField] private WorldTime      m_worldTime = null;
    [SerializeField] private List<Schedule> m_schedules = new List<Schedule>();

    private void Start()
    {
        m_worldTime.WorldTimeChanged += CheckSchedule;
    }
    
    private void OnDestroy()
    {
        m_worldTime.WorldTimeChanged -= CheckSchedule;
    }

    private void CheckSchedule(object sender, TimeSpan newTime)
    {
        var schedule = m_schedules.FirstOrDefault(s => s.Hour == newTime.Hours && s.Minute == newTime.Minutes);
        schedule?._action?.Invoke();
    }

    [Serializable]
    private class Schedule
    {
        public int Hour;
        public int Minute;
        public UnityEvent _action;
    }
}
