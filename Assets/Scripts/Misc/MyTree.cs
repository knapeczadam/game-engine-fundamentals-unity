using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent]
public class MyTree : MonoBehaviour
{
    private List<Highlight> _highlights = new List<Highlight>();
    
    [SerializeField]
    private Transform _catSocket = null;
    
    private void Awake()
    {
        var highlightComponents = GetComponentsInChildren<Highlight>();
        foreach (var highlightComponent in highlightComponents)
        {
            _highlights.Add(highlightComponent);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (HasCat() && other.CompareTag(Tags.FRIEND))
        {
            foreach (var highlight in _highlights)
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
                
                rootCat.SetParent(_catSocket, false);
                aiCat.gameObject.SetActive(false);
                staticCat.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.FRIEND))
        {
            foreach (var highlight in _highlights)
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
