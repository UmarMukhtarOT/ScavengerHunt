﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class SpriteRendererData
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> sprites;
    public float duration = 1f; // in seconds
    public int currentSpriteIndex = 0;
    private float elapsedTime = 0f;




    public void SwitchSprite()
    {
        currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Count;
        spriteRenderer.sprite = sprites[currentSpriteIndex];
        elapsedTime = 0f;
    }

    public void UpdateElapsedTime()
    {
        elapsedTime += Time.deltaTime;
    }

    public bool HasElapsed()
    {
        return elapsedTime >= duration;
    }
}



[System.Serializable]
public class AreaProps
{

   // public List<Transform> clouds;
    public List<Transform> AreaPos;
    public List<int> AreaObjNum;
    public List<BoxCollider> areaColliders;
    
}



public class AreaHolder : MonoBehaviour
{

    [SerializeField] public bool EditorSpawn;

    public GameObject Plane;
    public GameObject MagnifyingCircle;
    public GameObject TutorailBG;
    public GameObject tutorailmask;
    public List<Sprite> Sprites1;
    public List<Sprite> Sprites2;
    [SerializeField] public int AreaUnlockedTill;

    [SerializeField] public Env_ObjectToFind AreaObjPrefab;
    [SerializeField] public Transform Objectparent;
    [SerializeField] public Transform CollectableObjects;
    [SerializeField] public List<Transform> HiddenObjectList;   //list of all the hiddenObjects available in the scene


    [SerializeField] public List<Env_ObjectToFind> spriteRendererDataList;

    [Space(10)]
    public AreaProps areaProps;
   







    private void Start()
    {
        foreach (Env_ObjectToFind item in spriteRendererDataList)
        {
            item.ObjectsProperties.spriteRenderer.sprite = item.ObjectsProperties.sprites[item.ObjectsProperties.currentSpriteIndex];
        }




        if (PlayerPrefs.GetInt("TutorialPlayed", 0) == 1)
        {

            TutorialEnd();

        }
       





    }

    private void Update()
    {
        foreach (Env_ObjectToFind item in spriteRendererDataList)
        {
            item.ObjectsProperties.UpdateElapsedTime();

            if (item.ObjectsProperties.HasElapsed())
            {
                item.ObjectsProperties.SwitchSprite();
            }
        }

    }
  

    public void ArrangeList()
    {
        //HiddenObjectList = new List<AreaObjectPropertiesClass>();

        //for (int i = 0; i < CollectableObjects.transform.childCount; i++)
        //{

        //    AreaObjectPropertiesClass hiddenObjectData = new AreaObjectPropertiesClass();
        //    Debug.Log("dfdsfsd " + hiddenObjectData.ObjItself);

        //    hiddenObjectData.ObjItself.name = CollectableObjects.transform.GetChild(i).name;

        //    string name = CollectableObjects.transform.GetChild(i).name;
        //    HiddenObjectList.Add(hiddenObjectData);

        //}



        if (EditorSpawn)
        {


            for (int i = 0; i < Sprites1.Count; i++)
            {


                Env_ObjectToFind objToFind = Instantiate(AreaObjPrefab, Objectparent);

                spriteRendererDataList.Add(objToFind);


                string name = Sprites1[i].name.Remove(Sprites1[i].name.Length - 2);




                spriteRendererDataList[i].name = name;


                objToFind.ObjectsProperties.spriteRenderer = spriteRendererDataList[i].ObjectsProperties.spriteRenderer.GetComponent<SpriteRenderer>();
                objToFind.ObjectsProperties.spriteRenderer.sprite = Sprites1[i];
                objToFind.ObjectsProperties.sprites[0] = Sprites1[i];
                objToFind.ObjectsProperties.sprites[1] = Sprites2[i];
                objToFind.ObjectsProperties.duration = 1;





            }

        }
        else
        {
            Debug.Log("Set the 'EditorSpawn' bool true to spawn objects. ");



        }

    }





    public void TutorialEnd()
    {
        Plane.SetActive(true);
        tutorailmask.SetActive(false);
        TutorailBG.SetActive(false);



        foreach (var item in HiddenObjectList)
        {
            item.tag = "HidddenObject";
        }
    
    
    
    }







    public void Tutorialstart()
    {
        
        tutorailmask.SetActive(true);
        TutorailBG.SetActive(true);



    }






}
