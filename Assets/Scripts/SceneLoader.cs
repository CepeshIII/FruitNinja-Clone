using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader: Singleton<SceneLoader>
{
    public void LoadMenuScene()
    {
        StartCoroutine(LoadScene(0));
    }

    public void LoadMainScene()
    {
        StartCoroutine(LoadScene(1));
    }

    public IEnumerator LoadScene(int sceneBuildIndex)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(sceneBuildIndex);

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
