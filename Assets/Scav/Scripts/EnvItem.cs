using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnvItem : MonoBehaviour
{

    public int ID;

   
    public float heightOffSet = 0.7f;
    public LayerMask layer;
    [HideInInspector]
    
    Vector3 screenPoint;
    
    

    // Start is called before the first frame update
    void Start()
    {
      
      

    }



    private void OnMouseUp()
    {
           
        
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z); 
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);
           
           
           
    }


      


}


    


