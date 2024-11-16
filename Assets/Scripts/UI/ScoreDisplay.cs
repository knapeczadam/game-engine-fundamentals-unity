using TMPro;
using UnityEngine;

namespace GEF
{
    public class ScoreDisplay : MonoBehaviour
    {
        #region Properties
        private TMP_Text m_scoreText = null;
        #endregion

        #region Lifecycle
        private void Awake()
        {
            m_scoreText = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            ScoreManager.Instance.OnScoreChanged += UpdateScoreText;
            UpdateScoreText(ScoreManager.Instance.m_score);
        }

        private void OnDestroy()
        {
            if (ScoreManager.m_exists)
            {
                ScoreManager.Instance.OnScoreChanged -= UpdateScoreText;
            }
        }
        #endregion

        #region Methods
        private void UpdateScoreText(int score)
        {
            m_scoreText.text = score.ToString();
        }
        #endregion
    }
}
