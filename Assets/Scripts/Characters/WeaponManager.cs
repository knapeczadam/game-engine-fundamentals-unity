using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _weapons = new List<GameObject>();
    
    public List<GameObject> Weapons => _weapons;
    
    private GameObject _currentWeapon = null;
    public GameObject CurrentWeapon 
    {
        get => _currentWeapon;
        set => _currentWeapon = value;
    }
}
