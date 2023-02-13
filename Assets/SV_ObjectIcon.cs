using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SV_ObjectIcon : MonoBehaviour
{
   public Image childImg;
   public Text childText;
    public int TotalObjects=0;
   
            
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void setIconProperties(string IconName, Sprite IconSpr)
    {
        if (!PlayerPrefs.HasKey(IconName + "Collected"))
        {
            PlayerPrefs.SetInt((IconName + "Collected"), 0);

        }

        transform.name = IconName;
        childImg.sprite = IconSpr;
        

        updateStats(PlayerPrefs.GetInt((IconName + "Collected"), 0));

    }






    public void updateStats(int collected)
    {

        childText.text = collected + "/" + TotalObjects;




    }






}
