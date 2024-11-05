using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    private TMP_Text m_scoreText = null;

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
    
    private void UpdateScoreText(int score)
    {
        m_scoreText.text = score.ToString();
    }
}
