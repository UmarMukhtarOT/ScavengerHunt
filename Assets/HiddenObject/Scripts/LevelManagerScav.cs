using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using BitBenderGames;
using static UnityEngine.GraphicsBuffer;


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
    private TouchInputController TouchInput;
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

        TouchInput = Cam.GetComponent<TouchInputController>();
        Invoke(nameof(SetupLevel), 1);

    }




    void SetupLevel()  //Method selects objects from the hiddenobjects list which should be hidden
    {


        AreaHolderObj = LevelManager.instance.CurrentLevel.GetComponent<AreaHolder>();

        TouchInput.enabled = false;
        for (int i = 0; i <= PlayerPrefs.GetInt(("Level" + GameData.instance.GetLevelNumber() + "LatestUnlockedArea")); i++)
        {

            //  Debug.Log(" ////// unlocked areas " + PlayerPrefs.GetInt(("Level" + GameData.instance.GetLevelNumber() + "LatestUnlockedArea")));

            AreaHolderObj.areaProps.areaColliders[i].GetComponent<Animator>().enabled = true;
            // AreaHolderObj.areaProps.areaColliders[i].enabled = true;

        }

        UIManagerScav.instance.Sv_FillBar.fillAmount = Mathf.InverseLerp(0, maxHiddenObjectToFound, totalHiddenObjectsFound);
        AreaHolderObj.AreaUnlockedTill = PlayerPrefs.GetInt(("Level" + GameData.instance.GetLevelNumber() + "LatestUnlockedArea"), 0);


        Cam.GetComponent<SetBoundaryFromCollider>().SetBoundary(AreaHolderObj.areaProps.areaColliders[AreaHolderObj.AreaUnlockedTill]);

        activeHiddenObjectList = new List<Transform>();
        activeHiddenObjectList.Clear();
        maxHiddenObjectToFound = AreaHolderObj.areaProps.AreaObjNum[AreaHolderObj.AreaUnlockedTill];
        UIManagerScav.instance.Sv_fillText.text = totalHiddenObjectsFound + "/" + maxHiddenObjectToFound;
        UIManagerScav.instance.Sv_FillBar.fillAmount = Mathf.InverseLerp(0, maxHiddenObjectToFound, totalHiddenObjectsFound);
        UIManagerScav.instance.TimerText.transform.parent.gameObject.SetActive(false);
        gameStatus = GameStatus.PLAYING;





        //if (IsTimeLimited)
        //{
        //    UIManagerScav.instance.TimerText.text = "" + timeLimit;
        //    currentTime = timeLimit;
        //}
        //else
        //{
        //  UIManagerScav.instance.TimerText.transform.parent.gameObject.SetActive(false);


        // }





        for (int i = 0; i < AreaHolderObj.HiddenObjectList.Count; i++)
        {

            activeHiddenObjectList.Add(AreaHolderObj.HiddenObjectList[i]);

        }



        Vector3 NextAreaPos = AreaHolderObj.areaProps.AreaPos[AreaHolderObj.AreaUnlockedTill].transform.position;
        NextAreaPos.z = Cam.transform.position.z;



        Cam.transform.DOMove(NextAreaPos, 3).OnComplete(() =>
        {
            TouchInput.enabled = true;

            //  NextBound.GetComponent<Animator>().enabled = true;

        }

        );


        UIManagerScav.instance.PopulateHiddenObjectIcons(activeHiddenObjectList);   //send the activeHiddenObjectList to UIManager
        gameStatus = GameStatus.PLAYING;                                        //set gamestatus to Playing



    }





    private void Update()
    {
      
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

    public void CheckNextArea()
    {




        if (AreaHolderObj.AreaUnlockedTill < AreaHolderObj.areaProps.areaColliders.Count - 1)
        {
            Debug.Log("AreaUnlockedTill Pehly" + AreaHolderObj.AreaUnlockedTill);
            TouchInput.enabled = false;
            PlayerPrefs.SetInt(("Level" + GameData.instance.GetLevelNumber() + "LatestUnlockedArea"), AreaHolderObj.AreaUnlockedTill + 1);
            AreaHolderObj.AreaUnlockedTill = PlayerPrefs.GetInt(("Level" + GameData.instance.GetLevelNumber() + "LatestUnlockedArea"), 0);
            Debug.Log("AreaUnlockedTill Baad" + AreaHolderObj.AreaUnlockedTill);

            PlayerPrefs.SetInt(("Level" + GameData.instance.GetLevelNumber() + "LatestUnlockedArea"), AreaHolderObj.AreaUnlockedTill);



            BoxCollider NextBound = AreaHolderObj.areaProps.areaColliders[AreaHolderObj.AreaUnlockedTill];


            Cam.GetComponent<SetBoundaryFromCollider>().SetBoundary(NextBound);



            // AreaHolderObj.areaProps.areaColliders[AreaHolderObj.AreaUnlockedTill].GetComponent<Animator>().enabled = true;

            //  Debug.Log("NextAreaPos " + NextAreaPos);



            Vector3 NextAreaPos = AreaHolderObj.areaProps.AreaPos[AreaHolderObj.AreaUnlockedTill].transform.position;
            NextAreaPos.z = Cam.transform.position.z;



            Cam.transform.DOMove(NextAreaPos, 3).OnComplete(() =>
            {
                TouchInput.enabled = true;

                NextBound.GetComponent<Animator>().enabled = true;

            }

            );





            UIManagerScav.instance.Sv_FillBar.fillAmount = Mathf.InverseLerp(0, maxHiddenObjectToFound, totalHiddenObjectsFound);

            //   Cam.GetComponent<SetBoundaryFromCollider>().SetBoundary(AreaHolderObj.areaProps.areaColliders[AreaHolderObj.AreaUnlockedTill]);
            activeHiddenObjectList = new List<Transform>();
            activeHiddenObjectList.Clear();
            maxHiddenObjectToFound = AreaHolderObj.areaProps.AreaObjNum[AreaHolderObj.AreaUnlockedTill];
            UIManagerScav.instance.Sv_fillText.text = totalHiddenObjectsFound + "/" + maxHiddenObjectToFound;
            UIManagerScav.instance.Sv_FillBar.fillAmount = Mathf.InverseLerp(0, maxHiddenObjectToFound, totalHiddenObjectsFound);





        }
        else
        {
            Debug.Log("You won the game");                      //if yes then we have won the game
                                                                //UIManagerScav.instance.GameCompleteObj.SetActive(true); //activate GameComplete panel
            PlayerPrefs.DeleteAll();
            GameManager.instance.levelCompleteManager.LevelComplete(1);


            //  gameStatus = GameStatus.NEXT;

        }



    }





    public Vector3 HitPos;


    public IEnumerator HintObject() //Method called by HintButton of UIManager
    {
        int randomValue = UnityEngine.Random.Range(0, activeHiddenObjectList.Count);
        Vector3 originalScale = activeHiddenObjectList[randomValue].transform.localScale;
        activeHiddenObjectList[randomValue].transform.localScale = originalScale * 1.25f;
        yield return new WaitForSeconds(0.25f);
        activeHiddenObjectList[randomValue].transform.localScale = originalScale;
    }





    public void OnPickItemLM(RaycastHit hitInfo)
    {

        if (gameStatus == GameStatus.PLAYING)
        {
            GameObject go = hitInfo.collider.gameObject;


            if (go.CompareTag("HidddenObject"))
            {


                go.tag = "Untagged";

                GameObject clickprt = Instantiate(UIManagerScav.instance.ClickParticlePrefab, go.transform);
                clickprt.transform.localPosition = Vector3.zero;



                CheckSelectedHiddenObject(go.transform, hitInfo.point);


                for (int i = 0; i < activeHiddenObjectList.Count; i++)
                {
                    if (activeHiddenObjectList[i].name == go.name)
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


                    CheckNextArea();


                }


            }
            else
            {


                showCross(HitPos);



            }


        }
    }







    public void CheckSelectedHiddenObject(Transform objtrans, Vector3 pos)
    {
        Vector3 SvPos = Cam.Cam.WorldToScreenPoint(UIManagerScav.instance.scrollRect.transform.position);

        objtrans.transform.DOScale(Vector3.one * 2, 0.2f).OnComplete(() =>
        {

            objtrans.transform.DOMove(SvPos, 5).OnComplete(() =>
            {
                objtrans.gameObject.SetActive(false);


            });

        });




        string SelectedObjName = objtrans.gameObject.name;


        for (int i = 0; i < UIManagerScav.instance.SV_IconList.Count; i++) //loop through the list
        {
            if (SelectedObjName == UIManagerScav.instance.SV_IconList[i].name)      //check if index is same as name [our name is a number]
            {



                string id = objtrans.parent.name + "_" + objtrans.name + objtrans.GetSiblingIndex() + "_IsTaken";
                PlayerPrefs.SetInt((id), 1);


                UIManagerScav.instance.Sv_fillText.text = 0 + "/" + 0;


                string IconName = UIManagerScav.instance.SV_IconList[i].name;
                PlayerPrefs.SetInt((IconName + "Collected"), PlayerPrefs.GetInt((IconName + "Collected"), 0) + 1);

               // Debug.Log(" count " + PlayerPrefs.GetInt((IconName + "Collected"), PlayerPrefs.GetInt((IconName + "Collected"))));

                UIManagerScav.instance.infoImg.GetComponent<InfoImage>().SetInfovalues(" count " + PlayerPrefs.GetInt((IconName + "Collected"),
                    PlayerPrefs.GetInt((IconName + "Collected"))) +
                    "/" + UIManagerScav.instance.SV_IconList[i].TotalObjects, UIManagerScav.instance.SV_IconList[i].childImg.sprite);



                GetSnapToPositionToBringChildIntoView(UIManagerScav.instance.SV_IconList[i].GetComponent<RectTransform>());


                HitPos.y += 100;
                UIManagerScav.instance.infoImg.transform.position = HitPos;
                UIManagerScav.instance.infoImg.gameObject.SetActive(true);
                UIManagerScav.instance.SV_IconList[i].updateCollectedText();




                break;
            }
        }
    }

    public void GetSnapToPositionToBringChildIntoView(RectTransform child)
    {
        RectTransform contentRt = UIManagerScav.instance.scrollRect.content;


        Debug.Log("changing SV position");
        Canvas.ForceUpdateCanvases();
        Vector2 viewportLocalPosition = UIManagerScav.instance.scrollRect.viewport.localPosition;
        Vector2 childLocalPosition = child.localPosition;
        Vector2 result = new Vector2(0 - (viewportLocalPosition.x + childLocalPosition.x),0 - (viewportLocalPosition.y + childLocalPosition.y));

        contentRt.DOLocalMove(new Vector2(result.x, contentRt.localPosition.y), 0.2f).SetEase(Ease.Linear);
        
    }








    public void showCross(Vector3 poss)
    {
        UIManagerScav.instance.crossImg.gameObject.SetActive(true);
        UIManagerScav.instance.crossImg.transform.position = poss;

    }



    private void OnEnable()
    {
        Cam.GetComponent<TouchInputController>().OnInputClick += OnInputClick;
    }

    private void OnDisable()
    {
       // Cam.GetComponent<TouchInputController>().OnInputClick -= OnInputClick;
    }


    private void OnInputClick(Vector3 clickScreenPosition, bool isDoubleClick, bool isLongTap)
    {

        HitPos = clickScreenPosition;

    }





}









