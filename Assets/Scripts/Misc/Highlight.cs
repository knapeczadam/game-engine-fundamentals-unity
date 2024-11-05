using UnityEngine;
using UnityEngine.Serialization;

[DisallowMultipleComponent]
public class Highlight : MonoBehaviour
{
    [SerializeField] private Material m_highlightMaterial = null;
    private Material m_originalMaterial = null;
    private Renderer m_renderer         = null;
    
    private void Awake()
    {
        m_renderer = GetComponent<Renderer>();
        m_originalMaterial = m_renderer.material;
    }
    
    public void EnableHighlight()
    {
        m_renderer.material = m_highlightMaterial;
    }
    
    public void DisableHighlight()
    {
        m_renderer.material = m_originalMaterial;
    }
}
