using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    private UIDocument    _attachedDocument = null;
    private VisualElement _root             = null;
    private IntegerField  _catCountField    = null;

    private void Start()
    {
        _attachedDocument = GetComponent<UIDocument>();
        if (_attachedDocument)
        {
            _root = _attachedDocument.rootVisualElement;
        }

        if (_root != null)
        {
            _catCountField = _root.Q<IntegerField>();

            CatManager catManager = FindObjectOfType<CatManager>();
            if (catManager)
            {
                _catCountField.value = catManager.CatCount;
                catManager.OnCatCountChange += (catCount) => _catCountField.value = catCount;
            }
        }
    }
}
