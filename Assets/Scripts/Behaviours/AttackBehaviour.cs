using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] 
    private GameObject _gunTemplate = null;

    [SerializeField] 
    private GameObject _socket = null;

    private BasicWeapon _weapon = null;

    private void Awake()
    {
        if (_gunTemplate && _socket)
        {
            var gunObject = Instantiate(_gunTemplate, _socket.transform, true);
            gunObject.transform.localPosition = Vector3.zero;
            gunObject.transform.localRotation = Quaternion.identity;
            _weapon = gunObject.GetComponent<BasicWeapon>();
        }
    }

    public void Attack()
    {
        if (_weapon)
        {
            _weapon.Fire();
        }
    }
}
