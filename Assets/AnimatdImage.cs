using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnimatdImage : MonoBehaviour
{
    public Image myImg;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void AnimateFlyingImage(Vector3 startPos, int iconNum)
    {
        transform.position = startPos;
        myImg.sprite = UIManagerScav.instance.SV_IconList[iconNum].childImg.sprite;
        transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1);

        transform.DOMove(UIManagerScav.instance.SV_IconList[iconNum].childImg.transform.position, 1f).SetEase(DG.Tweening.Ease.InBack).OnComplete(() =>
        {

           //SoundsManager.instance.PlayScavSound(SoundsManager.instance.AssetCollect_Scav);

            UIManagerScav.instance.SV_IconList[iconNum].childImg.transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 0.15f).OnComplete(() =>
            {
                UIManagerScav.instance.SV_IconList[iconNum].childImg.transform.localScale = Vector3.one;
            });


            transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            gameObject.SetActive(false);

            Debug.Log(" elasticity " + UIManagerScav.instance.scrollRect.elasticity);
            if (iconNum == 0 || iconNum == UIManagerScav.instance.SV_IconList.Count)
            {
                Debug.Log("");
                Invoke("changeElasticity", 2);

            }
            else
            {
                UIManagerScav.instance.scrollRect.elasticity = 0.2f;


            }

            UIManagerScav.instance.FlyingGem.transform.position = UIManagerScav.instance.SV_IconList[iconNum].transform.position;
            UIManagerScav.instance.FlyingGem.SetActive(true);
            UIManagerScav.instance.FlyingGem.transform.DOMove(UIManagerScav.instance.GemCounter.transform.position, 1).
            SetEase(DG.Tweening.Ease.InBack).OnComplete(() =>
            {

                UIManagerScav.instance.FlyingGem.SetActive(false);
                GameCurrencyHandler.instance.AddToCoins(1);

            });

        });


    }

    void changeElasticity()
    {

        UIManagerScav.instance.scrollRect.elasticity = 0.2f;


    }





}
