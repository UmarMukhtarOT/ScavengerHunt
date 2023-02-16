using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public delegate void GameOverFunc();
    public GameOverFunc gameOverFuncEvent;
    private Coroutine GameOverCor = null;
    public void GameOver(float time)
    {

        if (GameOverCor != null)
            this.StopCoroutine(GameOverCor);
        GameOverCor = StartCoroutine(DelayGameOver(time));


    }
    IEnumerator DelayGameOver(float time)
    {
        yield return new WaitForSeconds(time);

        if ((GameManager.instance.IsGamePlayState()) || (GameManager.instance.IsMainMenuState()))
        {


            yield return new WaitForSeconds(1f);



            try
            {

                GameManager.instance.SetGameOverState();


                //UIManager will handle the UI On Off Setting using event system if consfusion? visit it


                int attempt = GameData.instance.GetLevelAttemptRate();

                attempt++;
                GameData.instance.SetLevelAttemptRate(attempt);

                gameOverFuncEvent.Invoke();

                SoundsManager.instance.PlayLevelFailSound(SoundsManager.instance.AS);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.ToString());

            }
        }

    }
}
