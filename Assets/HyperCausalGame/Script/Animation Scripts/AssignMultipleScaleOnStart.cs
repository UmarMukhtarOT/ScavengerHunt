using UnityEngine;
using DG.Tweening;



public class AssignMultipleScaleOnStart : MonoBehaviour
{

    public Vector3 ScaleStart = Vector3.zero;
    public Vector3 ScaleEnd = Vector3.one;
    public Transform[] ObjToScale = null;
    public float speed = 0;
    public bool OnEnableBool = false;
    public float DelayFactor = 0;
    public float initialDelay = 0;
    public Ease SpeedType = Ease.Linear;

    public void AssignScales()
    {
        float total = initialDelay;
        for (int i = 0; i < ObjToScale.Length; i++)
        {
            ObjToScale[i].transform.DOPause();
            ObjToScale[i].transform.localScale = ScaleStart;
            ObjToScale[i].transform.DOScale(ScaleEnd, speed).SetEase(SpeedType).SetDelay(total);
            total = total + DelayFactor;
        }

    }
    private void OnEnable()
    {
        if (ObjToScale != null)
        {
            if (OnEnableBool)
                AssignScales();
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < ObjToScale.Length; i++)
        {
            ObjToScale[i].transform.DOPause();

        }
    }
}