using UnityEngine;
using UnityEngine.Serialization;

namespace GEF
{
    public class ID : MonoBehaviour
    {
        #region Properties
        [SerializeField] private int m_id = 0;
        #endregion

        #region Public Methods
        public int GetID()
        {
            return m_id;
        }
        #endregion
    }
}