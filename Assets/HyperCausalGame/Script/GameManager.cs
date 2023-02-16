using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public enum GameState
    {
        MainMenu,
        GamePlay,
        GameOver,
        LevelComplete

    }
    public GameState gamestate = GameState.MainMenu;

    public static GameManager instance = null;

    [Header("kindly go through all thanks")]
    [Header("--------Caution------- Template Keys functions[Level-Start , GameOver , LevelComplete]  design using '--Event System Delegate events--'")]
    public UIManager UI = null;
    public LevelCompleteManager levelCompleteManager = null;
    public GameOverManager gameOverManager = null;



    public delegate void GameStarFunc();
    public GameStarFunc gameStarFuncEvent;

    public delegate void GameResetFunc();
    public GameStarFunc gameResetFuncEvent;








    private void Awake()
    {
        instance = this;
        gameResetFuncEvent += GameReset;
    }


    public void SetMainMenuState()
    {
        this.gamestate = GameState.MainMenu;
    }
    public void SetGamePlayState()
    {
        this.gamestate = GameState.GamePlay;
    }
    public void SetGameOverState()
    {
        this.gamestate = GameState.GameOver;
    }
    public void SetLevelCompleteState()
    {
        this.gamestate = GameState.LevelComplete;
    }
    public bool IsMainMenuState()
    {
        return this.gamestate == GameState.MainMenu;
    }
    public bool IsGamePlayState()
    {
        return this.gamestate == GameState.GamePlay;
    }
    public bool IsGameOverState()
    {
        return this.gamestate == GameState.GameOver;
    }
    public bool IsLevelCompleteState()
    {
        return this.gamestate == GameState.LevelComplete;
    }

    public void GameStart()
    {
        gameStarFuncEvent.Invoke();
        SetGamePlayState();
        //UIManager will handle the UI On Off Setting using event system if consfusion? visit it
    }
    public void GameReset()
    {

        SetMainMenuState();
        //UIManager will handle the UI On Off Setting using event system if consfusion? visit it
    }

    public void RetryBtn()
    {
        gameResetFuncEvent.Invoke();
        LoadSceneFunction("GamePlay");
    }
    public void LoadSceneFunction(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void NextBtn()
    {
        int lvl = GameData.instance.GetLevelNumber();

        if ((lvl % (LevelManager.instance.Levels.Count) == 0))
        {


            LevelManager.instance.LevelsArrayReshuffle();

        }

        lvl++;

        GameData.instance.SetLevelNumber(lvl);



        lvl = GameData.instance.GetLevelNumberIndex();
        lvl++;
        if ((lvl) > (LevelManager.instance.Levels.Count))
            lvl = 1;
        GameData.instance.SetLevelNumberIndex(lvl);

        gameResetFuncEvent.Invoke();

        LoadSceneFunction("GamePlay");


        //UIManager will handle the UI On Off Setting using event system if consfusion? visit it


    }



}
