using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class BannerAd : MonoBehaviour
{
    private BannerView AdView;
    public List<String> AdUnitID;
    int AdCount = 0;
    bool Consent = true;
    bool AdLoading = false;

    AdSize BannerSize = AdSize.SmartBanner;
    AdPosition BannerPos = AdPosition.Top;
    bool AdHideCalled = false;
        
 

    public void LoadAd()
    {
        Consent = PlayerPrefs.GetInt("userConsent") == 0 ? false : true;





        AdLoading = true;
        if (this.AdView != null)
        {
            this.AdView.OnAdFailedToLoad -= AdFailedtoLoad;
            this.AdView.OnAdLoaded -= AdLoaded;
            this.AdView.Destroy();

        }
  
        AdsManagerWrapper.Instance.Log(gameObject.name+" Loading with ID Number" + AdCount);
        this.AdView = new BannerView(AdUnitID[AdCount], BannerSize, BannerPos);
        this.AdView.OnAdFailedToLoad += AdFailedtoLoad;
        this.AdView.OnAdLoaded += AdLoaded;
        AdRequest request = new AdRequest.Builder().Build();
        this.AdView.LoadAd(request);


    }

    private void AdLoaded(object sender, EventArgs e)
    {
       // this.AdView.OnAdFailedToLoad -= AdFailedtoLoad;
        AdsManagerWrapper.Instance.Log(gameObject.name + " Loaded with ID Number" + AdCount);
        if (AdHideCalled)
        {
            AdLoading = false;
            AdView.Destroy();
        }
    }

    private void AdFailedtoLoad(object sender, EventArgs e)
    {
        if (AdCount < AdUnitID.Count - 1)
        {
            AdCount++;
            LoadAd();
            return;
        }
        this.AdView.OnAdFailedToLoad -= AdFailedtoLoad;
        AdsManagerWrapper.Instance.Log(gameObject.name + " Failed to Load");
        AdLoading = false;
        AdCount = 0;
    }


    public void ShowAd(AdPosition adPosition, AdSize _adSize)
    {

        if (AdLoading)
        {
            return;
        }


        BannerSize = _adSize;
        BannerPos = adPosition;

        AdHideCalled = false;

        AdCount = 0;
        if (PlayerPrefs.GetInt("RemoveAds", 0) == 1)
            return;

        LoadAd();

    }
    public void HideAd()
    {
        AdHideCalled = true;
        AdLoading = false;
        if (this.AdView != null)
        {
            this.AdView.Destroy();
        }
    }


}
