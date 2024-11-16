using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField] private float m_speed           = 30.0f;
    [SerializeField] private float m_lifeTime        = 1.0f;
    [SerializeField] private float m_damage          = 20.0f;
    [SerializeField] private float m_forceMultiplier = 1.0f;
    [SerializeField] List<GameObject> m_explosions = new List<GameObject>();
    
    private static readonly string[] m_RAYCAST_MASKS = { "Ground", "StaticLevel" };
    private static readonly string[] m_SHOOTABLE_LAYERS = { "Enemy", "Friend" };

    private void Awake()
    {
        Invoke(nameof(Die), m_lifeTime);

        foreach (var explosion in m_explosions)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        if (!WallDetection())
        {
            transform.position += transform.forward * (m_speed * Time.deltaTime);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private bool WallDetection()
    {
        Ray collisionRay = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(collisionRay, Time.deltaTime * m_speed, LayerMask.GetMask(m_RAYCAST_MASKS)))
        {
            Die();
            return true;
        }
        return false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var allowedLayers = LayerMask.GetMask(m_SHOOTABLE_LAYERS);
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
            health.TakeDamage(m_damage, m_forceMultiplier);
            Die();
        }
    }
}
