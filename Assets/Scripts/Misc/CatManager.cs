using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    private int _catCount = 0;
    private PickUpBehaviour _pickUpBehaviour;
    private bool _inSafeZone = false;
    
    private void Awake()
    {
        var player = FindObjectOfType<PlayerCharacter>();
        if (player)
        {
            _pickUpBehaviour = player.GetComponent<PickUpBehaviour>();
        }
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
        if (!catPickedUp && _inSafeZone)
        {
            _catCount++;
            Debug.Log($"Cat count: {_catCount}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.FRIEND))
        {
            var pickUpBehaviour = other.gameObject.GetComponent<PickUpBehaviour>();
            if (pickUpBehaviour)
            {
                if (pickUpBehaviour.CatPickedUp)
                {
                    _inSafeZone = true;
                    GetComponentInChildren<Highlight>().EnableHighlight();
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.FRIEND))
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.FRIEND))
        {
            GetComponentInChildren<Highlight>().DisableHighlight();
            _inSafeZone = false;
        }
    }
}
