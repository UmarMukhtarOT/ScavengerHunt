using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class contentViewHandler : MonoBehaviour
{
    public RectTransform contentRt;
    ScrollRect instance;

    private void Start()
    {
        
       instance = GetComponent<ScrollRect>();
    }
   
        public  void  GetSnapToPositionToBringChildIntoView(RectTransform child)
        {
            Debug.Log("changing SV position");
            Canvas.ForceUpdateCanvases();
            Vector2 viewportLocalPosition = instance.viewport.localPosition;
            Vector2 childLocalPosition   = child.localPosition;
            Vector2 result = new Vector2(
                0 - (viewportLocalPosition.x + childLocalPosition.x),
                0 - (viewportLocalPosition.y + childLocalPosition.y)
            );

        contentRt.DOLocalMove(new Vector2(result.x, contentRt.localPosition.y),0.2f).SetEase(Ease.Linear);
       // contentRt.localPosition = new Vector2(contentRt.localPosition.x,result.y);
       //// contentRt.localPosition = new Vector2(contentRt.localPosition.x,result.y);
        }
}
