using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    public GameObject consentPanel;
    public GameObject loading;
    public Image fillBar;
    // Start is called before the first frame update
    void Start()
    {


        if (!PlayerPrefs.HasKey("PlayingFirstTime"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("PlayingFirstTime",1);
        }


        if(!PlayerPrefs.HasKey(PlayerPrefKeys.ConsentGrantValue))
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.ConsentGrantValue, 0);
        }
        if(!PlayerPrefs.HasKey(PlayerPrefKeys.FirstAppOpen))
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.FirstAppOpen, 0);
        }
        CheckConsent();



    }

    void CheckConsent()
    {
        if(!PlayerPrefs.HasKey(PlayerPrefKeys.Consent))
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.Consent, 0);
            consentPanel.SetActive(true);
            consentPanel.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InOutQuad);

            loading.SetActive(false);
        }
        else
        {
            consentPanel.gameObject.SetActive(false);
            loading.SetActive(true);
            if(PlayerPrefs.GetInt(PlayerPrefKeys.ConsentGrantValue)==0)
            {
                AdsManagerWrapper.Instance.initialize(false);
            }
            else
            {
                AdsManagerWrapper.Instance.initialize(true);

            }
            StartCoroutine(LoadLevel());
        }
    }
    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(8f);



        if(PlayerPrefs.GetInt(PlayerPrefKeys.FirstAppOpen)!=0)
        {
            AdsManagerWrapper.Instance.ShowAppOpen();

        }
       // DummyBannerBar.Instance.showBannerWithBlackBar();

        if (PlayerPrefs.GetInt(PlayerPrefKeys.FirstAppOpen)==0)
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.FirstAppOpen, 1);
        }


        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync("GamePlay");
    }

    public void Accept()
    {
        consentPanel.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InOutQuad).OnComplete(()=>
        {
            consentPanel.gameObject.SetActive(false);
    
        });
        loading.SetActive(true);
        AdsManagerWrapper.Instance.initialize(true);
        PlayerPrefs.SetInt(PlayerPrefKeys.ConsentGrantValue, 1);
        StartCoroutine(LoadLevel());

    }

    public void NoThanks()
    {
        consentPanel.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            consentPanel.gameObject.SetActive(false);

        });
        loading.SetActive(true);
        AdsManagerWrapper.Instance.initialize(false);
        PlayerPrefs.SetInt(PlayerPrefKeys.ConsentGrantValue, 0);
        StartCoroutine(LoadLevel());
    }
    public void PrivacyPolicy()
    {
        Application.OpenURL("https://tinykraken.games/privacy.html");

    }
    public void TermsAndCondition()
    {
        Application.OpenURL("");

    }
}
