using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> m_weapons = new List<GameObject>();
    public GameObject m_currentWeapon { get; set; } = null;
    
    [SerializeField] private bool m_isTutorial = false;
    
    private readonly Dictionary<int, int> m_weaponRules = new Dictionary<int, int>()
    {
        {0, 0},  // default weapon needs 0 cast
        {1, 3},  
        {2, 5},
        {3, 10},
        {4, 15},
        {5, 20},
        {6, 30},
        {7, 50},
        {8, 100},
        {9, 200},
    };
    
    public Dictionary<int, bool> m_allowedWeapons = new Dictionary<int, bool>()
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
    
    [SerializeField] private List<GameObject> m_animations = new List<GameObject>();
    [SerializeField] private List<GameObject> m_weaponDisplay = new List<GameObject>();
    [SerializeField] private TMP_Text m_weaponText = null;
    
    private CatManager _catManager = null;
    
    private void Start()
    {
        _catManager = FindObjectOfType<CatManager>();
        if (_catManager)
        {
            _catManager.OnCatCountChange += OnCatCountChange;
        }
        
        if (m_isTutorial)
        {
            m_weaponRules[1] = 2;
            m_weaponRules[2] = 0;
        }
    }
    
    private void OnDestroy()
    {
        if (_catManager)
        {
            _catManager.OnCatCountChange -= OnCatCountChange;
        }
    }
    
    private void OnCatCountChange(int catCount, CatManager catManager)
    {
        foreach (var allowedWeapon in m_allowedWeapons)
        {
            if (!allowedWeapon.Value)
            {
                m_weaponText.text = $"{catManager.m_catCount} / {m_weaponRules[allowedWeapon.Key]}";
                if (catCount >= m_weaponRules[allowedWeapon.Key])
                {
                    m_allowedWeapons[allowedWeapon.Key] = true;
                    m_animations[allowedWeapon.Key].SetActive(true);
                    m_weaponDisplay[allowedWeapon.Key - 1].SetActive(false);
                    m_weaponDisplay[allowedWeapon.Key].SetActive(true);
                    catManager.m_catCount = 0;
                    m_weaponText.text = $"{catManager.m_catCount} / {m_weaponRules[allowedWeapon.Key + 1]}";
                }
                break;
            }
        }
    }

    public void Reset()
    {
        foreach (var allowedWeapon in m_allowedWeapons)
        {
            m_allowedWeapons[allowedWeapon.Key] = false;
        }
        m_allowedWeapons[0] = true;

        foreach (var weapon in m_weapons)
        {
            weapon.SetActive(false);
        }
        m_weapons[0].SetActive(true);
        m_currentWeapon = m_weapons[0];
        
        
    }
}
