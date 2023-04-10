using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class Interstitial : MonoBehaviour
{

    private InterstitialAd AdView;
    public List<String> AdUnitID;
    int AdCount = 0;
    bool Consent = true;
    bool AdLoading = false;
    public bool Active = false;




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

    public void ShowAd()
    {

 


        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            AdsManagerWrapper.Instance.Log("Inter Show Called");

            if (isAdAvailable())
            {
     
                AdsManagerWrapper.Instance.AdShown = true;
                this.AdView.Show();
                // FireBaseAnalyticsManager.Instance.ListenerToAnalyticsEvent("Interstitial_Ad");
            }

        }

    }

    public void LoadAd()
    {

        Consent = PlayerPrefs.GetInt("userConsent") == 0 ? false : true;

     
        AdLoading = true;

        if (this.AdView != null)
        {
            this.AdView.OnAdFailedToLoad -= AdFailedtoLoad;
            this.AdView.Destroy();

        }

        AdsManagerWrapper.Instance.Log("Inter Loading with ID Number" + AdCount);

        this.AdView = new InterstitialAd(AdUnitID[AdCount]);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.AdView.LoadAd(request);


        this.AdView.OnAdClosed += AdClosed;
        this.AdView.OnAdFailedToLoad += AdFailedtoLoad;
        this.AdView.OnAdLoaded += AdLoaded;
    }

    private void AdLoaded(object sender, EventArgs e)
    {
        AdsManagerWrapper.Instance.Log("Inter Loaded with ID Number" + AdCount);
    }

    private void AdFailedtoLoad(object sender, AdFailedToLoadEventArgs e)
    {
        if (AdCount < AdUnitID.Count - 1)
        {
            AdCount++;
            LoadAd();
            return;
        }
        AdsManagerWrapper.Instance.Log("Inter Failed to Load");
        AdCount = 0;
        AdLoading = false;
    }


    public void AdClosed(object sender, EventArgs args)
    {
        AdLoading = false;
        AdView.Destroy();
        AdCount = 0;
        LoadAd();
    }



}
