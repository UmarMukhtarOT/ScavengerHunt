

//using System;
//using Firebase.Analytics;
//using UnityEngine;


//public class FireBaseListener : MonoBehaviour
//{
//    public static FireBaseListener FireBaseListener_ins;
//    public bool EnableDebug=true;


//    private void Start()
//    {
//        FireBaseListener_ins = this;
//    }


//    void showDebugLog(string log)
//    {
//        if (EnableDebug)
//        {
//           // Debug.Log("``````````````````loging**************");
//            Debug.Log("<color=green>==========</color>" + log+ "<color=green>==========</color>"); 
//        }


//    }




//    //Supermarket Pause Fail Success Events

//    // *********************   Consent Scene ************************** 

//    public void UserConsent_Panel_Opened()
//    {

      
       
//            showDebugLog("UserConsent_Panel_Opened");

       
       

//        FbAnalytics.Instance.LogEvent("UserConsent_Panel_Opened");
//    }
//    public void UserConsent_Panel_Closed()
//    {


//        showDebugLog("UserConsent_Panel_Closed");

       

//        FbAnalytics.Instance.LogEvent("UserConsent_Panel_Closed");
//    }

//    public void UserConsentYes_BtnClick()
//    {
        
//        {
//            showDebugLog("UserConsent_Panel_Yes_BtnClick");

//        }


//        FbAnalytics.Instance.LogEvent("UserConsent_Panel_Yes_BtnClick");
//    }

//    public void UserConsentNo_BtnClick()
//    {
//        {
//            showDebugLog("UserConsent_Panel_No_BtnClick");
//        }   
//        FbAnalytics.Instance.LogEvent("UserConsent_Panel_No_BtnClick");
//    }

//    // *********************   Main Scene ************************** 


//    public void RemoveAd_Panel_Opened()
//    {
//        {
//            showDebugLog("MainMenu_RemoveAd_Panel_Opened");
//        }
//        FbAnalytics.Instance.LogEvent("MainMenu_RemoveAd_Panel_Opened");
//    }
//    public void RemoveAd_Panel_Closed()
//    {
//        {
//            showDebugLog("MainMenu_RemoveAd_Panel_Closed");
//        }
//        FbAnalytics.Instance.LogEvent("MainMenu_RemoveAd_Panel_Closed");
//    }
//    public void RemoveAd_Panel_Accepted()
//    {
//        {
//            showDebugLog("MainMenu_RemoveAd_Panel_Accepted");
//        }
//        FbAnalytics.Instance.LogEvent("MainMenu_RemoveAd_Panel_Accepted");
//    }
//    public void RemoveAd_Panel_Rejected()
//    {
//        {
//            showDebugLog("MainMenu_RemoveAd_Panel_CrossBtn_Click");
//        }
//        FbAnalytics.Instance.LogEvent("MainMenu_RemoveAd_Panel_CrossBtn_Click");
//    }

//    public void MoreApps_BtnClick()
//    {
//        {
//            showDebugLog("MainMenu_MoreApps_BtnClick");
//        }
//        FbAnalytics.Instance.LogEvent("MainMenu_MoreApps_BtnClick");
//    }

//    public void RateUs_BtnClick()
//    {
        
//        {
//            showDebugLog("MainMenu_RateUs_BtnClick");
//        }
//        FbAnalytics.Instance.LogEvent("MainMenu_RateUs_BtnClick");
//    }

//    public void Remove_Ads_BtnClick()
//    {
        
//        {
//            showDebugLog("MainMenu_Remove_Ads_BtnClick");
//        }
//        FbAnalytics.Instance.LogEvent("MainMenu_Remove_Ads_BtnClick");
//    }

//    public void RestorePurchase()
//    {
//        {
//            showDebugLog("MainMenu_Restore_Purchase_BtnClick");
//        }
//        FbAnalytics.Instance.LogEvent("MainMenu_Restore_Purchase_BtnClick");
//    }

//    public void PrivacyBtnClick()
//    {
//        {
//            showDebugLog("MainMenu_PrivacyPolicy_BtnClick");
//        }
//        FbAnalytics.Instance.LogEvent("MainMenu_PrivacyPolicy_BtnClick");
//    }

//    public void MenuScreenOpen()
//    {
        
//        {
//            showDebugLog("MainMenu_Opened");

//        }
        
//        FbAnalytics.Instance.LogEvent("MainMenu_Opened");
//    }

//    public void MenuScreenClose()
//    {
        
//        {
//            showDebugLog("MainMenu_Closed");

//        }
        
//        FbAnalytics.Instance.LogEvent("MainMenu_Closed");
//    }

//    public void UnlockAllGame()
//    {
        
//        {
//            showDebugLog("MainMenu_UnlockAllGame_BtnClick");

//        }

//        FbAnalytics.Instance.LogEvent("MainMenu_UnlockAllGame_BtnClick");
//    }


//    public void Exit_Panel_Opened()
//    {
//        {
//            showDebugLog("Exit_Panel_Opened");
//        }
//        FbAnalytics.Instance.LogEvent("Exit_Panel_Opened");
//    }

//    public void Exit_Panel_Closed()
//    {
//        showDebugLog("Exit_Panel_Closed");
        
//        FbAnalytics.Instance.LogEvent("Exit_Panel_Closed");
//    }

//    public void Exit_Panel_Yes_BtnClick()
//    {
//        {
//            showDebugLog("Exit_Panel_Yes_BtnClick");
//        }
//        FbAnalytics.Instance.LogEvent("Exit_Panel_Yes_BtnClick");
//    }

//    public void Exit_Panel_No_BtnClick()
//    {
//        showDebugLog("Exit_Panel_No_BtnClick");
       
//        FbAnalytics.Instance.LogEvent("Exit_Panel_No_BtnClick");
//    }




//    public void RobotSelectionOpen()
//    {
        
//        {
//            showDebugLog("RobotSelection_Opened");
//        }
//        FbAnalytics.Instance.LogEvent("RobotSelection_Opened");
//    }

//    public void RoboCategorySelected()
//    {
        
//        {
//            showDebugLog("Robot_" + PlayerPrefs.GetInt(MyPlayerPrefs.RoboCategory, 0) + "_Selected");
//        }

//        FbAnalytics.Instance.LogEvent("Robot_" + PlayerPrefs.GetInt(MyPlayerPrefs.RoboCategory, 0) + "_Selected");
//    }

//    public void RoboSkinSelected()
//    {
//        {
//            showDebugLog("Robot_Skin" + "_Selected");
//        }
//        FbAnalytics.Instance.LogEvent("Robot_Skin" + "_Selected");
//    }


//    public void RobotSelectionClose()
//    {
//        {
//            showDebugLog("RobotSelection_Closed");
//        }
//        FbAnalytics.Instance.LogEvent("RobotSelection_Closed");
//    }

//    // *********************   Robot Selection Scene **************************


//    // *********************   Car Selection Scene **************************

//    public void CarSelectionOpen()
//    {
//        {
//            showDebugLog("CarSelection_Opened");
//        }
//        FbAnalytics.Instance.LogEvent("CarSelection_Opened");
//    }

//    public void CarSelectionClose()
//    {
//        {
//            showDebugLog("CarSelection_Closed");
//        }
//        FbAnalytics.Instance.LogEvent("CarSelection_Closed");
//    }

//    // *********************   Car Selection Scene **************************


//    // *********************   Level Selection Scene **************************

//    public void LevelSelectionOpen()
//    {
//        {
//            showDebugLog("LevelSelection_Opened");
//        }
//        FbAnalytics.Instance.LogEvent("LevelSelection_Opened");
//    }

//    public void LevelSelectionClose()
//    {
//        {
//            showDebugLog("Level_No" + GameConstants.currentLevel.ToString() + "Selected");
//        }
//        FbAnalytics.Instance.LogEvent("Level_No" + GameConstants.currentLevel.ToString() + "Selected");
//    }

//    // *********************   Level Selection Scene **************************



//    // *********************   Bike Selection Scene **************************

//    public void BikeSelectionOpen()
//    {
//        {
//            showDebugLog("BikeSelection_Opened");
//        }
//        FbAnalytics.Instance.LogEvent("BikeSelection_Opened");
//    }

//    public void BikeSelectionClose()
//    {
//        {
//            showDebugLog("BikeSelection_Closed");
//        }
//        FbAnalytics.Instance.LogEvent("BikeSelection_Closed");
//    }

//    // *********************   Bike Selection Scene **************************



//    // *********************   Bike Selection Scene **************************

//    public void ShopSceenOpen()
//    {
//        {
//            showDebugLog("Shop_Opened");
//        }
//        FbAnalytics.Instance.LogEvent("Shop_Opened");
//    }

//    public void ShopSceenClose()
//    {
//        {
//            showDebugLog("Shop_Closed");
//        }
//        FbAnalytics.Instance.LogEvent("Shop_Closed");
//    }

//    // *********************   Bike Selection Scene **************************


//    // *********************   Gameplay Scene **************************

//    public void GamePlayLoading_Screen_Opened()
//    {
//        {
//            showDebugLog("GamePlay_Loading_Panel_Opened:Level_No_" + GameConstants.currentLevel.ToString());
//        }
//        FbAnalytics.Instance.LogEvent("GamePlay_Loading_Panel_Opened:Level_No_" + GameConstants.currentLevel.ToString());
//    }

//    public void GamePlayLoading_Screen_Closed()
//    {
//        {
//            showDebugLog("GamePlay_Loading_Panel_Closed:Level_No_" + GameConstants.currentLevel.ToString());
//        }
//        FbAnalytics.Instance.LogEvent("GamePlay_Loading_Panel_Closed:Level_No_" + GameConstants.currentLevel.ToString());
//    }

//    public void GamePlay_Scene_Opened()
//    {

//        {
//            showDebugLog("Level_No_" + GameConstants.currentLevel.ToString() + "_Started");
//        }
//        if (GameConstants.currentLevel == 14)
//        {
//            FbAnalytics.Instance.LogEvent("OpenWorld_" + "Started");
//            return;
//        }

//        if (PlayerPrefs.GetInt(MyPlayerPrefs.currentMode, 1) == 3)
//        {
//            FbAnalytics.Instance.LogEvent("Level_" + GameConstants.currentLevel.ToString() + "_Started_M3");
//        }
//        else
//        {
//            FbAnalytics.Instance.LogEvent("Level_" + GameConstants.currentLevel.ToString() + "_Started");

//        }



       
//    }

//    public void GamePlay_With_SelectedRobot()
//    {
       
//        {
//            showDebugLog("Robot_" + PlayerPrefs.GetInt(MyPlayerPrefs.RoboCategory, 0) + "_Selected");
//        }
//        FbAnalytics.Instance.LogEvent("Robot_" + PlayerPrefs.GetInt(MyPlayerPrefs.RoboCategory, 0) + "_Selected");
//    }

//    public void GamePlay_Scene_Closed()
//    {
        
//        {
//            showDebugLog("Level_No_" + GameConstants.currentLevel.ToString() + "_Ended");
//        }


//        if (GameConstants.currentLevel == 14)
//        {
//            FbAnalytics.Instance.LogEvent("OpenWorld_" + "Ended");
//            return;
//        }
//        FbAnalytics.Instance.LogEvent("Level_No_" + GameConstants.currentLevel.ToString() + "_Ended");
//    }

//    public void Gameplay_Pause_BtnClick()
//    {
//        {
//            showDebugLog("Level_No_" + GameConstants.currentLevel.ToString() + "_Paused");
//        }
//        if (GameConstants.currentLevel == 14)
//        {
//            FbAnalytics.Instance.LogEvent("OpenWorld_" + "Paused");
//            return;
//        }
//        FbAnalytics.Instance.LogEvent("Level_No_" + GameConstants.currentLevel.ToString() + "_Paused");
//    }



//    public void Pause_Panel_Opened()
//    {
//        {
//            showDebugLog("Level_No_" + GameConstants.currentLevel.ToString() + "_Paused");
//        }
//        FbAnalytics.Instance.LogEvent("Level_No_" + GameConstants.currentLevel.ToString() + "_Paused");
//    }

//    public void Pause_Panel_Closed()
//    {
//        {
//            showDebugLog("Level_No_" + GameConstants.currentLevel.ToString() + "_Exit_Pause");
//        }
//        FbAnalytics.Instance.LogEvent("Level_No_" + GameConstants.currentLevel.ToString() + "_Exit_Pause");
//    }

//    public void Pause_Panel_Exit()
//    {
        
//        {
//            showDebugLog("Level_No_" + GameConstants.currentLevel.ToString() + "_Exit");
//        }
//        FbAnalytics.Instance.LogEvent("Level_No_" + GameConstants.currentLevel.ToString() + "_Exit");
//    }



//    public void Tutorial_Skip_ButtonPressed()
//    {
        
//        {
//            showDebugLog("Tutorial_Skip_Button_Pressed");
//        }

//        FbAnalytics.Instance.LogEvent("Tutorial_Skip_Button_Pressed");
//    }


//    public void LevelComplete_Panel_Opened()
//    {

        
//        {
//            showDebugLog("Level_No_" + GameConstants.currentLevel.ToString() + "_Completed");
//        }




//        if (PlayerPrefs.GetInt(MyPlayerPrefs.currentMode, 1) == 3)
//        {
//            FbAnalytics.Instance.LogEvent("Level_No_" + GameConstants.currentLevel.ToString() + "_Completed_M3");
//        }
//        else
//        {
//            FbAnalytics.Instance.LogEvent("Level_No_" + GameConstants.currentLevel.ToString() + "_Completed");

//        }

//        FbAnalytics.Instance.LogEvent("Level_No_" + GameConstants.currentLevel.ToString() + "_Completed");
//    }
//    public void CheckPointReached()
//    {
//        {
//            showDebugLog("Level_No_" + GameConstants.currentLevel.ToString() + "_CheckPoint");
//        }
//        FbAnalytics.Instance.LogEvent("Level_No_" + GameConstants.currentLevel.ToString() + "_CheckPoint");
//    }
//    public void LevelComplete_Panel_Closed()
//    {
        
//        {
//            showDebugLog("GamePlay_Success_Panel_Closed:Level_No_" + GameConstants.currentLevel.ToString());
//        }


       
//        FbAnalytics.Instance.LogEvent("GamePlay_Success_Panel_Closed:Level_No_" + GameConstants.currentLevel.ToString());
//    }


//    public void LevelFailed_Panel_Opened()
//    {
        
//        {
//            showDebugLog("Level_No_" + GameConstants.currentLevel.ToString() + "_Failed");
//        }


//        if (PlayerPrefs.GetInt(MyPlayerPrefs.currentMode, 1) == 3)
//        {
//            FbAnalytics.Instance.LogEvent("Level_No_" + GameConstants.currentLevel.ToString() + "_Failed_M3");
//        }
//        else
//        {
//            FbAnalytics.Instance.LogEvent("Level_No_" + GameConstants.currentLevel.ToString() + "_Failed");

//        }



        
//    }

//    public void LevelFailed_Panel_Closed()
//    {
        
//        {
//            showDebugLog("GamePlay_Fail_Panel_Closed:Level_No_" + GameConstants.currentLevel.ToString());
//        }
//        FbAnalytics.Instance.LogEvent("GamePlay_Fail_Panel_Closed:Level_No_" + GameConstants.currentLevel.ToString()); 
//    }


//    // *********************   Gameplay Scene **************************



//    // *********************   Extra  **************************


//    public void Revive_Panel_Opened()
//    {
//        {
//            showDebugLog("GamePlay_Revive_Panel_Opened:Level_No_" + PlayerPrefs.GetInt(MyPlayerPrefs.LevelsUnlocked).ToString());
//        }
//        FbAnalytics.Instance.LogEvent("GamePlay_Revive_Panel_Opened:Level_No_" + PlayerPrefs.GetInt(MyPlayerPrefs.LevelsUnlocked).ToString());
//    }

//    public void Revive_Panel_Closed()
//    {
//        {
//            showDebugLog("GamePlay_Revive_Panel_Closed:Level_No_" + PlayerPrefs.GetInt(MyPlayerPrefs.LevelsUnlocked).ToString());
//        }
//        FbAnalytics.Instance.LogEvent("GamePlay_Revive_Panel_Closed:Level_No_" + PlayerPrefs.GetInt(MyPlayerPrefs.LevelsUnlocked).ToString());
//    }

//    public void Revive_Panel_Yes_BtnClick()
//    {
        
//        {
//            showDebugLog("GamePlay_Revive_Panel_Yes_BtnClick");
//        }
//        FbAnalytics.Instance.LogEvent("GamePlay_Revive_Panel_Yes_BtnClick");
//    }

//    public void Revive_Panel_Timeup()
//    {

        
//        {
//            showDebugLog("GamePlay_Revive_Panel_TimeUp");
//        }
//        FbAnalytics.Instance.LogEvent("GamePlay_Revive_Panel_TimeUp");
//    }



//    public void DoubleReward_Panel_Opened()
//    {
//        {
//            showDebugLog("GameOver_DoubleReward_Panel_Opened");
//        }
//        FbAnalytics.Instance.LogEvent("GameOver_DoubleReward_Panel_Opened");
//    }

//    public void DoubleReward_Panel_Closed()
//    {if (EnableDebug)
//        {
//            showDebugLog("GameOver_DoubleReward_Panel_Closed");
//        }
//        FbAnalytics.Instance.LogEvent("GameOver_DoubleReward_Panel_Closed");
//    }

//    public void DoubleReward_Panel_Yes_BtnClick()
//    {
//        {
//            showDebugLog("GameOver_DoubleReward_Yes_BtnClick");
//        }
//        FbAnalytics.Instance.LogEvent("GameOver_DoubleReward_Yes_BtnClick");
//    }

//    public void DoubleReward_Panel_Timeup()
//    {
//        {
//            showDebugLog("GameOver_DoubleReward_TimeUp");
//        }
//        FbAnalytics.Instance.LogEvent("GameOver_DoubleReward_TimeUp");
//    }



//    public void RateUs_Panel_Opened()
//    {
//        {
//            showDebugLog("GameOver_RateUs_Panel_Opened");
//        }
//        FbAnalytics.Instance.LogEvent("GameOver_RateUs_Panel_Opened");
//    }

//    public void RateUs_Panel_Closed()
//    {
//        {
//            showDebugLog("GameOver_RateUs_Panel_Closed");
//        }
//        FbAnalytics.Instance.LogEvent("GameOver_RateUs_Panel_Closed");
//    }

//    public void RateUs_Panel_Yes_BtnClick()
//    {
//        {
//            showDebugLog("GameOver_RateUs_Panel_Rate_BtnClick");
//        }
//        FbAnalytics.Instance.LogEvent("GameOver_RateUs_Panel_Rate_BtnClick");
//    }

//    public void RateUs_Panel_Cancel_BtnClick()
//    {
//        {
//            showDebugLog("GameOver_RateUs_Panel_Cancel_BtnClick");
//        }
//        FbAnalytics.Instance.LogEvent("GameOver_RateUs_Panel_Cancel_BtnClick");
//    }


//}