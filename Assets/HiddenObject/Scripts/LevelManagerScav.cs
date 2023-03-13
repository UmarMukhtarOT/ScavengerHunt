using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class AreaProperties
{
    
   
    public GameObject AreaItself;
   
}

public enum GameStatus
{
    PLAYING,
    NEXT
}


public class LevelManagerScav : MonoBehaviour
{
    public static LevelManagerScav instance;
    public bool IsTimeLimited;
    public Camera Cam;
    private int totalItemCount;
    public int[] perLevelReqCount;


    [SerializeField] private float timeLimit = 0;                       
    [SerializeField] private int maxHiddenObjectToFound = 6;            
    [SerializeField] private AreaHolder objectHolderPrefab;           //ObjectHolderPrefab contains list of all the hiddenObjects available in it
    [HideInInspector]public GameStatus gameStatus = GameStatus.NEXT;   
   
    private List<AreaObjectPropertiesClass> activeHiddenObjectList;              //list hidden objects which are marked as hidden from the above list
    private float currentTime;                                          
    private int totalHiddenObjectsFound = 0;                            
    private TimeSpan time;                                              
    private RaycastHit2D hit;
    private Vector3 pos;                                                //hold Mouse Tap position converted to WorldPoint

    [SerializeField]
    public List<AreaHolder> AreaHolder;
    public LayerMask requiredLayer;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        activeHiddenObjectList = new List<AreaObjectPropertiesClass>();          
        AssignHiddenObjects();                                  
    }

    void AssignHiddenObjects()  //Method selects objects from the hiddenobjects list which should be hidden
    {
        totalHiddenObjectsFound = 0;                                      
        activeHiddenObjectList.Clear();                                    
        gameStatus = GameStatus.PLAYING;


        if (IsTimeLimited)
        {
            UIManagerScav.instance.TimerText.text = "" + timeLimit;
            currentTime = timeLimit;
        }
        else
        {
            UIManagerScav.instance.TimerText.transform.parent.gameObject.SetActive(false);


        }


       
        
        
        for (int i = 0; i < AreaHolder[0].HiddenObjectList.Count; i++)
        {
            
            activeHiddenObjectList.Add(AreaHolder[0].HiddenObjectList[i]);

        }



        UIManagerScav.instance.PopulateHiddenObjectIcons(activeHiddenObjectList);   //send the activeHiddenObjectList to UIManager
        gameStatus = GameStatus.PLAYING;                                        //set gamestatus to Playing
    }





    private void Update()
    {
        if (gameStatus == GameStatus.PLAYING)                               //check if gamestatus is Playing
        {
            if (Input.GetMouseButtonDown(0))                                //check for left mouse tap
            {
                pos = Cam.ScreenToWorldPoint(Input.mousePosition);  //get the position of mouse tap and conver it to WorldPoint
                hit = Physics2D.Raycast(pos, Vector2.zero,100,requiredLayer);                 //create a Raycast hit from mouse tap position
               

                if (hit && hit.collider != null)                            //check if hit and collider is not null
                {

                    //Debug.Log("hit.collider" + hit.collider.name);

                    hit.collider.gameObject.SetActive(false);               //deactivate the hit object
                    //Remember we renamed all our object to their respective Index, we did it for UIManager
                    UIManagerScav.instance.CheckSelectedHiddenObject(hit.collider.transform); //send the name of hit object to UIManager

                    for (int i = 0; i < activeHiddenObjectList.Count; i++)
                    {
                        if (activeHiddenObjectList[i].ObjItself.name == hit.collider.gameObject.name)
                        {
                            activeHiddenObjectList.RemoveAt(i);
                            break;
                        }
                    }

                    totalHiddenObjectsFound++;                              //increase totalHiddenObjectsFound count

                    //check if totalHiddenObjectsFound is more or equal to maxHiddenObjectToFound
                    if (totalHiddenObjectsFound >= maxHiddenObjectToFound)  
                    {
                        Debug.Log("You won the game");                      //if yes then we have won the game
                        UIManagerScav.instance.GameCompleteObj.SetActive(true); //activate GameComplete panel
                        gameStatus = GameStatus.NEXT;                       //set gamestatus to Next
                    }
                }
            }


            if (IsTimeLimited)
            {
                currentTime -= Time.deltaTime;  //as long as gamestatus i in playing, we keep reducing currentTime by Time.deltaTime

                time = TimeSpan.FromSeconds(currentTime);                       //set the time value
                UIManagerScav.instance.TimerText.text = time.ToString("mm':'ss");   //convert time to Time format
                if (currentTime <= 0)                                           //if currentTime is less or equal to zero
                {
                    Debug.Log("Time Up");                                       //if yes then we have lost the game
                    UIManagerScav.instance.GameCompleteObj.SetActive(true);         //activate GameComplete panel
                    gameStatus = GameStatus.NEXT;                               //set gamestatus to Next
                }
            }
        }
    }







    public IEnumerator HintObject() //Method called by HintButton of UIManager
    {
        int randomValue = UnityEngine.Random.Range(0, activeHiddenObjectList.Count);
        Vector3 originalScale = activeHiddenObjectList[randomValue].ObjItself.transform.localScale;
        activeHiddenObjectList[randomValue].ObjItself.transform.localScale = originalScale * 1.25f;
        yield return new WaitForSeconds(0.25f);
        activeHiddenObjectList[randomValue].ObjItself.transform.localScale = originalScale;
    }










}

