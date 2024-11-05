using UnityEngine;
using UnityEngine.Serialization;

public class BasicEnemyCharacter : BasicCharacter
{
     [SerializeField] private float m_attackRange = 2.0f;
    private GameObject m_playerTarget = null;

    private void Start()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        if (player)
        {
            m_playerTarget = player.gameObject;
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        if (m_movementBehaviour)
        {
            m_movementBehaviour.m_target = m_playerTarget;
        }
    }

    private void HandleAttack()
    {
        if (m_attackBehaviour && m_playerTarget)
        {
            if ((transform.position - m_playerTarget.transform.position).sqrMagnitude <= m_attackRange * m_attackRange)
            {
                Debug.Log("Attacking player");
                m_attackBehaviour.Attack();
                // Invoke(nameof(Die), 0.2f);
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    
    
}
