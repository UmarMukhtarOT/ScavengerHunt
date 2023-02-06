using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyK.Touch;
using UnityEditor;
using TinyK.Common;
using UnityEngine.EventSystems;

public class MainComponent : Editor
{
    //[MenuItem("Tools/TinyK Touch/Add Main Touch Manager",false,-10)]
   public static void CreateMainBehavior()
   {

        if (FindObjectOfType<LeanTouch>()==null)
        {

            Selection.activeObject = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/0_MainLeanTouch/MainTouchManager") as GameObject);
            if (FindObjectOfType<EventSystem>() == null)
            {
                var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            }
            // Debug.Log("Not Already available");
            //EditorUtility.DisplayDialog("Title", "Drag Camera object ", "Ok");

        }
        //else
        //{
        //  //  Debug.Log("Already available");
        //    EditorUtility.DisplayDialog("Alert", "Component Already available in the Scene", "Ok");

        //}

   }
    //[MenuItem("Tools/TinyK Touch/Create Components/2D", false, 0)]
    //[MenuItem("Tools/TinyK Touch/Create Components/3D", false, 1)]
    static void CreateBehavior()
    {

    }



       



}
