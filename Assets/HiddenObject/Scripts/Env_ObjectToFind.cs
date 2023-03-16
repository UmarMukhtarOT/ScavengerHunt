
using UnityEngine;



public class Env_ObjectToFind : MonoBehaviour
{
    public bool IsTaken = false;
    public bool IsFindAble = false;
    public string Id;
    public SpriteRendererData ObjectsProperties;





    void Start()
    {
        ObjectsProperties.spriteRenderer = GetComponent<SpriteRenderer>();



        if (IsFindAble)
        {
            Id = transform.parent.name + "_" + transform.name + transform.GetSiblingIndex() + "_IsTaken";

            if (!PlayerPrefs.HasKey(Id))
            {
                PlayerPrefs.SetInt((Id), 0);

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


    }






   
}
