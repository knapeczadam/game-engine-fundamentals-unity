using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GEF
{
    public class MenuManager : MonoBehaviour
    {
        #region Public Methods
        public void LoadScene(string sceneName)
        {
            // StartCoroutine(LoadSceneAsync(sceneName));
            SceneManager.LoadScene(sceneName);
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }
        #endregion

        #region Methods
        private IEnumerator LoadSceneAsync(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (asyncLoad != null && !asyncLoad.isDone)
            {
                yield return null;
            }
        }
        #endregion
    }
}
