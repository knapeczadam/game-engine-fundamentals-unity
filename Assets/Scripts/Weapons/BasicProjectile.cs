using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField] private float _speed    = 30.0f;
    [SerializeField] private float _lifeTime = 1.0f;
    [SerializeField] private float _damage   = 20.0f;
    
    private static readonly string[] RAYCAST_MASKS = { "Ground", "StaticLevel" };
    private static readonly string[] SHOOTABLE_LAYERS = { "Enemy", "Friend" };

    private void Awake()
    {
        Invoke(nameof(Die), _lifeTime);
    }

    private void FixedUpdate()
    {
        if (!WallDetection())
        {
            transform.position += transform.forward * (_speed * Time.deltaTime);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private bool WallDetection()
    {
        Ray collisionRay = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(collisionRay, Time.deltaTime * _speed, LayerMask.GetMask(RAYCAST_MASKS)))
        {
            Die();
            return true;
        }
        return false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var allowedLayers = LayerMask.GetMask(SHOOTABLE_LAYERS);
        if ((allowedLayers & (1 << other.gameObject.layer)) == 0)
        {
            return;
        }
        
        if (other.CompareTag(tag))
        {
            return;
        }
        
        Health health = other.GetComponent<Health>();
        if (health)
        {
            health.TakeDamage(_damage);
            Die();
        }
    }
}
