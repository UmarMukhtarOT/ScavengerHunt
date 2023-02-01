using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    [SerializeField]
    public List<HiddenObjectData> HiddenObjectList;   //list of all the hiddenObjects available in the scene

   


    //Automatically rearrange the list by just clicking a button available on ObjectHolder prefab
    public void ArrangeList()
    {
        HiddenObjectList = new List<HiddenObjectData>();

        for (int i = 0; i < transform.childCount; i++)
        {
            HiddenObjectData hiddenObjectData = new HiddenObjectData();
            //hiddenObjectData.ID = i+36;
            hiddenObjectData.ObjItself = transform.GetChild(i).gameObject;
            hiddenObjectData.name = transform.GetChild(i).name;
            hiddenObjectData.makeHidden = false;

            HiddenObjectList.Add(hiddenObjectData);
        }
    }

}
