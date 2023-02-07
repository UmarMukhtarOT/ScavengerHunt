using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaHolder : MonoBehaviour
{

    public Transform CollectableObjects;
    [SerializeField]
    public List<AreaObjectPropertiesClass> HiddenObjectList;   //list of all the hiddenObjects available in the scene
   







    //bool ProductExist(string name)
    //{
    //    for (int j = 0; j < HiddenObjectList.Count; j++)
    //    {

    //        if (HiddenObjectList[j].name.Equals(name))
    //        {
    //            HiddenObjectList[j].Count++;

    //            return true;
    //        }
    //    }
    //    return false;
    //}





    //Automatically rearrange the list by just clicking a button available on ObjectHolder prefab
    public void ArrangeList()
    {
        HiddenObjectList = new List<AreaObjectPropertiesClass>();
       
        for (int i = 0; i < CollectableObjects.transform.childCount; i++)
        {
            AreaObjectPropertiesClass hiddenObjectData = new AreaObjectPropertiesClass();
            hiddenObjectData.name       = CollectableObjects.transform.GetChild(i).name;
           // hiddenObjectData.makeHidden = false;


            string name = CollectableObjects.transform.GetChild(i).name;
            HiddenObjectList.Add(hiddenObjectData);


            {
                //if (HiddenObjectList.Count == 0)
                //{

                //    HiddenObjectList.Add(hiddenObjectData);
                //    HiddenObjectList[i].Count = 1;


                //}
                //else
                //{
                //    if(!ProductExist(name))
                //    {


                //        HiddenObjectList.Add(hiddenObjectData);
                //        HiddenObjectList[i].Count = 1;
                //    }
                //    else
                //    {

                //    }

                //}


            }



        }






    }

}
