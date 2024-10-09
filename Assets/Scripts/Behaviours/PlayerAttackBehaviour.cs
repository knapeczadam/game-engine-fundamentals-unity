using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAttackBehaviour : AttackBehaviour
{
    private WeaponManager _weaponManager = null;
    public bool canAttack = true;
    private PickUpBehaviour _pickUpBehaviour = null;
    
    private void Awake()
    {
        _weaponManager = GetComponent<WeaponManager>();
        _pickUpBehaviour = GetComponent<PickUpBehaviour>();
    }

    private void Start()
    {
        if (_pickUpBehaviour)
        {
            _pickUpBehaviour.OnPickUp += HandlePickUp;
        }
    }

    private void HandlePickUp(bool catPickedUp)
    {
        canAttack = !catPickedUp;
    }
    
    public override void Attack()
    {
        Weapon = _weaponManager.CurrentWeapon.GetComponent<BasicWeapon>();
        if (canAttack && Weapon)
        {
            Weapon.Fire();
        }
    }
}
