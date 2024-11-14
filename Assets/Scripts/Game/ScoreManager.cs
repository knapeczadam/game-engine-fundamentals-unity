using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int m_score { get; private set; } = 0;
    public delegate void ScoreChanged(int newScore);
    public event ScoreChanged OnScoreChanged;
    
    private static ScoreManager _instance = null;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null && !m_applicationQuitting)
            {
                _instance = FindObjectOfType<ScoreManager>();
                if (_instance == null)
                {
                    GameObject newInstance = new GameObject("Singleton_SpawnManager");
                    _instance = newInstance.AddComponent<ScoreManager>();
                }
            }
            return _instance;
        }
    }

    public static bool m_exists { get { return _instance != null; } }
    public static bool m_applicationQuitting = false;

    protected void OnApplicationQuit()
    {
        m_applicationQuitting = true;
    }

    private void Awake()
    {
        // DontDestroyOnLoad(gameObject);
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
    
    public void AddScore(int score)
    {
        m_score += score;
        OnScoreChanged?.Invoke(m_score);
        
        PlayerPrefs.SetInt("CurrentScore", m_score);
        
        if (m_score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", m_score);
        }
    }

    public void Reset()
    {
        m_score = 0;
        OnScoreChanged?.Invoke(m_score);
    }
}
