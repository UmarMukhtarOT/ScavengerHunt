
using UnityEngine;
using UnityEditor;
using TinyK.Touch;

public class CanvasComponent : Editor
{
  //  [MenuItem("Tools/TinyK Touch/Create Components/2D/Canvas/Single Touch", false, 0)]
    static void Createbutton()
    {


        if (Selection.activeGameObject == null || Selection.activeGameObject.GetComponent<RectTransform>() == null)
        {

            EditorUtility.DisplayDialog("Alert", "Select a UI component ", "Ok");

        }
        else
        {
            if (Selection.activeGameObject.GetComponent<LeanFingerDownCanvas>() == null)
            {

                Selection.activeGameObject.AddComponent<LeanFingerDownCanvas>();
                EditorUtility.DisplayDialog("Alert", "Now you can add events in the 'LeanFingerDownCanvas' component ", "Ok");

            }
            else
            {
                Debug.Log("Already available");
                EditorUtility.DisplayDialog("Alert", "Component already available", "Ok");

            }

        }

    }


    //[MenuItem("Tools/TinyK Touch/Create Components/2D/Canvas/Multiple Touch", false, 1)]
    static void CreateMultiUpdateButton()
    {

        if (Selection.activeGameObject == null || Selection.activeGameObject.GetComponent<RectTransform>() == null)
        {

            EditorUtility.DisplayDialog("Alert", "Select a UI component ", "Ok");

        }
        else
        {


            if (Selection.activeGameObject.GetComponent<LeanMultiUpdateCanvas>() == null)
            {

                Selection.activeGameObject.AddComponent<LeanMultiUpdateCanvas>();
                EditorUtility.DisplayDialog("Alert", "Now you can add events in the 'LeanMultiUpdateCanvas' component ", "Ok");

            }
            else
            {
                Debug.Log("Already available");
                EditorUtility.DisplayDialog("               Alert", "Component already available", "Ok");

            }



        }

    }




}
