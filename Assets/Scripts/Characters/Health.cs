using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth = 100;
    public float MaxHealth => _maxHealth;
    
    private float _currentHealth = 0;
    public float CurrentHealth => _currentHealth;
    
    public delegate void HealthChange(float startHealth, float currentHealth);
    public event HealthChange OnHealthChange;
    
    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag(Tags.ENEMY))
        {
            TakeDamage(0.1f);
        }

        // TODO: Hard coded values, should be replaced with a more dynamic solution
        switch (other.gameObject.tag)
        {
            case Tags.SlOW_ENEMY:
                TakeDamage(0.1f);
                break;
            case Tags.FAST_ENEMY:
                TakeDamage(0.01f);
                break;
            case Tags.BOSS:
                TakeDamage(0.5f);
                break;
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (OnHealthChange != null) OnHealthChange.Invoke(_maxHealth, _currentHealth);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}
