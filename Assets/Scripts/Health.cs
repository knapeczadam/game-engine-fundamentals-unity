using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth = 100;
    public float MaxHealth => _maxHealth;
    
    private int _currentHealth = 0;
    public float CurrentHealth => _currentHealth;
    
    public delegate void HealthChange(float startHealth, float currentHealth);
    public event HealthChange OnHealthChange;
    
    private void Awake()
    {
        _currentHealth = _maxHealth;
    }
    
    public void TakeDamage(int damage)
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
