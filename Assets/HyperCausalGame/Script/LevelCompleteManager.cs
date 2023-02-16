using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{

    public delegate void LevelCompleteFunc();
    public LevelCompleteFunc levelCompleteFuncEvent;

    private Coroutine LevelCompleteCor = null;
    public void LevelComplete(float time)
    {

        if (LevelCompleteCor != null)
            this.StopCoroutine(LevelCompleteCor);
        LevelCompleteCor = StartCoroutine(DelayLevelComplete(time));


    }


    IEnumerator DelayLevelComplete(float time)
    {


        yield return new WaitForSeconds(time);


        if (GameManager.instance.IsGamePlayState())
        {



            try
            {
                GameManager.instance.SetLevelCompleteState();












                //UIManager will handle the UI On Off Setting using event system if consfusion? visit it



                GameData.instance.SetLevelAttemptRate(1);

                levelCompleteFuncEvent.Invoke();

                SoundsManager.instance.PlayLevelWinSound(SoundsManager.instance.AS);


                CurrencyAnimationHandler.instance.RunGemAnimation(Vector3.zero, Vector3.zero, Vector3.one, Vector3.one);





            }
            catch (System.Exception e)
            {
                Debug.LogError(e.ToString());

            }
        }
    }


}
