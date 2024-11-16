using UnityEngine;
using UnityEngine.UIElements;

namespace GEF
{
    public class HUD : MonoBehaviour
    {
        #region Properties
        private UIDocument    m_attachedDocument = null;
        private VisualElement m_root             = null;
        private IntegerField  m_catCountField    = null;
        #endregion

        #region Lifecycle
        private void Start()
        {
            m_attachedDocument = GetComponent<UIDocument>();
            if (m_attachedDocument)
            {
                m_root = m_attachedDocument.rootVisualElement;
            }

            if (m_root != null)
            {
                m_catCountField = m_root.Q<IntegerField>();

                CatManager catManager = FindObjectOfType<CatManager>();
                if (catManager)
                {
                    m_catCountField.value = catManager.m_catCount;
                    catManager.OnCatCountChange += OnCatCountChange;
                }
            }
        }
        #endregion

        #region Methods
        private void OnCatCountChange(int catCount, CatManager catManager)
        {
            m_catCountField.value = catCount;
        }
        #endregion
    }
}
