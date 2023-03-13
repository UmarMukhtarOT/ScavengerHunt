using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AreaObjectPropertiesClass
{


    public GameObject ObjItself;

}



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
    public List<Env_ObjectToFind> spriteRendererDataList;








    private void Start()
    {
        foreach (Env_ObjectToFind item in spriteRendererDataList)
        {
            item.ObjectsProperties.spriteRenderer.sprite = item.ObjectsProperties.sprites[item.ObjectsProperties.currentSpriteIndex];
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



























}
