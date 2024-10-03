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
    private float _damage = 20.0f;

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

    private static readonly string[] RAYCAST_MASKS = { "Ground", "StaticLevel" };

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
        if (!other.CompareTag(Tags.FRIEND) && !other.CompareTag(Tags.ENEMY))
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
