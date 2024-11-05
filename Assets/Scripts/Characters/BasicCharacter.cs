using UnityEngine;

    public class BasicCharacter : MonoBehaviour
    {
        protected MovementBehaviour m_movementBehaviour = null;
        protected AttackBehaviour   m_attackBehaviour   = null;

        protected virtual void Awake()
        {
            m_movementBehaviour = GetComponent<MovementBehaviour>();
            m_attackBehaviour   = GetComponent<AttackBehaviour>();
        }
    }
