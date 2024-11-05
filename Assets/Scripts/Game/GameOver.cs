using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject m_player = null;

    private void Update()
    {
        if (m_player == null)
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        SceneManager.LoadScene(0);
    }
}
