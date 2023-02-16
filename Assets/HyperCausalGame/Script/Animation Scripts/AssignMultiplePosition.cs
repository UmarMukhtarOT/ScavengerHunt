using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AssignMultiplePosition : MonoBehaviour
{

    public RectTransform[] ObjToPosition = null;
    public Vector3[] PositionsStart = null;
    public Vector3[] PositionsEnd = null;
    public float speed = 0;
    public bool OnEnableBool = false;
    public float DelayFactor = 0;
    public float initialDelay = 0;
    public Ease SpeedType = Ease.Linear;

    public void AssignPosition()
    {
        RectTransform RT = null;
        float total = initialDelay;
        for (int i = 0; i < ObjToPosition.Length; i++)
        {
            ObjToPosition[i].transform.DOPause();
            RT = ObjToPosition[i].GetComponent<RectTransform>();
            if (RT != null)

                RT.anchoredPosition = PositionsStart[i];
            //  ObjToPosition[i].transform.position = PositionsStart[i];
            RT.DOAnchorPos(PositionsEnd[i], speed).SetEase(SpeedType).SetDelay(total);
            //   ObjToPosition[i].transform.DOLocalMove(PositionsEnd[i], speed).SetEase(SpeedType).SetDelay(total);
            total = total + DelayFactor;
        }
    }


    private void OnEnable()
    {
        if (ObjToPosition != null)
        {
            if (OnEnableBool)
                AssignPosition();
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < ObjToPosition.Length; i++)
        { ObjToPosition[i].transform.DOPause(); }
    }
    public void CloseEverything()
    {
        RectTransform RT = null;
        float total = initialDelay;
        for (int i = 0; i < ObjToPosition.Length; i++)
        {
            ObjToPosition[i].transform.DOPause();
            RT = ObjToPosition[i].GetComponent<RectTransform>();
            if (RT != null)


                RT.DOAnchorPos(PositionsStart[i], speed).SetEase(SpeedType).SetDelay(total);

            total = total + DelayFactor;
        }
    }
}