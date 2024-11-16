using UnityEngine;
using UnityEngine.Serialization;

namespace GEF
{
    [DisallowMultipleComponent]
    public class Highlight : MonoBehaviour
    {
        #region Properties
        [SerializeField] private Material m_highlightMaterial = null;
        private Material m_originalMaterial = null;
        private Renderer m_renderer         = null;
        #endregion

        #region Lifecycle
        private void Awake()
        {
            m_renderer = GetComponent<Renderer>();
            m_originalMaterial = m_renderer.material;
        }
        #endregion

        #region Public Methods
        public void EnableHighlight()
        {
            m_renderer.material = m_highlightMaterial;
        }

        public void DisableHighlight()
        {
            m_renderer.material = m_originalMaterial;
        }
        #endregion
    }
}
