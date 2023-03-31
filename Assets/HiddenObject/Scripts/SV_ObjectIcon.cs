using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class SV_ObjectIcon : MonoBehaviour
{
   public Image childImg;
   public Text CollectedObjectsText;
   public Text TotalObjectsText;
   public int TotalObjects=0;

    private void Start()
    {
        Invoke("updateCollectedText",0.1f);
    }



    public void setIconProperties(string IconName, Sprite IconSpr)
    {
        if (!PlayerPrefs.HasKey(IconName + "Collected"))
        {
            PlayerPrefs.SetInt((IconName + "Collected"), 0);
        }

        transform.name = IconName;
        childImg.sprite = IconSpr;
        updateCollectedText();
       

    }






    public void updateCollectedText()
    {

        int collected = PlayerPrefs.GetInt((transform.name + "Collected"), 0);

        CollectedObjectsText.text = collected+"";
        TotalObjectsText.text = TotalObjects + "";
        Invoke("updateSibling",5);

        
    }

   
     public void updateSibling()
     {

        int collected = PlayerPrefs.GetInt((transform.name + "Collected"), 0);

        if (collected >= TotalObjects && TotalObjects != 0)
        {

            //  Debug.Log("completes an object"+ transform.name);

            transform.SetAsLastSibling();


        }


    }




}
