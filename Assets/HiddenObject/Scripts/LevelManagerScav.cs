using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using DG.Tweening;

using BitBenderGames;

[System.Serializable]
public class AreaBoundaries
{

    public Vector2 MinBound;
    public Vector2 MaxBound;
   

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
    public LayerMask requiredLayer;
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
        UIManagerScav.instance.Sv_fillText.text = totalHiddenObjectsFound + "/" + maxHiddenObjectToFound;
        UIManagerScav.instance.Sv_FillBar.fillAmount = Mathf.InverseLerp(0, maxHiddenObjectToFound, totalHiddenObjectsFound);
        if (!PlayerPrefs.HasKey("Level" + GameData.instance.GetLevelNumber() + "LatestUnlockedArea"))
        {
            PlayerPrefs.SetInt(("Level" + GameData.instance.GetLevelNumber() + "LatestUnlockedArea"), 0);

        }

        AreaHolderObj.AreaUnlockedTill = PlayerPrefs.GetInt(("Level" + GameData.instance.GetLevelNumber() + "LatestUnlockedArea"), 0);
        Invoke(nameof(AssignHiddenObjects), 1);
        



        //AssignHiddenObjects();
    }




    void AssignHiddenObjects()  //Method selects objects from the hiddenobjects list which should be hidden
    {
        UIManagerScav.instance.Sv_FillBar.fillAmount = Mathf.InverseLerp(0, maxHiddenObjectToFound, totalHiddenObjectsFound);

        Cam.BoundaryMin = AreaHolderObj.areaBoundaries[AreaHolderObj.AreaUnlockedTill].MinBound;
        Cam.BoundaryMin = AreaHolderObj.areaBoundaries[AreaHolderObj.AreaUnlockedTill].MaxBound;

        //Debug.Log("LevelInfo "+ LevelManager.gameObject.name);
        AreaHolderObj = LevelManager.instance.CurrentLevel.GetComponent<AreaHolder>();


        activeHiddenObjectList = new List<Transform>();
        //totalHiddenObjectsFound = 0;
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





        for (int i = 0; i < AreaHolderObj.HiddenObjectList.Count; i++)
        {

            activeHiddenObjectList.Add(AreaHolderObj.HiddenObjectList[i]);

        }



        UIManagerScav.instance.PopulateHiddenObjectIcons(activeHiddenObjectList);   //send the activeHiddenObjectList to UIManager
        gameStatus = GameStatus.PLAYING;                                        //set gamestatus to Playing
    }




    //In Update() we will check the mouse tap and decide if it is a valid hidden object, also if the required count is completed
    private void Update()
    {
        if (gameStatus == GameStatus.PLAYING)                               //check if gamestatus is Playing
        {
            //if (Input.GetMouseButtonDown(0))                                //check for left mouse tap
            //{
            //    pos = Cam.ScreenToWorldPoint(Input.mousePosition);  //get the position of mouse tap and conver it to WorldPoint
            //    hit = Physics2D.Raycast(pos, Vector2.zero, 100, requiredLayer);                 //create a Raycast hit from mouse tap position



            //}


            //if (Input.GetMouseButtonDown(0)) // Detect if the left mouse button was clicked
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Create a ray from the camera through the mouse position
            //    RaycastHit hit; // Create a RaycastHit variable to store information about the hit

            //    if (Physics.Raycast(ray, out hit, 500)) // Cast the ray and check if it hit an object within the specified length
            //    {
            //        Debug.Log("Hit object: " + hit.collider.gameObject.name); // Log the name of the hit object




            if (_Controller.TapedCollider != null)                            //check if hit and collider is not null
            {

                Debug.Log("hit.collider" + _Controller.TapedCollider.name);

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
                    Debug.Log("You won the game");                      //if yes then we have won the game
                    UIManagerScav.instance.GameCompleteObj.SetActive(true); //activate GameComplete panel
                    gameStatus = GameStatus.NEXT;                       //set gamestatus to Next
                }

                _Controller.TapedCollider = null;

            }



            //  }//

            // Debug.DrawRay(ray.origin, Vector3.forward * 500, Color.red, 0.5f); // Draw a red line to show the raycast in the specified direction and length
            //}//
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


   

    //public void OnPickItem(RaycastHit hitInfo)
    //{
    //    Debug.Log("Picked a collider:///////////////////// " + hitInfo.collider);
    //  //  TapedCollider = hitInfo.collider;
    //   // ShowInfoText("" + hitInfo.collider, 2);
    //}






    public IEnumerator HintObject() //Method called by HintButton of UIManager
    {
        int randomValue = UnityEngine.Random.Range(0, activeHiddenObjectList.Count);
        Vector3 originalScale = activeHiddenObjectList[randomValue].transform.localScale;
        activeHiddenObjectList[randomValue].transform.localScale = originalScale * 1.25f;
        yield return new WaitForSeconds(0.25f);
        activeHiddenObjectList[randomValue].transform.localScale = originalScale;
    }


}









