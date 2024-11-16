using UnityEngine;

namespace GEF
{
    public class HealthBar : MonoBehaviour
    {
        #region Lifecycle
        void Start()
        {
            PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
            if (player)
            {
                Health playerHealth = player.GetComponent<Health>();
                if (playerHealth)
                {
                    UpdateHealth(playerHealth.MaxHealth, playerHealth.m_currentHealth);
                    playerHealth.OnHealthChange += UpdateHealth;
                }
            }
        }
        #endregion

        #region Public Methods
        public void UpdateHealth(float startHealth, float currentHealth)
        {
            var value = currentHealth / startHealth;
            transform.localScale = new Vector3(value, 1, 1);
        }
        #endregion
    }
}
