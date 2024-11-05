using UnityEngine;
using UnityEngine.Serialization;

[DisallowMultipleComponent]
public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject m_gunTemplate = null;
    [SerializeField] private GameObject m_socket      = null;

    protected BasicWeapon Weapon { get; set; } = null;
    
    private void Awake()
    {
        if (m_gunTemplate && m_socket)
        {
            var gunObject = Instantiate(m_gunTemplate, m_socket.transform, true);
            gunObject.transform.localPosition = Vector3.zero;
            gunObject.transform.localRotation = Quaternion.identity;
            Weapon = gunObject.GetComponent<BasicWeapon>();
        }
    }

    public virtual void Attack()
    {
        if (Weapon)
        {
            Weapon.Fire();
        }
    }
}
