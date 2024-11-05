using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    private TMP_Text m_scoreText = null;

    private void Awake()
    {
        m_scoreText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        var highScore = PlayerPrefs.GetInt("HighScore", 0);
        m_scoreText.text = highScore.ToString();
    }
}
