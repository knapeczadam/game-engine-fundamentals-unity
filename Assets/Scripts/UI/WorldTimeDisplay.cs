using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace GEF
{
    [RequireComponent(typeof(TMP_Text))]
    public class WorldTimeDisplay : MonoBehaviour
    {
        #region Properties
        [SerializeField] private WorldTime m_worldTime = null;
        private TMP_Text m_text;
        #endregion

        #region Lifecycle
        private void Awake()
        {
            m_text = GetComponent<TMP_Text>();
            m_worldTime.WorldTimeChanged += OnWorldTimeChanged;
        }

        private void OnDestroy()
        {
            m_worldTime.WorldTimeChanged -= OnWorldTimeChanged;
        }
        #endregion

        #region Methods
        private void OnWorldTimeChanged(object sender, TimeSpan newTime)
        {
            m_text.SetText(newTime.ToString(@"hh\:mm"));
        }
        #endregion
    }
}
