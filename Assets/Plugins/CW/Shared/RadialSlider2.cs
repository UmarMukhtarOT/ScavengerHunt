//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class RadialSlider : MonoBehaviour
//{
//    [SerializeField]
//    Transform handle;
//    [SerializeField]
//    Image fill;
//    [SerializeField]
//    Text valtxt;
//    Vector3 mousePos;

//    public void OnHandleDrag()
//    {
//        mousePos = Input.mousePosition;
//        Vector2 dir = mousePos - handle.position;
//        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//        angle = (angle <= 0) ? (360 + angle) : angle;
//        if (angle <= 225 || angle >= 315)
//        {
//            Quaternion r = Quaternion.AngleAxis(angle + 135f, Vector3.forward);
//            handle.rotation = r;
//            angle = ((angle >= 315) ? (angle - 360) : angle) + 45;
//            fill.fillAmount = 0.75f - (angle / 360f);
//            valtxt.text = Mathf.Round((fill.fillAmount * 100) / 0.75f).ToString();
//        }
//    }
//}