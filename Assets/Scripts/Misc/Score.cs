using UnityEngine;
using UnityEngine.Serialization;

public class Score : MonoBehaviour
{
    [SerializeField] private int m_hitScore = 0;
    [SerializeField] private int m_killScore = 0;
    
    public int HitScore
    {
        get
        {
            return m_hitScore;
        }
    }
    
    public int KillScore
    {
        get
        {
            return m_killScore;
        }
    }
}
