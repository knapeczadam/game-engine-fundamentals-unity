using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[DisallowMultipleComponent]
public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] 
    private GameObject _gunTemplate = null;

    [SerializeField] 
    private GameObject _socket = null;

    protected BasicWeapon Weapon { get; set; } = null;
    
    private void Awake()
    {
        if (_gunTemplate && _socket)
        {
            var gunObject = Instantiate(_gunTemplate, _socket.transform, true);
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
