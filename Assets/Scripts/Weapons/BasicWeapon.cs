using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    
    [SerializeField]
    private bool _fastFire = false;
    public bool FastFire
    {
        get => _fastFire;
        set => _fastFire = value;
    }

    private void Start()
    {
        // InvokeRepeating(nameof(Fire), 1.0f, 1.0f);
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
    }

    public void Fire()
    {
        _triggerPulled = true;
    }
}
