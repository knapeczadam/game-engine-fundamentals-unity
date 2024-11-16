using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GEF
{
    public class ButtonInfoDisplay : MonoBehaviour
    {
        #region Properties
        private TMP_Text m_infoText = null;
        #endregion

        #region Lifecycle
        private void Awake()
        {
            m_infoText = GetComponentInChildren<TMP_Text>();
        }
        #endregion

        #region Public Methods
        public void UpdateText(string info)
        {
            m_infoText.text = info;
        }
        #endregion
    }
}
