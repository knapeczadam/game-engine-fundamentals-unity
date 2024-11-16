using UnityEngine;
using UnityEngine.Serialization;

namespace GEF
{
    [DisallowMultipleComponent]
    public class PlayerAttackBehaviour : AttackBehaviour
    {
        #region Properties
        private WeaponManager   m_weaponManager   = null;
        private PickUpBehaviour m_pickUpBehaviour = null;
        private bool            m_canAttack       = true;
        #endregion

        #region Lifecycle
        private void Awake()
        {
            m_weaponManager = GetComponent<WeaponManager>();
            m_pickUpBehaviour = GetComponent<PickUpBehaviour>();
        }

        private void Start()
        {
            if (m_pickUpBehaviour)
            {
                m_pickUpBehaviour.OnPickUp += HandlePickUp;
            }
        }
        #endregion
        
        #region Public Methods
        public override void Attack()
        {
            m_weapon = m_weaponManager.m_currentWeapon.GetComponent<BasicWeapon>();
            if (m_canAttack && m_weapon)
            {
                m_weapon.Fire();
            }
            else
            {
                Debug.Log("Cannot attack while carrying a cat.");
            }
        }
        #endregion

        #region Methods
        private void HandlePickUp(bool catPickedUp)
        {
            m_canAttack = !catPickedUp;
        }
        #endregion
    }
}
