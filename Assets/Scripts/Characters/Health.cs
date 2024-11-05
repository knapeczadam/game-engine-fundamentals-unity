using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[DisallowMultipleComponent]
public abstract class Health : MonoBehaviour
{
    public float m_maxHealth      = 100.0f;
    public float m_currentHealth { get; private set; } = 0.0f;
    public bool  m_isDead        { get; private set; } = false;
    
    public delegate void HealthChange(float startHealth, float currentHealth);
    public event HealthChange OnHealthChange = null;
    
    protected virtual void Awake()
    {
        m_currentHealth = m_maxHealth;
    }

    public virtual bool TakeDamage(float damage, float forceMultiplier = 1.0f)
    {
        if (m_isDead) return false;
        
        m_currentHealth -= damage;
        
        if (m_currentHealth > m_maxHealth)
        {
            m_currentHealth = m_maxHealth;
        }
        
        OnHealthChange?.Invoke(m_maxHealth, m_currentHealth);

        if (m_currentHealth <= 0)
        {
            m_isDead = true;
            Die();
            
            return false;
        }
        
        return true;
    }
    

    protected abstract void Die();
    
    public void Reset ()
    {
        m_currentHealth = m_maxHealth;
        m_isDead = false;
    }
}
