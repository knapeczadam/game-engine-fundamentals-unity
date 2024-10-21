using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public abstract class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    public float MaxHealth => _maxHealth;
    
    private float _currentHealth = 0;
    public float CurrentHealth => _currentHealth;
    
    public delegate void HealthChange(float startHealth, float currentHealth);
    public event HealthChange OnHealthChange = null;
    
    private bool _isDead = false;
    public bool IsDead => _isDead;
    
    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public virtual bool TakeDamage(float damage)
    {
        if (_isDead) return false;
        
        _currentHealth -= damage;
        if (OnHealthChange != null) OnHealthChange.Invoke(_maxHealth, _currentHealth);
        
        if (_currentHealth <= 0)
        {
            _isDead = true;
            Die();
            
            return false;
        }
        
        return true;
    }

    protected abstract void Die();
}
