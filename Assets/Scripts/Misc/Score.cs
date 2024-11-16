using UnityEngine;
using UnityEngine.Serialization;

namespace GEF
{
    public class Score : MonoBehaviour
    {
        #region Properties
        [SerializeField, Range(0, 10)] private int m_hitScore  = 0;
        [SerializeField, Range(0, 10)] private int m_killScore = 0;
        #endregion

        #region Public Methods
        public int HitScore
        {
            get { return m_hitScore; }
        }

        public int KillScore
        {
            get { return m_killScore; }
        }
        #endregion
    }
}
