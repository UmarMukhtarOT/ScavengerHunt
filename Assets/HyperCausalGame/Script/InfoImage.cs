using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoImage : MonoBehaviour
{

    public Image img;
    public Text Txt;

   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetInfovalues(string str,Sprite spr) 
    {

        Debug.Log("info image count is: "+str+ " name "+ spr.name);
        img.sprite = spr;   
        Txt.text = spr.name +"\n"+ str;
    
    
    
    }





}
