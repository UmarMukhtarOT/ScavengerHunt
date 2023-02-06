using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TinyK.Touch;
using TinyK.Common;
using UnityEditor.UI;

public class AllComponent3D : Editor
{
    public static void SingleSelectionComponent()
    {
        if (FindObjectOfType<LeanFingerDown>() == null)
        {
            PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/0_MainLeanTouch/Press To Select") as GameObject);

        }

    }



    [MenuItem("Tools/TinyK Touch/Create Components/3D/Scale", false, 0)]
    static void CreateScale()
    {
        
        Selection.activeObject = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/02_3D/ScaleAbleSphere") as GameObject);
        MainComponent.CreateMainBehavior();
        SingleSelectionComponent();

    }

    [MenuItem("Tools/TinyK Touch/Create Components/3D/Drag Along Path", false, 1)]
    static void CreateDragAlongPath()
    {

        Selection.activeObject = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/02_3D/DragAlongPath") as GameObject);
        Debug.Log("Not Already available");
        MainComponent.CreateMainBehavior();

    }




    [MenuItem("Tools/TinyK Touch/Create Components/3D/Rotateable", false, 1)]
    static void CreateRotateable()
    {
        Selection.activeObject = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/02_3D/Rotateable") as GameObject);
        Debug.Log("Not Already available");
        MainComponent.CreateMainBehavior();
        SingleSelectionComponent();
        
    }


    [MenuItem("Tools/TinyK Touch/Create Components/3D/SelectionBox", false, 1)]
    static void CreateSelectionBox()
    {
        Selection.activeObject = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/02_3D/Selection Box") as GameObject);
        // Debug.Log("Selection.activeObject "+ Selection.activeObject.name);
        if (FindObjectOfType<Canvas>()==null)
        {
            GameObject Can = new GameObject();
            Canvas canvas = Can.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler cs = Can.AddComponent<CanvasScaler>();
           // cs.scaleFactor = 10.0f;
           // cs.dynamicPixelsPerUnit = 10f;
            GraphicRaycaster gr = Can.AddComponent<GraphicRaycaster>();
          //  Can.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3.0f);
          //  Can.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 3.0f);

        }
        Selection.activeGameObject.GetComponent<LeanSelectionBox>().Root = FindObjectOfType<Canvas>().gameObject.GetComponent<RectTransform>();


        Debug.Log("Not Already available");
        MainComponent.CreateMainBehavior();
        


        SingleSelectionComponent();
        
    }



    
    [MenuItem("Tools/TinyK Touch/Create Components/3D/DragRotateScale", false, 1)]
    static void CreateDragRotateScale()
    {
        Selection.activeObject = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/02_3D/DragRotateScale") as GameObject);
        PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/0_MainLeanTouch/Tap To Select") as GameObject);
       


        Debug.Log("Not Already available");
        MainComponent.CreateMainBehavior();
        


        
        
    }










}
