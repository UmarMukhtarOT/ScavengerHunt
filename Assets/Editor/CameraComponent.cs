
using TinyK.Common;
using TinyK.Touch;
using UnityEditor;
using UnityEngine;

public class CameraComponent : Editor
{

   // [MenuItem("Tools/TinyK Touch/Create Components/Camera/Auto Rotate", false, -2)]

    //[MenuItem("Tools/TinyK Touch/Create Components/Camera/Dolly", false, -3)]
   // [MenuItem("Tools/TinyK Touch/Create Components/Camera/Swipe", false, -4)]
   // [MenuItem("Tools/TinyK Touch/Create Components/Camera/One Finger Zoom", false, 5)]


    [MenuItem("Tools/TinyK Touch/Create Components/Camera/Orbit Camera", false, -2)]
    static void CreateOrbitCamera()
    {

        if (FindObjectOfType<LeanPitchYaw>() == null)
        {

            Selection.activeObject = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/1_Camera/OrbitCameraPivot") as GameObject);
           // Debug.Log("Not Already available");
            MainComponent.CreateMainBehavior();

        }
        else
        {
           // Debug.Log("Already available");
            EditorUtility.DisplayDialog("Alert", "Component already available in the Scene", "Ok");

        }

    }

    [MenuItem("Tools/TinyK Touch/Create Components/Camera/Zoom", false, 1)]
    static void CreateZoomCamera()
    {
        

        if (Selection.activeGameObject == null||Selection.activeGameObject.GetComponent<Camera>() == null)
        {

            EditorUtility.DisplayDialog("Alert", "Select Camera to add this component", "Ok");

        }
        else
        {
           

            if (FindObjectOfType<LeanPinchCamera>() == null)
            {

                Selection.activeGameObject.AddComponent<LeanPinchCamera>();
                //Debug.Log("Not Already available");


            }
            else
            {
             //   Debug.Log("Already available");
                EditorUtility.DisplayDialog("Alert", "Component already available in the Scene", "Ok");

            }

            MainComponent.CreateMainBehavior();

        }


        




    }

    [MenuItem("Tools/TinyK Touch/Create Components/Camera/Wheel", false, 2)]
    static void CreateWheelCamera() 
    {

        if (Selection.activeGameObject == null || Selection.activeGameObject.GetComponent<Camera>() == null)
        {

            EditorUtility.DisplayDialog("Alert", "Select Camera to add this component", "Ok");

        }
        else
        {


            if (FindObjectOfType<LeanMouseWheel>() == null)
            {

                LeanMouseWheel LMW = Selection.activeGameObject.AddComponent<LeanMouseWheel>();
                LMW.Multiplier = -0.1f;
                LMW.Coordinate= LeanMouseWheel.CoordinateType.OneBased;
               

                if (LMW.GetComponent<LeanPinchCamera>()==null)
                {
                    LMW.gameObject.AddComponent<LeanPinchCamera>();

                }

                LMW.OnDelta.AddListener(LMW.GetComponent<LeanPinchCamera>().MultiplyZoom);
             //   Debug.Log("Not Already available");
                MainComponent.CreateMainBehavior();

            }
            else
            {
              //  Debug.Log("Already available");
                EditorUtility.DisplayDialog("Alert", "Component already available in the Scene", "Ok");

            }

            

        }

    }


    [MenuItem("Tools/TinyK Touch/Create Components/Camera/Drag", false, 3)]
    static void CreatePanCamera()
    {
       

        if (FindObjectOfType<LeanDragCamera>() == null)
        {

            Selection.activeObject = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/1_Camera/DragCamera") as GameObject);
            Debug.Log("Not Already available");
            MainComponent.CreateMainBehavior();

        }
        else
        {
            Debug.Log("Already available");
            EditorUtility.DisplayDialog("Alert", "Component already available in the Scene", "Ok");

        }

      // MainComponent.CreateMainBehavior();
    }








}
