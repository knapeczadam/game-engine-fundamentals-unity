using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    private UIDocument    m_attachedDocument = null;
    private VisualElement m_root             = null;
    private IntegerField  m_catCountField    = null;

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
                catManager.OnCatCountChange += (catCount) => m_catCountField.value = catCount;
            }
        }
    }
}
