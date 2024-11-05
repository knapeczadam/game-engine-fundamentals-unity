using UnityEngine;

public class CatManager : MonoBehaviour
{
    public int              m_catCount { get; private set; } = 0;
    public delegate void    CatCountChange(int catCount);
    public event CatCountChange OnCatCountChange = null;

    private PickUpBehaviour m_pickUpBehaviour = null;
    private bool m_inSafeZone  = false;
    private bool            m_isDay           = false;
    
    private void Awake()
    {
        var player = FindObjectOfType<PlayerCharacter>();
        if (player)
        {
            m_pickUpBehaviour = player.GetComponent<PickUpBehaviour>();
        }
    }

    private void Start()
    {
        if (m_pickUpBehaviour)
        {
            m_pickUpBehaviour.OnPickUp += HandlePickUp;
        }
    }

    private void HandlePickUp(bool catPickedUp)
    {
        if (m_isDay && !catPickedUp && m_inSafeZone)
        {
            m_catCount++;
            OnCatCountChange?.Invoke(m_catCount);
            Debug.Log($"Cat count: {m_catCount}");
            
        }
    }
    
    public bool SafeZoneActive()
    {
        return m_inSafeZone && m_isDay;
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
    
    public void SetDay(bool isDay)
    {
        m_isDay = isDay;
    }

    public void Reset()
    {
        m_catCount = 0;
        OnCatCountChange?.Invoke(m_catCount);
        m_isDay = false;
        m_inSafeZone = false;
    }
}
