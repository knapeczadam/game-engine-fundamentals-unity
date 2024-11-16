using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private UnityEvent m_onTriggerEnter = null;
    [SerializeField] private UnityEvent m_onTriggerExit = null;
    
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
}
