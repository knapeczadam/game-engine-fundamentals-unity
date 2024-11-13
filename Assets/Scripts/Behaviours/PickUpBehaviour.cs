using System;
using UnityEngine;

[DisallowMultipleComponent]
public class PickUpBehaviour : MonoBehaviour
{
    public bool m_catPickedUp { get; private set; } = false;
    public delegate void PickUpAction(bool catPickedUp);
    public event PickUpAction OnPickUp = null;
    private GameObject m_aiCat       = null;
    private GameObject m_staticCat   = null;
    private GameObject m_rootCat     = null;
    private WeaponManager m_weaponManager = null;
    
    [SerializeField] private GameObject _catSocket = null;
    [SerializeField] CatManager m_catManager = null;
    private bool       m_catDetected = false;

    private void Awake()
    {
        m_weaponManager = GetComponent<WeaponManager>();
    }

    public void PickUp()
    {
        if (!m_catPickedUp && m_catDetected)
        {
            Debug.Log("Cat picked up");
            
            m_catPickedUp = true;
            OnPickUp?.Invoke(m_catPickedUp);
            m_weaponManager.m_currentWeapon.SetActive(false);

            // Hide the AI cat and show the static cat
            m_aiCat.SetActive(false);
            m_staticCat.SetActive(true);
            
            m_rootCat.transform.SetParent(_catSocket.transform, false);
        }
        else if (m_catPickedUp)
        {
            Debug.Log("Release the cat");
            
            m_catPickedUp = false;
            OnPickUp?.Invoke(m_catPickedUp);
            m_weaponManager.m_currentWeapon.SetActive(true);

            if (m_catManager && m_catManager.SafeZoneActive())
            {
                if (m_catManager.m_destroyCat)
                {
                    Destroy(m_rootCat);
                }
                else
                {
                    // Hide the AI cat and show the static cat
                    m_aiCat.SetActive(false);
                    m_staticCat.SetActive(true);
                
                    var catSocket = m_rootCat.GetComponent<Cat>().m_catSocket;
                    m_rootCat.transform.SetParent(catSocket.transform, false);
                }
            }
            else
            {
                // Hide the static cat and show the AI cat
                m_staticCat.SetActive(false);
            
                m_rootCat.transform.SetParent(null, false);
            
                // Reset the cat's position and rotation
                m_aiCat.transform.position = transform.position;
                m_aiCat.transform.rotation = transform.rotation;
                m_aiCat.SetActive(true);
            }
        }
        else
        {
            Debug.Log("No cat detected");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.CAT))
        {
            m_catDetected = true;
            
            m_aiCat = other.gameObject;
            m_staticCat = other.transform.parent.GetComponentInChildren<StaticCat>(true).gameObject;
            m_rootCat = other.transform.root.gameObject;
        }
        else if (other.CompareTag(Tags.TREE))
        {
            m_catDetected = other.gameObject.GetComponent<MyTree>().HasCat();
            if (m_catDetected)
            {
                m_staticCat = other.gameObject.GetComponentInChildren<StaticCat>().gameObject;
                m_aiCat = m_staticCat.transform.parent.GetComponentInChildren<AICat>(true).gameObject;
                m_rootCat = m_staticCat.transform.parent.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.CAT))
        {
            m_catDetected = false;
            m_aiCat = null;
            m_staticCat = null;
        }
        else if (other.CompareTag(Tags.TREE))
        {
            m_catDetected = false;
            if (!m_catPickedUp)
            {
                m_staticCat = null;
                m_aiCat = null;
                m_rootCat = null;
            }
        }
    }
}