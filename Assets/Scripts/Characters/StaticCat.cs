using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaticCat : MonoBehaviour
{
    private void Awake()
    {
        var rootCat = gameObject.transform.parent;
        var aiCat = rootCat.GetComponentInChildren<AiCat>(true);
        aiCat.GetComponent<SeekingBehaviour>().Target = rootCat.root.gameObject;
    }
}
