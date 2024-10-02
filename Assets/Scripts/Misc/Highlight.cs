using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent]
public class Highlight : MonoBehaviour
{
    private Material _originalMaterial;
    private Renderer _renderer;
    
    [SerializeField]
    private Material _highlightMaterial;
    
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _originalMaterial = _renderer.material;
    }
    
    public void EnableHighlight()
    {
        _renderer.material = _highlightMaterial;
    }
    
    public void DisableHighlight()
    {
        _renderer.material = _originalMaterial;
    }
}
