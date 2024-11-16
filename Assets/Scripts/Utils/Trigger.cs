using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GEF
{
    public class Trigger : MonoBehaviour
    {
        #region Properties
        [SerializeField] private UnityEvent m_onTriggerEnter = null;
        [SerializeField] private UnityEvent m_onTriggerExit  = null;
        #endregion

        #region Lifecycle
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.PLAYER))
            {
                m_onTriggerEnter?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Tags.PLAYER))
            {
                m_onTriggerExit?.Invoke();
            }
        }
        #endregion
    }
}