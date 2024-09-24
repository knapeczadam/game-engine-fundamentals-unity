using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 30.0f;

    [SerializeField] 
    private float _lifeTime = 1.0f;

    [SerializeField] 
    private int _damage = 20;

    private void Awake()
    {
        Invoke(nameof(Kill), _lifeTime);
    }

    private void FixedUpdate()
    {
        if (!WallDetection())
        {
            transform.position += transform.forward * (_speed * Time.deltaTime);
        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    private static readonly string[] RAYCAST_MASKS = { "Ground", "StaticLevel" };

    private bool WallDetection()
    {
        Ray collisionRay = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(collisionRay, Time.deltaTime * _speed, LayerMask.GetMask(RAYCAST_MASKS)))
        {
            Kill();
            return true;
        }
        return false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health is object)
        {
            health.TakeDamage(_damage);
            Kill();
        }
    }
}
