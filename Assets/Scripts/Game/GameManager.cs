using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_player = null;
    [SerializeField] private List<UnityEvent> m_onGameOver = new List<UnityEvent>();

    private void Update()
    {
        if (m_player == null)
        {
            TriggerGameOver();
        }
    }
    
    void TriggerGameOver()
    {
        foreach (var unityEvent in m_onGameOver)
        {
            unityEvent.Invoke();
        }
    }
    
    public void ToggleTimeScale()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

}
