using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{
    public static MenuScripts instance { get; private set; }
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else { instance = this; }
    }
    public void LoadScene(int scene)
    {
        instance.StartCoroutine(LoadAsyncScene(scene));
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
