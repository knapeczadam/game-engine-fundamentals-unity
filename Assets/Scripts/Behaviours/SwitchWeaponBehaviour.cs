using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeaponBehaviour : MonoBehaviour
{
    private WeaponManager _weaponManager = null;
    
    public delegate void WeaponSwitched(int weaponIndex);
    public event WeaponSwitched OnWeaponSwitched;
    
    private void Awake()
    {
        _weaponManager = GetComponent<WeaponManager>();
        
        // Switch to the first weapon in the list
        if (_weaponManager.Weapons.Count > 0)
        {
            SwitchWeapon(0);
        }
    }

    public void SwitchWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= _weaponManager.Weapons.Count)
            return;

        for (int i = 0; i < _weaponManager.Weapons.Count; i++)
        {
            _weaponManager.Weapons[i].SetActive(i == weaponIndex);
            _weaponManager.CurrentWeapon = _weaponManager.Weapons[weaponIndex];
        }
        
        // Invoke WeaponBar's UpdateWeaponText method
        OnWeaponSwitched?.Invoke(weaponIndex);
    }
}
