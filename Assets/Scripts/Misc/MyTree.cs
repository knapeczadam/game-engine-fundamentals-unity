using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[DisallowMultipleComponent]
public class MyTree : MonoBehaviour
{
    [SerializeField] private Transform m_catSocket = null;
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
        if (HasCat() && other.CompareTag(Tags.PLAYER))
        {
            foreach (var highlight in m_highlights)
            {
                highlight.EnableHighlight();
            }
        }
        else if (!HasCat() && other.CompareTag(Tags.CAT))
        {
            var aiCat = other.GetComponent<AICat>();
            if (aiCat)
            {
                var rootCat = aiCat.transform.parent;
                var staticCat = rootCat.GetComponentInChildren<StaticCat>(true);
                
                rootCat.SetParent(m_catSocket, false);
                aiCat.gameObject.SetActive(false);
                staticCat.gameObject.SetActive(true);
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

    public bool HasCat()
    {
        var cat = gameObject.GetComponentInChildren<StaticCat>();
        return cat != null;
    }
}
