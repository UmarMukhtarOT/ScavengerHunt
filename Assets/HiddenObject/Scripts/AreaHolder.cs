using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaHolder : MonoBehaviour
{
    [SerializeField]
    public List<AreaObjectPropertiesClass> HiddenObjectList;   //list of all the hiddenObjects available in the scene





    private void Awake()
    {
        //ArrangeList();
    }











    //Automatically rearrange the list by just clicking a button available on ObjectHolder prefab
    public void ArrangeList()
    {
        HiddenObjectList = new List<AreaObjectPropertiesClass>();
       
        
        
       


        for (int i = 0; i < transform.childCount; i++)
        {
            AreaObjectPropertiesClass hiddenObjectData = new AreaObjectPropertiesClass();
            hiddenObjectData.ObjItself = transform.GetChild(i).gameObject;
            hiddenObjectData.name = transform.GetChild(i).name;
            hiddenObjectData.makeHidden = false;

            //if (HiddenObjectList.Count == 0)
            //{
            //    HiddenObjectList.Add(hiddenObjectData);
            //    HiddenObjectList[i].Count = 1;
            //}
            //else
            //{


            //    //for (int j = 0; j < HiddenObjectList.Count; j++)
            //    //{

            //    //    if (HiddenObjectList[j].name != transform.GetChild(i).name)
            //    //    {

            //    //        HiddenObjectList.Add(hiddenObjectData);



            //    //    }
            //    //    else
            //    //    {

            //    //        HiddenObjectList[i].Count++;


            //    //    }

            //    //}


            //}





             HiddenObjectList.Add(hiddenObjectData);









        }






    }

}
