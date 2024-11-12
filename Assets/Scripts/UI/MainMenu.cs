using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(LoadGameSceneAsync());
    }
    
    IEnumerator LoadGameSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("New Scene");
        while (asyncLoad != null && !asyncLoad.isDone)
        {
            yield return null;
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void LoadMainMenu()
    {
        StartCoroutine(LoadMainMenuSceneAsync());
    }
    
    private IEnumerator LoadMainMenuSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");
        while (asyncLoad != null && !asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
