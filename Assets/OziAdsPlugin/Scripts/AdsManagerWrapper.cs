using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;




//using GoogleMobileAds.Api.Mediation.AdColony;
//using GoogleMobileAds.Api.Mediation.IronSource;
//using GoogleMobileAds.Api.Mediation.AppLovin;

public class AdsManagerWrapper : MonoBehaviour
{

    public static AdsManagerWrapper Instance;




   public Interstitial _inter;
  public Rewarded _rewarded;
    public BannerAd _banner;
    public BannerAd _rectBanner;
    public AppOpen _appOpen;
    public  Interstitial _cp;
    public RewardedInterstitial _rewardInter;
    public RewardedInterPanel RewardPanel;

    private static DateTime Time1ForAds;

    [HideInInspector]
    public bool InitSucceded;
    [HideInInspector]
    public bool AdShown = false;
    public List<String> testDeviceIds;
    public AdSize adaptiveSize = AdSize.GetLandscapeAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
    #region init
    public Text _log;
    bool TopBannerCalled=false;
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }



    public void initialize(bool agree)
    {


        Log("Init Called");
        MobileAds.Initialize((initStatus) =>
        {
            OnInitSuccessHandler();
        });
        if (testDeviceIds.Count > 0)
        {
            RequestConfiguration configuration =
                new RequestConfiguration.Builder().SetTestDeviceIds(testDeviceIds).build();

            MobileAds.SetRequestConfiguration(configuration);
        }


    }
    void OnInitSuccessHandler()
    {
        StartCoroutine(OnInitSuccess());
    }
    public IEnumerator OnInitSuccess()
    {



        Log("Init Success");



        // AppLovin.SetHasUserConsent(true);
        if (_inter.Active)
        {
            _inter.LoadAd();
            yield return new WaitForSeconds(0.2f);
        }
        if (_rewarded.Active)
        {
            _rewarded.LoadAd();
            yield return new WaitForSeconds(0.2f);
        }
        if (_rewardInter.Active)
        {
            _rewardInter.LoadAd();
            yield return new WaitForSeconds(0.2f);
        }
        if (_appOpen.Active)
        {
            _appOpen.LoadAd();
        }
        InitSucceded = true;

    }

    #endregion


    public void ShowInterstitial()
    {
        if (!InitSucceded || PlayerPrefs.GetInt("RemoveAds") == 1)
            return;

        _inter.ShowAd();
    }

    public void ShowCpinterstitial()
    {
        if (!InitSucceded || PlayerPrefs.GetInt("RemoveAds") == 1)
            return;

        _cp.ShowAd();
    }

    public bool IsInterstitialAvailable()
    {
        if (!InitSucceded || PlayerPrefs.GetInt("RemoveAds") == 1)
            return false;

        return _inter.isAdAvailable();
    }

    public void ShowRewardedVideo(Action _Reward)
    {
        if (!InitSucceded || PlayerPrefs.GetInt("RemoveAds") == 1)
            return;

        _rewarded.ShowAd(_Reward);
    }
    public void ShowRewardedInterStitial(Action _Reward)
    {
        if (!InitSucceded || PlayerPrefs.GetInt("RemoveAds") == 1)
            return;

        _rewardInter.ShowAd(_Reward);
    }
    public void ShowRewardedInterStitialHandler(Action Reward,string msg,bool NoThanksAd=true)
    {

        if (_rewardInter.IsAdAvailable())
        {
            RewardPanel.Reward = Reward;
            RewardPanel.RewardMsg.text = msg;
            RewardPanel.NothankAd = NoThanksAd;
            RewardPanel.gameObject.SetActive(true);
        }
        else
        {
            if (NoThanksAd)
            {
                _inter.ShowAd();
            }
        }
    }

    public bool IsRewardedVideoAvailable()
    {
        if (!InitSucceded || PlayerPrefs.GetInt("RemoveAds") == 1)
            return false;

        return _rewarded.isAdAvailable();
    }
    public void ShowBanner(AdPosition adPosition, AdSize _adSize)
    {

//
  //      if (!InitSucceded || PlayerPrefs.GetInt("RemoveAds") == 1)
    //        return;


        if (TopBannerCalled) return;

        TopBannerCalled = true;

        _banner.ShowAd(adPosition, _adSize);
    }
    public void HideBanner()
    {
        if (!InitSucceded)
            return;

        _banner.HideAd();

    }

    public void ShowRectBanner(AdPosition adPosition, AdSize _adsize)
    {
        if (!InitSucceded || PlayerPrefs.GetInt("RemoveAds") == 1)
            return;

        _rectBanner.ShowAd(adPosition, _adsize);

    }
    public void HideRectBanner()
    {
        if (!InitSucceded)
            return;

        _rectBanner.HideAd();

    }


    public void ShowAppOpen()
    {
        if (!InitSucceded || PlayerPrefs.GetInt("RemoveAds") == 1)
            return;

        _appOpen.ShowAd();
        AdShown = true;

    }
    public void Log(string text)
    {
        Debug.Log("Haseeb" + text);
        if (_log)
        {
            _log.text = text;
        }
    }
  
}

























