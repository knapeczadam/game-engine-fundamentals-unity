using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GEF
{
    public class PlayerHealth : Health
    {
        #region Lifecycle
        protected void OnCollisionStay(Collision other)
        {
            var health = other.gameObject.GetComponent<Health>();
            if (health && health.enabled)
            {
                if (health.m_isDead)
                {
                    return;
                }

                // TODO: Hard coded values, should be replaced with a more dynamic solution
                switch (other.gameObject.tag)
                {
                    case Tags.SlOW_ENEMY:
                        TakeDamage(0.5f);
                        break;
                    case Tags.FAST_ENEMY:
                        TakeDamage(0.25f);
                        break;
                    case Tags.BOSS_ENEMY:
                        TakeDamage(0.8f);
                        break;
                    case Tags.ENEMY: // base case
                        TakeDamage(0.25f);
                        break;
                }
            }
        }
        
        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                if (m_currentHealth < MaxHealth)
                {
                    StartCoroutine(RestoreHealth());
                }
            }
        }
        #endregion

        #region Methods
        protected override void Die()
        {
            Destroy(gameObject);
        }
        
        private IEnumerator RestoreHealth()
        {
            while (m_currentHealth < MaxHealth)
            {
                TakeDamage(-0.1f);
                yield return new WaitForSeconds(0.1f);
            }
        }
        #endregion
    }
}
