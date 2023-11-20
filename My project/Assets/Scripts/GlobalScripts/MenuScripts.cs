using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class MenuScripts : MonoBehaviour
{
    private MenuScripts() { }
    public static MenuScripts _instance;

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }

    public void LoadScene(int scene)
    {
        StartCoroutine(LoadAsyncScene(scene));
    }

    private IEnumerator LoadAsyncScene(int scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
