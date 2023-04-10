using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class RewardedInterstitial : MonoBehaviour
{

    private RewardedInterstitialAd AdView;
    public List<String> AdUnitID;
    int AdCount = 0;
    bool Consent = true;
    bool AdLoading = false;
    bool isRewarded = false;
    public Action RewardHandle;
    public bool Active = false;

    public bool IsAdAvailable()
    {
        if (this.AdView != null)
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



    public void LoadAd()
    {



        AdLoading = true;
        AdView = null;
        AdRequest request = new AdRequest.Builder().Build();
        AdsManagerWrapper.Instance.Log("RewardInterStitial Loading ID Number" + AdCount);

        RewardedInterstitialAd.LoadAd(AdUnitID[AdCount], request, adLoadCallback);
    }


    private void adLoadCallback(RewardedInterstitialAd ad, AdFailedToLoadEventArgs error)
    {
        if (error != null)
        {
            if (AdCount < AdUnitID.Count - 1)
            {
                AdCount++;
                LoadAd();
            }
            else
            {
                AdsManagerWrapper.Instance.Log("RewardInterStitial Failed to Load");
                AdLoading = false;
                AdCount = 0;
            }

            return;
        }
        AdView = ad;
    }


    public void ShowAd(Action _Reward)
    {
        if (IsAdAvailable())
        {
            AdsManagerWrapper.Instance.AdShown = true;
            RewardHandle = _Reward;
            AdsManagerWrapper.Instance.Log("RewardInterStitial Showed");
            AdLoading = false;
            AdView.Show(RewardedCompleted);
        }

        if (!AdLoading)
        {
            AdCount = 0;
            LoadAd();
        }
    }

    private void RewardedCompleted(Reward obj)
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
