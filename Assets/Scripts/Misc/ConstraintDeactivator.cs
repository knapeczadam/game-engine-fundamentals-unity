using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GEF
{
    public class ConstraintDeactivator : MonoBehaviour
    {
        #region Lifecycle
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.PLAYER))
            {
                var constraint = other.GetComponent<Rigidbody>();
                if (constraint)
                {
                    constraint.constraints = RigidbodyConstraints.None;
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(Tags.PLAYER))
            {
                var constraint = other.GetComponent<Rigidbody>();
                if (constraint)
                {
                    constraint.constraints = RigidbodyConstraints.None;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Tags.PLAYER))
            {
                var constraint = other.GetComponent<Rigidbody>();
                if (constraint)
                {
                    constraint.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                }
            }
        }
        #endregion
    }
}
