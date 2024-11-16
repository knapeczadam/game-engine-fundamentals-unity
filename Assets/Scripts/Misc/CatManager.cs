using UnityEngine;

namespace GEF
{
    public class CatManager : MonoBehaviour
    {
        #region Fields
        public int m_catCount { get; set; } = 0;
        public delegate void CatCountChange(int catCount, CatManager catManager);
        public event CatCountChange OnCatCountChange = null;
        #endregion

        #region Properties
        [SerializeField] private GameObject m_fx = null;
        [SerializeField] private bool m_destroyCat = false;
        private PickUpBehaviour m_pickUpBehaviour = null;
        private bool            m_inSafeZone      = false;
        private bool            m_isDay           = false;
        #endregion

        #region Lifecycle
        private void Awake()
        {
            var player = FindObjectOfType<PlayerCharacter>();
            if (player)
            {
                m_pickUpBehaviour = player.GetComponent<PickUpBehaviour>();
            }
        }

        public void Reset()
        {
            m_catCount = 0;
            OnCatCountChange?.Invoke(m_catCount, this);
            m_isDay = false;
            m_inSafeZone = false;
        }

        private void Start()
        {
            if (m_pickUpBehaviour)
            {
                m_pickUpBehaviour.OnPickUp += HandlePickUp;
            }
        }
        #endregion

        #region Public Methods
        public bool DestroyCat => m_destroyCat;
        
        public bool SafeZoneActive()
        {
            return m_inSafeZone && m_isDay;
        }
        
        public void SetDay(bool isDay)
        {
            m_isDay = isDay;
        }

        #endregion

        #region Methods
        private void HandlePickUp(bool catPickedUp)
        {
            if (m_isDay && !catPickedUp && m_inSafeZone)
            {
                m_catCount++;
                OnCatCountChange?.Invoke(m_catCount, this);
                Debug.Log($"Cat count: {m_catCount}");
                if (m_fx)
                {
                    Instantiate(m_fx, transform.position, Quaternion.identity);
                }

            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.PLAYER))
            {
                var pickUpBehaviour = other.gameObject.GetComponent<PickUpBehaviour>();
                if (pickUpBehaviour)
                {
                    if (pickUpBehaviour.m_catPickedUp)
                    {
                        m_inSafeZone = true;
                        GetComponentInChildren<Highlight>().EnableHighlight();
                    }
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.PLAYER))
            {
                var pickUpBehaviour = other.gameObject.GetComponent<PickUpBehaviour>();
                if (pickUpBehaviour)
                {
                    if (pickUpBehaviour.m_catPickedUp)
                    {
                        m_inSafeZone = true;
                        GetComponentInChildren<Highlight>().EnableHighlight();
                    }
                    else
                    {
                        GetComponentInChildren<Highlight>().DisableHighlight();
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.PLAYER))
            {
                GetComponentInChildren<Highlight>().DisableHighlight();
                m_inSafeZone = false;
            }
        }
        #endregion
    }
}
