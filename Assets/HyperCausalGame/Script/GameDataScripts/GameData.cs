using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance = null;
    private void Awake()
    {
        instance = this;
        Initialization();
    }
    public void Initialization()
    {
        if (!GameVersionHasKey(1)) //version 1.0 //1
        {

            SetGameVerionKey(1, 1);
            SetCoins(0);

            SetLevelNumber(1);
        }

    }
    #region GameVersions

    public bool GameVersionHasKey(int veriosnnumber)
    {
        return PlayerPrefs.HasKey(GamePlayerPrefKeys.GameVersion + veriosnnumber);
    }

    public void SetGameVerionKey(int veriosnnumber, int value)
    {
        PlayerPrefs.SetInt(GamePlayerPrefKeys.GameVersion + veriosnnumber, value);
    }
    public int GetGameVersionKey(int veriosnnumber)
    {
        return PlayerPrefs.GetInt(GamePlayerPrefKeys.GameVersion + veriosnnumber, 0);
    }
    #endregion


    #region GameCurrency
    public void SetCoins(int value)
    {
        PlayerPrefs.SetInt(GamePlayerPrefKeys.Coins, value);
    }
    public int GetCoins()
    {
        return PlayerPrefs.GetInt(GamePlayerPrefKeys.Coins, 0);
    }

    #endregion
    #region Level
    public void SetLevelNumber(int value)
    {
        PlayerPrefs.SetInt(GamePlayerPrefKeys.Level, value);
    }
    public int GetLevelNumber()
    {
        return PlayerPrefs.GetInt(GamePlayerPrefKeys.Level, 1);
    }
    public void SetLevelNumberIndex(int value)
    {
        PlayerPrefs.SetInt(GamePlayerPrefKeys.LevelsIndex, value);
    }
    public int GetLevelNumberIndex()
    {
        return PlayerPrefs.GetInt(GamePlayerPrefKeys.LevelsIndex, 1);
    }








    public bool LevelRandomizationOrderHasKey()
    {

        return PlayerPrefs.HasKey(GamePlayerPrefKeys.LevelsRandomizationOrder);


    }
    public void SetLevelRandomizationOrder(LevelRandomization LR)
    {
        PlayerPrefs.SetString(GamePlayerPrefKeys.LevelsRandomizationOrder, JsonUtility.ToJson(LR));
    }
    public LevelRandomization GetLevelRandomizationOrder()
    {
        return JsonUtility.FromJson<LevelRandomization>(PlayerPrefs.GetString(GamePlayerPrefKeys.LevelsRandomizationOrder));
    }



    public void SetLevelAttemptRate(int count)
    {
        PlayerPrefs.SetInt(GamePlayerPrefKeys.LevelAttemptRate, count);
    }
    public int GetLevelAttemptRate()
    {
        return PlayerPrefs.GetInt(GamePlayerPrefKeys.LevelAttemptRate, 1);
    }

    #endregion
}
