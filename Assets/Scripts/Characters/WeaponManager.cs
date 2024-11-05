using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> m_weapons = new List<GameObject>();
    public GameObject m_currentWeapon { get; set; } = null;
    
    private readonly Dictionary<int, int> _weaponRules = new Dictionary<int, int>()
    {
        {0, 0},  // default weapon needs 0 cast
        {1, 2},  // uzi needs 5 cast
        {2, 15}, // shotgun needs an additional 10 cast 
        {3, 30},
        {4, 50},
        {5, 75},
        {6, 105},
        {7, 145},
        {8, 195},
        {9, 255},
    };
    
    private Dictionary<int, bool> _allowedWeapons = new Dictionary<int, bool>()
    {
        {0, true},
        {1, false},
        {2, false},
        {3, false},
        {4, false},
        {5, false},
        {6, false},
        {7, false},
        {8, false},
        {9, false},
    };
    public Dictionary<int, bool> AllowedWeapons => _allowedWeapons;
    
    private CatManager _catManager = null;
    
    private void Start()
    {
        _catManager = FindObjectOfType<CatManager>();
        if (_catManager)
        {
            _catManager.OnCatCountChange += OnCatCountChange;
        }
    }
    
    private void OnDestroy()
    {
        if (_catManager)
        {
            _catManager.OnCatCountChange -= OnCatCountChange;
        }
    }
    
    private void OnCatCountChange(int catCount)
    {
        foreach (var allowedWeapon in _allowedWeapons)
        {
            if (!allowedWeapon.Value)
            {
                if (catCount >= _weaponRules[allowedWeapon.Key])
                {
                    _allowedWeapons[allowedWeapon.Key] = true;
                }
                break;
            }
        }
    }

    public void Reset()
    {
        foreach (var allowedWeapon in _allowedWeapons)
        {
            _allowedWeapons[allowedWeapon.Key] = false;
        }
        _allowedWeapons[0] = true;

        foreach (var weapon in m_weapons)
        {
            weapon.SetActive(false);
        }
        m_weapons[0].SetActive(true);
        m_currentWeapon = m_weapons[0];
        
        
    }
}
