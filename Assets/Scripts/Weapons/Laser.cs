using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GEF
{
    public class Laser : MonoBehaviour
    {
        #region Properties
        [SerializeField] private LayerMask m_ignoredLayer = 0;
        private LineRenderer m_lineRenderer = null;
        #endregion

        #region Lifecycle
        private void Awake()
        {
            m_lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            // Set the start point of the line renderer
            m_lineRenderer.SetPosition(0, transform.position);

            // raycast to detect the end point of the line renderer
            bool isHit = Physics.Raycast(transform.position, transform.forward, out var hit, 100.0f, ~m_ignoredLayer);

            if (isHit)
            {
                m_lineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                m_lineRenderer.SetPosition(1, transform.position + transform.forward * 100);
            }
        }
        #endregion
    }
}