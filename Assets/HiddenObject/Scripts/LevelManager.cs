using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class HiddenObjectData
{
    public string name;
    public int ID;
    public int count;
    public GameObject ObjItself;
    public bool makeHidden = false;
}

public enum GameStatus
{
    PLAYING,
    NEXT
}


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool IsTimeLimited;
    [SerializeField] private float timeLimit = 0;                       
    [SerializeField] private int maxHiddenObjectToFound = 6;            
    [SerializeField] private ObjectHolder objectHolderPrefab;           //ObjectHolderPrefab contains list of all the hiddenObjects available in it

    [HideInInspector] public GameStatus gameStatus = GameStatus.NEXT;   
    private List<HiddenObjectData> activeHiddenObjectList;              //list hidden objects which are marked as hidden from the above list
    private float currentTime;                                          
    private int totalHiddenObjectsFound = 0;                            
    private TimeSpan time;                                              
    private RaycastHit2D hit;
    private Vector3 pos;                                                //hold Mouse Tap position converted to WorldPoint

    [SerializeField]
    public List<ObjectHolder> objectHolder;




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
        activeHiddenObjectList = new List<HiddenObjectData>();          
        AssignHiddenObjects();                                  
    }

    void AssignHiddenObjects()  //Method select objects from the hiddenobjects list which should be hidden
    {
        //ObjectHolder objectHolder = Instantiate(objectHolderPrefab, Vector3.zero, Quaternion.identity);
       // Debug.Log("objectHolder "+ objectHolder.name);
        totalHiddenObjectsFound = 0;                                      
        activeHiddenObjectList.Clear();                                    
        gameStatus = GameStatus.PLAYING;




        if (IsTimeLimited)
        {
            UIManager.instance.TimerText.text = "" + timeLimit;
            currentTime = timeLimit;
        }
        else
        {
            UIManager.instance.TimerText.transform.parent.gameObject.SetActive(false);


        }


        for (int i = 0; i < objectHolder[0].HiddenObjectList.Count; i++)
        {
            //deacivate collider, as we only want selected hidden objects to have collider active
            objectHolder[0].HiddenObjectList[i].ObjItself.GetComponent<Collider2D>().enabled = false;
        }









        int k = 0;
        while (k < maxHiddenObjectToFound) //we check while k is less that maxHiddenObjectToFound, keep looping
        {
            //we randomly select any number between 0 to hiddenObjectList.Count
            int randomNo = UnityEngine.Random.Range(0, objectHolder[0].HiddenObjectList.Count);
            //then we check is the makeHidden bool of that hiddenObject is false
            if (!objectHolder[0].HiddenObjectList[randomNo].makeHidden)
            {
                //we are setting the object name similar to index, because we are going to use index to identify the tapped object
                //and this index will help us to deactive the hidden object icon from the UI
               // objectHolder[0].HiddenObjectList[randomNo].ObjItself.name = "Item " + k;    //set their name to index

                objectHolder[0].HiddenObjectList[randomNo].makeHidden = true;          //if false, then we set it to true
                                                                                    //activate its collider, so we can detect it on tap
                objectHolder[0].HiddenObjectList[randomNo].ObjItself.GetComponent<Collider2D>().enabled = true;
                activeHiddenObjectList.Add(objectHolder[0].HiddenObjectList[randomNo]);//add the hidden object to the activeHiddenObjectList
                k++;                                                                //and increase the k
            }
        }





        UIManager.instance.PopulateHiddenObjectIcons(activeHiddenObjectList);   //send the activeHiddenObjectList to UIManager
        gameStatus = GameStatus.PLAYING;                                        //set gamestatus to Playing
    }









    private void Update()
    {
        if (gameStatus == GameStatus.PLAYING)                               //check if gamestatus is Playing
        {
            if (Input.GetMouseButtonDown(0))                                //check for left mouse tap
            {
                pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //get the position of mouse tap and conver it to WorldPoint
                hit = Physics2D.Raycast(pos, Vector2.zero);                 //create a Raycast hit from mouse tap position
                if (hit && hit.collider != null)                            //check if hit and collider is not null
                {
                    hit.collider.gameObject.SetActive(false);               //deactivate the hit object
                    //Remember we renamed all our object to their respective Index, we did it for UIManager
                    UIManager.instance.CheckSelectedHiddenObject(hit.collider.gameObject.name); //send the name of hit object to UIManager

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
                        UIManager.instance.GameCompleteObj.SetActive(true); //activate GameComplete panel
                        gameStatus = GameStatus.NEXT;                       //set gamestatus to Next
                    }
                }
            }


            if (IsTimeLimited)
            {
                currentTime -= Time.deltaTime;  //as long as gamestatus i in playing, we keep reducing currentTime by Time.deltaTime

                time = TimeSpan.FromSeconds(currentTime);                       //set the time value
                UIManager.instance.TimerText.text = time.ToString("mm':'ss");   //convert time to Time format
                if (currentTime <= 0)                                           //if currentTime is less or equal to zero
                {
                    Debug.Log("Time Up");                                       //if yes then we have lost the game
                    UIManager.instance.GameCompleteObj.SetActive(true);         //activate GameComplete panel
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

