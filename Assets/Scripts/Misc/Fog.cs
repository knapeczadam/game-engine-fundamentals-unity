using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GEF
{
    public class Fog : MonoBehaviour
    {
        #region Public Methods

        public void EnableFog()
        {
            RenderSettings.fog = true;
        }

        public void DisableFog()
        {
            RenderSettings.fog = false;
        }

        #endregion
    }
}
