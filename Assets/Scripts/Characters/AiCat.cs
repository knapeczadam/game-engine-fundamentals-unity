using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class AICat : MonoBehaviour
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
        if (other.CompareTag(Tags.FRIEND))
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
}
