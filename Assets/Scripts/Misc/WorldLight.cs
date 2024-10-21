using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class WorldLight : MonoBehaviour
{
    private Light _light = null;
    [SerializeField] private WorldTime _worldTime = null;
    [SerializeField] private Gradient  _gradient  = null;

    private void Awake()
    {
        _light = GetComponent<Light>();
        _worldTime.WorldTimeChanged += OnWorldTimeChanged;
    }

    private void OnWorldTimeChanged(object sender, TimeSpan newTime)
    {
        Debug.Log(PercentOfDay(newTime));
        _light.transform.rotation = Quaternion.Euler(PercentOfDay(newTime) * 360 - 90, 0, 0);
        _light.color = _gradient.Evaluate(PercentOfDay(newTime));
    }

    private void OnDestroy()
    {
        _worldTime.WorldTimeChanged -= OnWorldTimeChanged;
    }

    private float PercentOfDay(TimeSpan timeSpan)
    {
        return (float) timeSpan.TotalMinutes % WorldTimeConstants.MINUTES_IN_DAY / WorldTimeConstants.MINUTES_IN_DAY;
    }
}
