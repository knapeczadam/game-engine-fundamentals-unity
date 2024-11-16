using UnityEngine;

namespace GEF
{
    public class UILookAt : MonoBehaviour
    {
        #region Lifecycle
        private void LateUpdate()
        {
            transform.LookAt(Camera.main.transform);
        }
        #endregion
    }
}
