using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        // StartCoroutine(LoadSceneAsync(sceneName));
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (asyncLoad != null && !asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
