using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AreaObjectPropertiesClass
{


    public GameObject ObjItself;

}



public class AreaHolder : MonoBehaviour
{

    public Transform CollectableObjects;
    [SerializeField]
    public List<AreaObjectPropertiesClass> HiddenObjectList;   //list of all the hiddenObjects available in the scene
   







    
    public void ArrangeList()
    {
        HiddenObjectList = new List<AreaObjectPropertiesClass>();
       
        for (int i = 0; i < CollectableObjects.transform.childCount; i++)
        {
            AreaObjectPropertiesClass hiddenObjectData = new AreaObjectPropertiesClass();
            hiddenObjectData.ObjItself.name = CollectableObjects.transform.GetChild(i).name;
            string name = CollectableObjects.transform.GetChild(i).name;
            HiddenObjectList.Add(hiddenObjectData);

        }


    }

}
