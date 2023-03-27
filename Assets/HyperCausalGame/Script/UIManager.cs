using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    [Header("CurrencyUI")]
    public RectTransform CoinTextParent = null;
    public Text CoinText = null;

    [Header("MainMenuUI")]
    public GameObject MainMenuDialogue = null;

    [Header("GamePlayUI")]
    public GameObject GamePlayDialogue = null;
    public RectTransform GamePlayLevelNumberParent = null;
    public Text GamePlayLevelNumberText = null;

    [Header("LevelCompleteUI")]
    public GameObject LevelCompleteDialogue = null;
    [Header("GameOverUI")]
    public GameObject GameOverDialogue = null;


    
    public GameObject crossImage = null;

    private void Awake()
    {
        instance = this;
        AnimateGamePlayLevelNumber(1000f, 0, Ease.Linear);
        AnimateCoinTextParent(1000f, 0, Ease.Linear);
    }
    private void Start()
    {
        AnimateGamePlayLevelNumber(-250f, 1f, Ease.Linear);
        AnimateCoinTextParent(-250f, 1f, Ease.Linear);
        LevelManager.instance.levelCreateFuncEvent += SetGamePlayLevelNumberText;
        GameManager.instance.gameStarFuncEvent += SetGameStartUI;
        GameManager.instance.gameResetFuncEvent += SetGameResetUI;
        GameManager.instance.gameOverManager.gameOverFuncEvent += SetGameOverUI;
        GameManager.instance.levelCompleteManager.levelCompleteFuncEvent += SetLevelCompleteUI;
    }
    public void SetGamePlayLevelNumberText(int Level)
    {
        GamePlayLevelNumberText.text = "Level " + Level;
    }
    public void SetGameStartUI()
    {
        MainMenuDialogue.gameObject.SetActive(false);
        GamePlayDialogue.gameObject.SetActive(true);
    }

    public void SetGameResetUI()
    {
        MainMenuDialogue.gameObject.SetActive(true);
        GamePlayDialogue.gameObject.SetActive(false);
        GameOverDialogue.gameObject.SetActive(false);
        LevelCompleteDialogue.gameObject.SetActive(false);
        AnimateGamePlayLevelNumber(-250f, 1f, Ease.Linear);

    }
    public void SetLevelCompleteUI()
    {
        LevelCompleteDialogue.gameObject.SetActive(true);
        MainMenuDialogue.gameObject.SetActive(false);
        GamePlayDialogue.gameObject.SetActive(false);
        AnimateGamePlayLevelNumber(1000f, 1f, Ease.Linear);


    }
    public void SetGameOverUI()
    {
        GameOverDialogue.gameObject.SetActive(true);
        MainMenuDialogue.gameObject.SetActive(false);
        GamePlayDialogue.gameObject.SetActive(false);
        AnimateGamePlayLevelNumber(1000f, 1f, Ease.Linear);
        AnimateCoinTextParent(1000f, 1f, Ease.Linear);
    }
    public void AnimateGamePlayLevelNumber(float Ypos, float speed, Ease ease)
    {
        GamePlayLevelNumberParent.DOPause();
        GamePlayLevelNumberParent.DOAnchorPosY(Ypos, speed).SetEase(ease);
    }
    public void AnimateCoinTextParent(float Ypos, float speed, Ease ease)
    {
        CoinTextParent.DOPause();
        CoinTextParent.DOAnchorPosY(Ypos, speed).SetEase(ease);
    }
    private void OnDisable()
    {
        CoinTextParent.DOPause();
        GamePlayLevelNumberParent.DOPause();
    }
}
