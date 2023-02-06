using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraWizard : ScriptableWizard
{
    public float range = 500;
    public Color color = Color.red;



    //[MenuItem("GameObject/Create Light Wizard")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<CameraWizard>("Create Camera", "Create");
        //If you don't want to use the secondary button simply leave it out:
        //ScriptableWizard.DisplayWizard<WizardCreateLight>("Create Light", "Create");
    }

    void OnWizardCreate()
    {
        GameObject go = new GameObject("New Light");
        Camera Cam = go.AddComponent<Camera>();
       
    }

    void OnWizardUpdate()
    {
        helpString = "Please Drag Your Camera into it";
    }

    // When the user presses the "Apply" button OnWizardOtherButton is called.
    void OnWizardOtherButton()
    {
        if (Selection.activeTransform != null)
        {
            Light lt = Selection.activeTransform.GetComponent<Light>();

            if (lt != null)
            {
                lt.color = Color.red;
            }
        }
    }
}
