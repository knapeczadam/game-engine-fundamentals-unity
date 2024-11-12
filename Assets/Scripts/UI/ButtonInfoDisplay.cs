using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonInfoDisplay : MonoBehaviour
{ 
    private TMP_Text m_infoText = null;
    
    private void Awake()
    {
        m_infoText = GetComponentInChildren<TMP_Text>();
    }

    public void UpdateText(string info)
    {
        m_infoText.text = info;
    }
}
