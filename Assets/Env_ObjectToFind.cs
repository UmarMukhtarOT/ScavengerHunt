using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class Env_ObjectToFind : MonoBehaviour
{
    //public string ObjName;
    public string Id;
    public bool IsTaken=false;





    // Start is called before the first frame update
    void Start()
    {
        Id=transform.parent.name+"_"+transform.name+transform.GetSiblingIndex()+ "_IsTaken";

        if (!PlayerPrefs.HasKey(Id))
        {
            PlayerPrefs.SetInt((Id ), 0);

        }




        if (PlayerPrefs.GetInt((Id), 0) != 0)
        {

            IsTaken = true;
            this.gameObject.SetActive(false);

        }
        else
        { 
            IsTaken = false;

        }










    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
