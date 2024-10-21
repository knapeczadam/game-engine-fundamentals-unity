using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaticCat : MonoBehaviour
{
    [SerializeField] private GameObject _aiCat = null;
    
    private void Awake()
    {
        _aiCat.GetComponent<SeekingBehaviour>().Target = transform.root.gameObject;
    }
}
