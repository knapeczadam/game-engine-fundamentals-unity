using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAttackBehaviour : AttackBehaviour
{
    private WeaponManager _weaponManager = null;
    
    private void Awake()
    {
        _weaponManager = GetComponent<WeaponManager>();
    }
    
    public override void Attack()
    {
        Weapon = _weaponManager.CurrentWeapon.GetComponent<BasicWeapon>();
        if (Weapon)
        {
            Weapon.Fire();
        }
    }
}
