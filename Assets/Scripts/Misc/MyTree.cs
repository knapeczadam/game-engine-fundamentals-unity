using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent]
public class MyTree : MonoBehaviour
{
    private List<Highlight> _highlights = new List<Highlight>();
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
        var cat = gameObject.GetComponentInChildren<Cat>();
        return cat != null;
    }
}
