using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    void Start()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        if (player)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth)
            {
                UpdateHealth(playerHealth.MaxHealth, playerHealth.CurrentHealth);
                playerHealth.OnHealthChange += UpdateHealth;
            }
        }
    }
    
    public void UpdateHealth(float startHealth, float currentHealth)
    {
        var value = currentHealth / startHealth;
        transform.localScale = new Vector3(value, 1, 1);
    }
}
