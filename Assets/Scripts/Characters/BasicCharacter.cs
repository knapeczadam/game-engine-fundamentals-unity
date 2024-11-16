using UnityEngine;

namespace GEF
{
    public class BasicCharacter : MonoBehaviour
    {
        #region Properties
        protected MovementBehaviour m_movementBehaviour = null;
        protected AttackBehaviour   m_attackBehaviour   = null;
        #endregion

        #region Lifecycle
        protected virtual void Awake()
        {
            m_movementBehaviour = GetComponent<MovementBehaviour>();
            m_attackBehaviour   = GetComponent<AttackBehaviour>();
        }
        #endregion
    }
}