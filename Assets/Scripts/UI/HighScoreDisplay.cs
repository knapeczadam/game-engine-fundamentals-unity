using System;
using TMPro;
using UnityEngine;

namespace GEF
{
    public class HighScoreDisplay : MonoBehaviour
    {
        #region Properties
        [SerializeField] private TMP_Text m_highScoreText    = null;
        [SerializeField] private TMP_Text m_currentScoreText = null;
        #endregion

        #region Lifecycle
        private void OnEnable()
        {
            if (m_highScoreText)
            {
                var highScore = PlayerPrefs.GetInt("HighScore", 0);
                m_highScoreText.text = highScore.ToString();
            }

            if (m_currentScoreText)
            {
                var currentScore = PlayerPrefs.GetInt("CurrentScore", 0);
                m_currentScoreText.text = currentScore.ToString();
            }
        }
        #endregion
    }
}
