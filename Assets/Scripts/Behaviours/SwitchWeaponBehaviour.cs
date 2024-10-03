using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeaponBehaviour : MonoBehaviour
{
    private WeaponManager _weaponManager = null;
    private void Awake()
    {
        _weaponManager = GetComponent<WeaponManager>();
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
    }
}
