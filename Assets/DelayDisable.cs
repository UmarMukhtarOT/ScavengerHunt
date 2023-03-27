using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDisable : MonoBehaviour
{
    public float Delay = 2;



    // Start is called before the first frame update
   void OnEnable()
    {
        Invoke(nameof(Disable), Delay);
    }

    // Update is called once per frame
    void Disable()
    {

        gameObject.SetActive(false);
        
    }
}
