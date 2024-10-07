using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponBar : MonoBehaviour
{
    private TMP_Text _weaponText = null;
    private WeaponManager _weaponManager = null;

    private void Awake()
    {
        _weaponText = GetComponent<TMP_Text>();
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        if (player)
        {
            _weaponManager = player.GetComponent<WeaponManager>();
        }
    }

    void Start()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        if (player)
        {
            var switchWeaponBehaviour = player.GetComponent<SwitchWeaponBehaviour>();
            if (switchWeaponBehaviour)
            {
                UpdateWeaponText(0);
                switchWeaponBehaviour.OnWeaponSwitched += UpdateWeaponText;
            }
        }
    }
    
    private void UpdateWeaponText(int weaponIndex)
    {
        _weaponText.text = _weaponManager.CurrentWeapon.GetComponent<BasicWeapon>().WeaponName;
    }
}
