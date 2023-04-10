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
   public GameObject tickmark;


    private void Start()
    {
        tickmark.SetActive(false);
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


       

        int val = collected % LevelManagerScav.instance.appreciateEveryTurn;

       // Debug.Log(" val is currently " + val);

        if (collected>0)
        {
            if (LevelManagerScav.instance.appreciateEveryTurn % collected == 0)
            {
                Debug.Log("apper " + val + " collected " + collected);
                //   UIManagerScav.instance.AppericiateWithAdpanel.SetActive(true);

            }

        }
        



    }


    public void updateSibling()
     {

        int collected = PlayerPrefs.GetInt((transform.name + "Collected"), 0);

        if (collected >= TotalObjects && TotalObjects != 0)
        {

            //  Debug.Log("completes an object"+ transform.name);

            transform.SetAsLastSibling();
           tickmark.SetActive(true);



        }


    }




}
