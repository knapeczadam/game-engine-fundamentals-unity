using UnityEngine;

namespace GEF
{
    public class Blood : MonoBehaviour
    {
        #region Properties
        [SerializeField, Range(1.0f, 20.0f)] private float m_minTimeToDestroy = 3.0f;
        [SerializeField, Range(1.0f, 20.0f)] private float m_maxTimeToDestroy = 10.0f;
        #endregion

        #region Lifecycle
        private void Start()
        {
            // If the random number is 1, destroy the object after a random time between _minTimeToDestroy and _maxTimeToDestroy
            if (Random.Range(0, 1) == 1)
            {
                var randomTime = Random.Range(m_minTimeToDestroy, m_maxTimeToDestroy);
                Destroy(gameObject, randomTime);
            }
        }
        #endregion
    }
}