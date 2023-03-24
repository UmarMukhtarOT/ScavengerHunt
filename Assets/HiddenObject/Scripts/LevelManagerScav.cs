using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using BitBenderGames;


public enum GameStatus
{
    PLAYING,
    NEXT
}


public class LevelManagerScav : MonoBehaviour
{
    public static LevelManagerScav instance;
    public bool IsTimeLimited;
    public MobileTouchCamera Cam;
    private int totalItemCount;
    public int[] perLevelReqCount;


    [SerializeField] private float timeLimit = 0;
    [SerializeField] public int maxHiddenObjectToFound = 6;
    [SerializeField] private AreaHolder objectHolderPrefab;           //ObjectHolderPrefab contains list of all the hiddenObjects available in it
    [HideInInspector] public GameStatus gameStatus = GameStatus.NEXT;

    private List<Transform> activeHiddenObjectList;              //list hidden objects which are marked as hidden from the above list
    private float currentTime;
    public int totalHiddenObjectsFound = 0;
    private TimeSpan time;
    private RaycastHit2D hit;
    private Vector3 pos;                                                //hold Mouse Tap position converted to WorldPoint

    [SerializeField]
    public AreaHolder AreaHolderObj;
    
    public DemoController _Controller;
    
   






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
      
        if (!PlayerPrefs.HasKey("Level" + GameData.instance.GetLevelNumber() + "LatestUnlockedArea"))
        {
            PlayerPrefs.SetInt(("Level" + GameData.instance.GetLevelNumber() + "LatestUnlockedArea"), 0);

        }

       
        Invoke(nameof(SetupLevel), 1);
        



        
    }




    void SetupLevel()  //Method selects objects from the hiddenobjects list which should be hidden
    {
       
        
        AreaHolderObj = LevelManager.instance.CurrentLevel.GetComponent<AreaHolder>();


        UIManagerScav.instance.Sv_FillBar.fillAmount = Mathf.InverseLerp(0, maxHiddenObjectToFound, totalHiddenObjectsFound);
        AreaHolderObj.AreaUnlockedTill = PlayerPrefs.GetInt(("Level" + GameData.instance.GetLevelNumber() + "LatestUnlockedArea"), 0);
        Cam.GetComponent<SetBoundaryFromCollider>().SetBoundary(AreaHolderObj.areaProps.areaColliders[AreaHolderObj.AreaUnlockedTill]);
        activeHiddenObjectList = new List<Transform>();
        activeHiddenObjectList.Clear();
        maxHiddenObjectToFound = AreaHolderObj.areaProps.AreaObjNum[AreaHolderObj.AreaUnlockedTill];
        UIManagerScav.instance.Sv_fillText.text = totalHiddenObjectsFound + "/" + maxHiddenObjectToFound;
        UIManagerScav.instance.Sv_FillBar.fillAmount = Mathf.InverseLerp(0, maxHiddenObjectToFound, totalHiddenObjectsFound);

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





        for (int i = 0; i < AreaHolderObj.HiddenObjectList.Count; i++)
        {

            activeHiddenObjectList.Add(AreaHolderObj.HiddenObjectList[i]);

        }



        UIManagerScav.instance.PopulateHiddenObjectIcons(activeHiddenObjectList);   //send the activeHiddenObjectList to UIManager
        gameStatus = GameStatus.PLAYING;                                        //set gamestatus to Playing



        // PlayerPrefs.SetInt(("Level" + GameData.instance.GetLevelNumber() + "LatestUnlockedArea"), AreaHolderObj.AreaUnlockedTill);

        Debug.Log("unlocked till"+ AreaHolderObj.AreaUnlockedTill);


        for (int i = 1; i < AreaHolderObj.AreaUnlockedTill; i++)
        {

            AreaHolderObj.areaProps.areaColliders[i].GetComponent<Animator>().enabled=true;



        }



    }




    
    private void Update()
    {
        if (gameStatus == GameStatus.PLAYING)                               //check if gamestatus is Playing
        {
            


            if (_Controller.TapedCollider != null)                            //check if hit and collider is not null
            {

                Debug.Log("hit.collider//////////" + _Controller.TapedCollider.name);

                _Controller.TapedCollider.gameObject.SetActive(false);               //deactivate the hit object


                UIManagerScav.instance.CheckSelectedHiddenObject(_Controller.TapedCollider.transform); //send the transform of hit object to UIManager

                for (int i = 0; i < activeHiddenObjectList.Count; i++)
                {
                    if (activeHiddenObjectList[i].name == _Controller.TapedCollider.gameObject.name)
                    {
                        activeHiddenObjectList.RemoveAt(i);
                        break;
                    }
                }

                totalHiddenObjectsFound++;                              //increase totalHiddenObjectsFound count

                UIManagerScav.instance.Sv_fillText.text = totalHiddenObjectsFound + "/" + maxHiddenObjectToFound;
                
               

                UIManagerScav.instance.Sv_FillBar.fillAmount = Mathf.InverseLerp(0, maxHiddenObjectToFound, totalHiddenObjectsFound); 


                //check if totalHiddenObjectsFound is more or equal to maxHiddenObjectToFound
                if (totalHiddenObjectsFound >= maxHiddenObjectToFound)
                {

                    Debug.Log("AreaUnlockedTill " + AreaHolderObj.AreaUnlockedTill);

                    if (AreaHolderObj.AreaUnlockedTill < AreaHolderObj.areaProps.areaColliders.Count)
                    {

                        Debug.Log("AreaUnlockedTill "+ AreaHolderObj.AreaUnlockedTill);
                        UnlockNextArea();
                        SetupLevel();
                    }
                    else
                    {
                        Debug.Log("You won the game");                      //if yes then we have won the game
                        //UIManagerScav.instance.GameCompleteObj.SetActive(true); //activate GameComplete panel
                        PlayerPrefs.DeleteAll();
                        GameManager.instance.levelCompleteManager.LevelComplete(1);

                        
                      //  gameStatus = GameStatus.NEXT;

                    }
                   
                                       //set gamestatus to Next
                }

                _Controller.TapedCollider = null;

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

    public void UnlockNextArea()
    {

        AreaHolderObj.AreaUnlockedTill++;
        PlayerPrefs.SetInt(("Level" + GameData.instance.GetLevelNumber() + "LatestUnlockedArea"), AreaHolderObj.AreaUnlockedTill);
        BoxCollider NextBound = AreaHolderObj.areaProps.areaColliders[AreaHolderObj.AreaUnlockedTill];
        Vector3 NextAreaPos = AreaHolderObj.areaProps.AreaPos[AreaHolderObj.AreaUnlockedTill].transform.position;
        Cam.GetComponent<SetBoundaryFromCollider>().SetBoundary(NextBound);



        NextAreaPos.z = Cam.transform.position.z;
        Cam.transform.DOMove(NextAreaPos, 3).OnComplete(() =>
        {
            NextBound.GetComponent<Animator>().enabled = true;
           Debug.Log("dsfd");
        }
        
        );

      
    }




   




    public IEnumerator HintObject() //Method called by HintButton of UIManager
    {
        int randomValue = UnityEngine.Random.Range(0, activeHiddenObjectList.Count);
        Vector3 originalScale = activeHiddenObjectList[randomValue].transform.localScale;
        activeHiddenObjectList[randomValue].transform.localScale = originalScale * 1.25f;
        yield return new WaitForSeconds(0.25f);
        activeHiddenObjectList[randomValue].transform.localScale = originalScale;
    }


}









