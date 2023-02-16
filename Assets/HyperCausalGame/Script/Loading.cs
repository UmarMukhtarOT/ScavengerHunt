using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{

    public string SceneToLoad = "Gameplay";
    public Image FillBar = null;

    public bool FirstScene = false;

    AsyncOperation asyncOperation = null;
    void Awake()
    {

        StartCoroutine(LoadScene());

    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1f);

        //Begin to load the Scene you specify
        asyncOperation = SceneManager.LoadSceneAsync(SceneToLoad);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        //Debug.Log("Pro :" + asyncOperation.progress);

        LoadGamePlayScene();






    }

    public void LoadGamePlayScene()
    {
        StartCoroutine(SceneLoad(asyncOperation));
    }

    IEnumerator SceneLoad(AsyncOperation asyncOperation)
    {
        while (!asyncOperation.isDone)
        {

            FillBar.fillAmount = asyncOperation.progress;

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {

                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }






}