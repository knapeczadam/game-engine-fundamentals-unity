using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    [SerializeField] private Texture2D _skyboxNight   = null;
    [SerializeField] private Texture2D _skyboxSunrise = null;
    [SerializeField] private Texture2D _skyboxDay     = null;
    [SerializeField] private Texture2D _skyboxSunset  = null;
    
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

    public void FromNightToSunrise()
    {
        StartCoroutine(LerpSkybox(_skyboxNight, _skyboxSunrise, 1));
    }

    public void FromSunriseToDay()
    {
        StartCoroutine(LerpSkybox(_skyboxSunrise, _skyboxDay, 1));
    }

    public void FromDayToSunset()
    {
        StartCoroutine(LerpSkybox(_skyboxDay, _skyboxSunset, 1));
    }

    public void FromSunsetToNight()
    {
        StartCoroutine(LerpSkybox(_skyboxSunset, _skyboxNight, 1));
    }
}
