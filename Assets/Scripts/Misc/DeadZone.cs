using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GEF
{
    public class DeadZone : MonoBehaviour
    {
        #region Lifecycle
        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
        }
        #endregion
    }
}