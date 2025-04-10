using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader: Singleton<SceneLoader>
{
    public void LoadMainMenuScene()
    {
        StartCoroutine(LoadScene(0, LoadSceneMode.Single));
    }

    public void LoadMainScene()
    {
        StartCoroutine(LoadScene(1, LoadSceneMode.Single));
    }

    public void LoadMenuScene()
    {
        StartCoroutine(LoadScene(2, LoadSceneMode.Additive));
    }

    public IEnumerator LoadScene(int sceneBuildIndex, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(sceneBuildIndex, loadSceneMode);

        if(asyncOperation != null)
        {
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
