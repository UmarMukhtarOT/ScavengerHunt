using System.Collections;
using UnityEngine;


public class BeingApple : MonoBehaviour
{

    private Transform BarImage;
    private Transform barParent;
    private Transform Prefb;
    private Transform targetPos;
    private Transform clone;
    private bool IsCollected;
    private Camera Cam;
    private bool startMoving;
   
    
    

    // Start is called before the first frame update
    void Start()
    {
        
       // barParent = GameManager.Instance.AppleBarImage;
      //  Prefb = GameManager.Instance.AppleImagePrefb;
      //  Cam = GameManager.Instance.SceneCam.GetComponent<Camera>();
      //  targetPos = GameManager.Instance.AppletargetPos;

    }

    // Update is called once per frame
    void Update()
    {
       

        if (startMoving)
        {
            clone.transform.localPosition = Vector3.Lerp(clone.transform.localPosition,targetPos.localPosition,5*Time.deltaTime);
          //   clone.transform.position = Vector3.Lerp(clone.transform.position, targetPos.position, 0.1f);
            

        }


    }



    public void ActivateApple()
    {
        if (!IsCollected)
        {
            IsCollected = true;
            StartCoroutine(Apple());
           // Debug.Log(transform.name+" Activated by  "+str);
        }


    }







    IEnumerator Apple()
    {

        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(1);
        Vector3 screenPos = Cam.WorldToScreenPoint(transform.position);
        screenPos = new Vector3(screenPos.x + (-20), screenPos.y + (12), screenPos.z);
        clone=Instantiate(Prefb) as Transform;
        clone.transform.parent = barParent.transform;
        clone.transform.rotation = Quaternion.identity;
        clone.transform.position = screenPos;
        startMoving = true;
        transform.GetComponent<MeshRenderer>().enabled = false;
        transform.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(0.65f);
      //  startMoving = false;
        
       //*** GameManager.Instance.CollectApples();
        
        yield return new WaitForSeconds(20);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {


        ActivateApple();


    }


}






