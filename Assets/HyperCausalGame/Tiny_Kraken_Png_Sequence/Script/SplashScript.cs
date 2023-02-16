using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour
{
    public string SceneName;
    public float DelayTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("NextSceneLoad", DelayTime);
    }

    public void NextSceneLoad()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void RetryScene()
    {
        SceneManager.LoadScene("SplashScene");
    }
}
