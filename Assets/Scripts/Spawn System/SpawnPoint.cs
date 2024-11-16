using UnityEngine;
using UnityEngine.Serialization;

namespace GEF
{
    public class SpawnPoint : MonoBehaviour
    {
        #region Properties
        [SerializeField] private GameObject m_spawnTemplate = null;
        #endregion

        #region Lifecycle
        private void OnEnable()
        {
            SpawnManager.Instance.RegisterSpawnPoint(this);

            SpawnManager.Instance.SpawnWave();
        }

        private void OnDisable()
        {
            if (SpawnManager.Exists)
            {
                SpawnManager.Instance.UnregisterSpawnPoint(this);
            }
        }
        #endregion

        #region Public Methods
        public GameObject Spawn()
        {
            return Instantiate(m_spawnTemplate, transform.position, transform.rotation);
        }
        #endregion
    }
}
