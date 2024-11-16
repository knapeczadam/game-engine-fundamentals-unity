using UnityEngine;

namespace GEF
{
    public class SwitchWeaponBehaviour : MonoBehaviour
    {
        #region Fields
        public delegate void WeaponSwitched(int weaponIndex);
        public event WeaponSwitched OnWeaponSwitched = null;
        #endregion

        #region Properties
        private WeaponManager m_weaponManager = null;
        #endregion

        #region Lifecycle
        private void Awake()
        {
            m_weaponManager = GetComponent<WeaponManager>();

            // Switch to the first weapon in the list
            if (m_weaponManager.m_weapons.Count > 0)
            {
                SwitchWeapon(0);
            }
        }
        #endregion

        #region Public Methods
        public void SwitchWeapon(int weaponIndex)
        {
            if (weaponIndex < 0 || weaponIndex >= m_weaponManager.m_weapons.Count)
            {
                return;
            }

            if (m_weaponManager.m_allowedWeapons[weaponIndex] == false)
            {
                return;
            }

            for (int i = 0; i < m_weaponManager.m_weapons.Count; i++)
            {
                m_weaponManager.m_weapons[i].SetActive(i == weaponIndex);
                m_weaponManager.m_currentWeapon = m_weaponManager.m_weapons[weaponIndex];
            }

            // Invoke WeaponBar's UpdateWeaponText method
            OnWeaponSwitched?.Invoke(weaponIndex);
        }
        #endregion
    }
}