using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class Rewarded : MonoBehaviour
{

    private RewardedAd AdView;
    public List<String> AdUnitID;
    int AdCount = 0;
    bool Consent = true;
    bool AdLoading = false;
    bool isRewarded = false;
    public Action RewardHandle;
    public bool Active = false;
    public void Start()
    {
        Invoke("RemoveIds", 1f);
    }


    public void LoadAd()
    {
        Consent = PlayerPrefs.GetInt("userConsent") == 0 ? false : true;


        AdLoading = true;


        if (this.AdView!= null)
        {
            this.AdView.OnAdFailedToLoad -= AdFailedtoLoad;

        }

        AdsManagerWrapper.Instance.Log("Rewarded Loading with ID Number" + AdCount);

        this.AdView = new RewardedAd(AdUnitID[AdCount]);
        AdRequest request = new AdRequest.Builder().Build();
        this.AdView.LoadAd(request);


        this.AdView.OnAdClosed += AdClosed;
        this.AdView.OnAdFailedToLoad += AdFailedtoLoad;
        this.AdView.OnAdLoaded += AdLoaded;
    }

    private void AdLoaded(object sender, EventArgs e)
    {
        AdsManagerWrapper.Instance.Log("Rewarded Loaded with ID Number" + AdCount);
    }

    private void AdFailedtoLoad(object sender, AdFailedToLoadEventArgs e)
    {
        if (AdCount < AdUnitID.Count - 1)
        {
            AdCount++;
            LoadAd();
            return;
        }
        AdsManagerWrapper.Instance.Log("Rewarded Failed to Load");
        AdCount = 0;
        AdLoading = false;
    }




    private void AdClosed(object sender, EventArgs e)
    {
        AdCount = 0;
        AdLoading = false;
        LoadAd();
        
    }

    public bool isAdAvailable()
    {



        if (this.AdView.IsLoaded())
        {
            return true;
        }
        else
        {
            if (!AdLoading)
            {
                LoadAd();
            }
            return false;
        }
    }



    public void ShowAd(Action _Reward)
    {
        isRewarded = false;

        AdsManagerWrapper.Instance.Log("Rewarded Show Called");
        if (isAdAvailable())
        {
         
            AdsManagerWrapper.Instance.AdShown = true;
            RewardHandle = _Reward;
            AdView.OnUserEarnedReward += RewardedCompleted;
            this.AdView.Show();
        }
    }

    private void RewardedCompleted(object sender, Reward args)
    {


        isRewarded = true;


    }
    private void Update()
    {

        if (isRewarded)
        {
            isRewarded = false;
            if (RewardHandle != null)
            {
                RewardHandle.Invoke();
            }

        }


    }




}
