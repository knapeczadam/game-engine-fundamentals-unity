using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class BasicWeapon : MonoBehaviour
{
    [SerializeField] private GameObject      m_bulletTemplate = null;
    [SerializeField] private float           m_fireRate       = 25.0f;
    [SerializeField] private List<Transform> m_fireSockets    = new    List<Transform>();
    [SerializeField] private float m_amplitude = 1.0f;
    [SerializeField] private float m_frequency = 1.0f;
    [SerializeField] private UnityEvent               _onFireEvent = null;
    [SerializeField] CameraManager m_cameraManager = null;
    
    private bool  m_triggerPulled     = false;
    private float m_timeSinceLastShot = 0.0f;

    public string m_weaponName = "Basic Weapon";
    public bool m_fastFire = false;
    
    private void Awake()
    {
        if (m_cameraManager == null)
        {
            m_cameraManager = FindObjectOfType<CameraManager>();
        }
    }

    private void Update()
    {
        if (m_timeSinceLastShot > 0.0f)
        {
            m_timeSinceLastShot -= Time.deltaTime;
        }
        
        if (m_timeSinceLastShot <= 0.0f && m_triggerPulled)
        {
            FireProjectile();
        }
        // the trigger will release by itself
        // if we still are firing, we will receive new fire input
        m_triggerPulled = false;
    }

    private void FireProjectile()
    {
        // no bullet to fire
        if (m_bulletTemplate == null)
        {
            return;
        }
        
        for (int i = 0; i < m_fireSockets.Count; i++)
        {
            Instantiate(m_bulletTemplate, m_fireSockets[i].position, m_fireSockets[i].rotation);
        }
        
        // set the time so we respect the firerate
        m_timeSinceLastShot += 1.0f / m_fireRate;
        
        _onFireEvent?.Invoke();
    }

    public void Fire()
    {
        m_triggerPulled = true;
        
        // start shake for 0.2 seconds
        m_cameraManager.EnableShake(m_amplitude, m_frequency);
        Invoke(nameof(DisableShake), 0.1f);
    }
    
    private void DisableShake()
    {
        m_cameraManager.DisableShake();
    }

    public void Reset()
    {
        m_timeSinceLastShot = 0.0f;
        m_triggerPulled = false;
    }
}
