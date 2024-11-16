using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GEF
{
    public class FreeZombie_EyesGlow : MonoBehaviour
    {
        #region Fields
        public Material[] BodyMaterials = new Material[1];
        public enum EyesGlow
        {
            No,
            Yes
        }

        public EyesGlow eyesGlow;

        #endregion
        
        #region Properties
        private int eyesTyp;
        #endregion

        #region Lifecycle
        void OnValidate()
        {
            if (eyesGlow == 0)
            {
                BodyMaterials[0].DisableKeyword("_EMISSION");
                BodyMaterials[0].SetFloat("_EmissiveExposureWeight", 1);
            }
            else
            {
                BodyMaterials[0].EnableKeyword("_EMISSION");
                BodyMaterials[0].SetFloat("_EmissiveExposureWeight", 0);

            }
        }
        #endregion
    }
}
