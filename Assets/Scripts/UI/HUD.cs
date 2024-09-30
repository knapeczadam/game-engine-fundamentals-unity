using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    private UIDocument _attachedDocument = null;
    private VisualElement _root = null;
    
    private ProgressBar _healthBar = null;

    private void Start()
    {
        _attachedDocument = GetComponent<UIDocument>();
        if (_attachedDocument)
        {
            _root = _attachedDocument.rootVisualElement;
        }

        if (_root != null)
        {
            _healthBar = _root.Q<ProgressBar>();

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
    }

    public void UpdateHealth(float startHealth, float currentHealth)
    {
        if (_healthBar == null) return;
        
        _healthBar.value = currentHealth / startHealth;
        _healthBar.title = $"{currentHealth}/{startHealth}";
    }
}
