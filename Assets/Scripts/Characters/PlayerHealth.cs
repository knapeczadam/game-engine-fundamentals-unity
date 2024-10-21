using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : Health
{
    protected void OnCollisionStay(Collision other)
    {
        var health = other.gameObject.GetComponent<Health>();
        if (health && health.enabled)   
        {
            if (health.IsDead)
            {
                return;
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
                case Tags.BOSS_ENEMY:
                    TakeDamage(0.5f);
                    break;
            }
        }
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
