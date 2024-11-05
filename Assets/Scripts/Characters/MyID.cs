using UnityEngine;
using UnityEngine.Serialization;

public class MyID : MonoBehaviour
{
    [SerializeField] private int m_id = 0;

    public int GetID()
    {
        return m_id;
    }
}
