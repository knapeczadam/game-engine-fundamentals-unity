using UnityEngine;

public class StaticCat : MonoBehaviour
{
    [SerializeField] private GameObject m_aiCat = null;
    
    private void Awake()
    {
        // m_aiCat.GetComponent<SeekingBehaviour>().m_target = transform.root.gameObject;
    }
}
