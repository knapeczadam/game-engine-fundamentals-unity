using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstraintDeactivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var constraint = other.GetComponent<Rigidbody>();
        if (constraint)
        {
            constraint.constraints = RigidbodyConstraints.None;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        var constraint = other.GetComponent<Rigidbody>();
        if (constraint)
        {
            constraint.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
    }
}
