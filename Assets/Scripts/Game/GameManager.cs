using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace GEF
{
    public class GameManager : MonoBehaviour
    {
        #region Properties
        [SerializeField] private GameObject m_player = null;
        [SerializeField] private UnityEvent m_onGameOver = null;
        private bool m_gameOver = false;
        #endregion

        #region Lifecycle
        private void Update()
        {
            if (!m_gameOver && m_player == null)
            {
                m_gameOver = true;
                TriggerGameOver();
            }
        }
        #endregion

        #region Public Methods
        public void ToggleTimeScale()
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }
        #endregion

        #region Methods
        private void TriggerGameOver()
        {
            m_onGameOver?.Invoke();
        }
        #endregion
    }
}
