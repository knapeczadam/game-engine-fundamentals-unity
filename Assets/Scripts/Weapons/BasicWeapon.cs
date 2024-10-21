using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class BasicWeapon : MonoBehaviour
{
    [SerializeField] 
    private GameObject _bulletTemplate = null;
    
    [SerializeField]
    private float _fireRate = 25.0f;
    
    [SerializeField]
    private List<Transform> _fireSockets = new List<Transform>();
    
    private bool _triggerPulled = false;
    private float _timeSinceLastShot = 0.0f;

    [SerializeField]
    private string _weaponName = "Basic Weapon";
    public string WeaponName => _weaponName;
    
    [SerializeField] private float _amplitude = 1.0f;
    [SerializeField] private float _frequency = 1.0f;

    
    [SerializeField]
    private bool _fastFire = false;
    public bool FastFire
    {
        get => _fastFire;
        set => _fastFire = value;
    }
    
    [SerializeField] private CinemachineVirtualCamera _camera = null;
    
    [SerializeField] private UnityEvent _onFireEvent = null;


    private void Start()
    {
        // InvokeRepeating(nameof(Fire), 1.0f, 1.0f);
        _camera = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
    }

    private void EnableShake()
    {
        if (_camera)
        {
            _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = _amplitude;
            _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = _frequency;
        }
    }
    
    private void DisableShake()
    {
        if (_camera)
        {
            _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;
            _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.0f;
        }
    }

    private void Update()
    {
        if (_timeSinceLastShot > 0.0f)
        {
            _timeSinceLastShot -= Time.deltaTime;
        }
        
        if (_timeSinceLastShot <= 0.0f && _triggerPulled)
        {
            FireProjectile();
        }
        // the trigger will release by itself
        // if we still are firing, we will receive new fire input
        _triggerPulled = false;
    }

    private void FireProjectile()
    {
        // no bullet to fire
        if (_bulletTemplate == null)
        {
            return;
        }
        
        for (int i = 0; i < _fireSockets.Count; i++)
        {
            Instantiate(_bulletTemplate, _fireSockets[i].position, _fireSockets[i].rotation);
        }
        
        // set the time so we respect the firerate
        _timeSinceLastShot += 1.0f / _fireRate;
        
        _onFireEvent?.Invoke();
    }

    public void Fire()
    {
        _triggerPulled = true;
        
        // start shake for 0.2 seconds
        EnableShake();
        Invoke(nameof(DisableShake), 0.1f);
    }
}
