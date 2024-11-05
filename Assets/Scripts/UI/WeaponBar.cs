using TMPro;
using UnityEngine;

public class WeaponBar : MonoBehaviour
{
    private TMP_Text      m_weaponText    = null;
    private WeaponManager m_weaponManager = null;

    private void Awake()
    {
        m_weaponText = GetComponent<TMP_Text>();
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        if (player)
        {
            m_weaponManager = player.GetComponent<WeaponManager>();
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
    
    private void OnDestroy()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        if (player)
        {
            var switchWeaponBehaviour = player.GetComponent<SwitchWeaponBehaviour>();
            if (switchWeaponBehaviour)
            {
                switchWeaponBehaviour.OnWeaponSwitched -= UpdateWeaponText;
            }
        }
    }
    
    private void UpdateWeaponText(int weaponIndex)
    {
        m_weaponText.text = m_weaponManager.m_currentWeapon.GetComponent<BasicWeapon>().m_weaponName;
    }
}
