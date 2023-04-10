using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;
using UnityEngine.UI;

public class AppOpen : MonoBehaviour
{
    // Start is called before the first frame update
    #region OpenAd

    private AppOpenAd AdView;
    public List<String> AdUnitID;
    int AdCount = 0;
    bool Consent = true;
    bool AdLoading = false;
    private static DateTime Time1ForAds;
    public bool Active = false;
    public bool OnFocus;
    private void Start()
    {
        if (OnFocus)
        {
            AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
        }
    }
    private void OnAppStateChanged(AppState state)
    {
        // Display the app open ad when the app is foregrounded.
        UnityEngine.Debug.Log("App State is " + state);
        if (state == AppState.Foreground)
        {
            ShowAd();
        }
    }
    private bool IsAdAvailable
    {
        get
        {
            return AdView != null;
        }
    }


 
    public void LoadAd()
    {
      

  
        AdLoading = true;
        AdView = null;
        AdRequest request = new AdRequest.Builder().Build();
        AdsManagerWrapper.Instance.Log("Open Ad Loading ID Number" + AdCount);
        // Load an app open ad for portrait orientation
        AppOpenAd.LoadAd(AdUnitID[AdCount], ScreenOrientation.LandscapeLeft, request, ((appOpenAd, error) =>
        {
            if (error != null)
            {

                // Handle the error.
                if (AdCount < AdUnitID.Count - 1)
                {
                    AdCount++;
                    LoadAd();
                }
                else
                {
                    AdsManagerWrapper.Instance.Log("App Open Failed to Load");
                    AdLoading = false;
                    AdCount = 0;
                }

                return;

            }
            AdsManagerWrapper.Instance.Log("Open Ad Loaded ID Number" + AdCount);
            // App open ad is loaded.
            AdView = appOpenAd;
        }));
    }

    public void ShowAd()
    {
            if (IsAdAvailable)
            {
            AdsManagerWrapper.Instance.Log("Open Ad Showed");
                AdLoading = false;
            AdView.Show();
            }

        if (!AdLoading && OnFocus)
        {
            AdCount = 0;
            LoadAd();
        }
    }


    #endregion
}
