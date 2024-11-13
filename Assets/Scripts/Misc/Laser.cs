using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private bool m_isTutorial = false;
    private LineRenderer m_lineRenderer = null;
    
    private void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
    }
    
    private void Update()
    {
        // Set the start point of the line renderer
        m_lineRenderer.SetPosition(0, transform.position);
        // raycast to detect the end point of the line renderer
        RaycastHit hit;
        bool isHit = false;

        if (m_isTutorial)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Enemy");
            isHit = Physics.Raycast(transform.position, transform.forward, out hit, 100.0f, layerMask);
        }
        else
        {
            isHit = Physics.Raycast(transform.position, transform.forward, out hit);
        }
        
        if (isHit)
        {
            m_lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            m_lineRenderer.SetPosition(1, transform.position + transform.forward * 100);
        }
    }
}
