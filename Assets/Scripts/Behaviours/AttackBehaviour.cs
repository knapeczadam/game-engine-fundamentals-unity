using UnityEngine;
using UnityEngine.Serialization;

namespace GEF
{
    [DisallowMultipleComponent]
    public class AttackBehaviour : MonoBehaviour
    {
        #region Properties
        protected BasicWeapon m_weapon { get; set; } = null;
        #endregion
        
        #region Fields
        [SerializeField] private GameObject m_gunTemplate = null;
        [SerializeField] private GameObject m_socket      = null;
        #endregion

        #region Lifecycle
        private void Awake()
        {
            if (m_gunTemplate && m_socket)
            {
                var gunObject = Instantiate(m_gunTemplate, m_socket.transform, true);
                gunObject.transform.localPosition = Vector3.zero;
                gunObject.transform.localRotation = Quaternion.identity;
                m_weapon = gunObject.GetComponent<BasicWeapon>();
            }
        }
        #endregion

        #region Public Methods
        public virtual void Attack()
        {
            if (m_weapon)
            {
                m_weapon.Fire();
            }
        }
        #endregion
    }
}