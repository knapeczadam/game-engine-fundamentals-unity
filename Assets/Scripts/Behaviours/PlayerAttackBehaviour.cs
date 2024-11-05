using UnityEngine;
using UnityEngine.Serialization;

[DisallowMultipleComponent]
public class PlayerAttackBehaviour : AttackBehaviour
{
    private WeaponManager   m_weaponManager   = null;
    private PickUpBehaviour m_pickUpBehaviour = null;
    public  bool            m_canAttack       = true;
    
    private void Awake()
    {
        m_weaponManager = GetComponent<WeaponManager>();
        m_pickUpBehaviour = GetComponent<PickUpBehaviour>();
    }

    private void Start()
    {
        if (m_pickUpBehaviour)
        {
            m_pickUpBehaviour.OnPickUp += HandlePickUp;
        }
    }

    private void HandlePickUp(bool catPickedUp)
    {
        m_canAttack = !catPickedUp;
    }
    
    public override void Attack()
    {
        Weapon = m_weaponManager.m_currentWeapon.GetComponent<BasicWeapon>();
        if (m_canAttack && Weapon)
        {
            Weapon.Fire();
        }
        else
        {
            Debug.Log("Cannot attack while carrying a cat.");
        }
    }
}
