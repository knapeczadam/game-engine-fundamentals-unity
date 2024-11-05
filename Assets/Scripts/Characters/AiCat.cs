using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AICat : MonoBehaviour
{
    private List<Highlight> m_highlights = new List<Highlight>();
    
    private void Awake()
    {
        var highlightComponents = GetComponentsInChildren<Highlight>();
        foreach (var highlightComponent in highlightComponents)
        {
            m_highlights.Add(highlightComponent);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            foreach (var highlight in m_highlights)
            {
                highlight.EnableHighlight();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            foreach (var highlight in m_highlights)
            {
                highlight.DisableHighlight();
            }
        }
    }
}
