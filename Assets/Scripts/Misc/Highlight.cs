using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    private Material _originalMaterial;
    
    [SerializeField]
    private Material _highlightMaterial;
    
    private void Awake()
    {
        var rendererComponent = GetComponent<Renderer>();
        _originalMaterial = rendererComponent.material;
    }
    
    public void EnableHighlight()
    {
        
        var rendererComponent = GetComponent<Renderer>();
        rendererComponent.material = _highlightMaterial;
    }
    
    public void DisableHighlight()
    {
        var rendererComponent = GetComponent<Renderer>();
        rendererComponent.material = _originalMaterial;
    }
}
