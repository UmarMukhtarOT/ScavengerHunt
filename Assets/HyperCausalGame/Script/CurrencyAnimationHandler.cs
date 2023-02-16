using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CurrencyAnimationHandler : MonoBehaviour
{
    public GameObject CoinPrefab;
    public int CoinTotalCreation = 4;
    public Transform CoinParent = null;
    public Transform CoinTarget = null;
    private List<GameObject> Coins = new List<GameObject>();
    public static CurrencyAnimationHandler instance = null;
    private void Awake()
    {
        instance = this;
        CreateGems();
    }

    void CreateGems()
    {
        GameObject obj = null;
        Coins.Clear();
        for (int i = 0; i < CoinTotalCreation; i++)
        {
            obj = Instantiate(CoinPrefab, Vector3.zero, Quaternion.identity);
            Coins.Add(obj);
            obj.transform.SetParent(CoinParent);
            obj.gameObject.SetActive(false);

        }
    }


    public void RunGemAnimation(Vector3 InitialPosition, Vector3 TargetPos, Vector3 InitialScale, Vector3 FinalScale)
    {
        float speedtime = 1f;
        float total = 0, ad = 0.04f;
        for (int i = 0; i < Coins.Count; i++)
        {
            Coins[i].gameObject.SetActive(true);
            Coins[i].transform.SetParent(CoinParent);

            Coins[i].transform.localPosition = InitialPosition + new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), 0f);

            Coins[i].transform.DOPause();

            Coins[i].transform.SetParent(CoinTarget);

            Coins[i].transform.localScale = InitialScale;
            Coins[i].transform.DOLocalMove(TargetPos, speedtime).SetDelay(total).SetEase(Ease.Linear);
            Coins[i].transform.localEulerAngles = Vector3.zero;
            Coins[i].transform.DOScale(FinalScale, speedtime).SetDelay(total).SetEase(Ease.Linear);
            StartCoroutine(OnOffObj(Coins[i].gameObject, false, speedtime + total));
            total = total + ad;

        }
        StartCoroutine(DelayUdateUI(total + speedtime));


    }


    IEnumerator OnOffObj(GameObject obj, bool value, float time)
    {
        yield return new WaitForSeconds(time);
        obj.gameObject.SetActive(value);
    }


    IEnumerator DelayUdateUI(float time)
    {
        yield return new WaitForSeconds(time);
        if (GameCurrencyHandler.instance != null)
            GameCurrencyHandler.instance.Update_();
    }



}
