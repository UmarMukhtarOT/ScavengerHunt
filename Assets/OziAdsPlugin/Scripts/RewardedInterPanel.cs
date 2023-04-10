using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardedInterPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Text msg;
    public Text RewardMsg;
    int i = 5;
    public Action Reward;
    public bool NothankAd = true;
    void OnEnable()
    {
        StartCoroutine(StartTimer());
    }
    public IEnumerator StartTimer()
    {
       
        while (i > 0)
        {
            msg.text = "Ads Starting in " + i + " sec";
            yield return new WaitForSecondsRealtime(1f);
            i--;
        }
        AdsManagerWrapper.Instance.ShowRewardedInterStitial(Reward);
        i = 5;
        gameObject.SetActive(false);
    }
    public void Nothanks()
    {
        StopCoroutine(StartTimer());
        i = 5;
        if (NothankAd)
        {
            AdsManagerWrapper.Instance.ShowInterstitial();
        }
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
