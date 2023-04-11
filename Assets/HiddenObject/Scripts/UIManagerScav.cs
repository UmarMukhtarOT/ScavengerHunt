using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
//using static GameManager;

public class UIManagerScav : MonoBehaviour
{
    public static UIManagerScav instance;


    public Image crossImg;
    public Image infoImg;

    [SerializeField] public GameObject hiddenObjectIconHolder;     //reference to Icon Holder object
    [SerializeField] private SV_ObjectIcon hiddenObjectIconPrefab;     //reference to Icon prefab
    [SerializeField] private GameObject gameCompleteObj;            //reference to GameComplete panel
    [SerializeField] private Text timerText;                        //reference to time text
    
    [HideInInspector]
    public List<SV_ObjectIcon> SV_IconList;                  //list to store Icons of active hidden objects

    public Text Sv_fillText;
    public Image Sv_FillBar;
    public Animator SvAnim;
    public Transform[] AnimatedImage;
    public GameObject FlyingGem;
    public GameObject GemCounter;

    


    public GameObject GameCompleteObj { get => gameCompleteObj; }
    public Text TimerText { get => timerText; }
    public GameObject SVUpBtn, SVDownBtn;
    public Canvas ScavCanv;
    public GameObject ClickParticlePrefab;
    public contentViewHandler ContentViewHandler;
    public GameObject AppericiateWithAdpanel;

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

        SV_IconList = new List<SV_ObjectIcon>();              //initialize the list
    }


    private void Start()
    {
       // 
    }

    public void PopulateHiddenObjectIcons(List<Transform> AreaObjects)
    {

        SV_IconList.Clear();
        for (int i = 0; i < AreaObjects.Count; i++)
        {


            if (!SV_IconList.Exists(x => x.name == AreaObjects[i].name))
            {
                SV_ObjectIcon icon = Instantiate(hiddenObjectIconPrefab, hiddenObjectIconHolder.transform);
                icon.setIconProperties(AreaObjects[i].name, AreaObjects[i].GetComponent<SpriteRenderer>().sprite);
                SV_IconList.Add(icon);


            }


            int matchingIndex = SV_IconList.FindIndex(x => x.name == AreaObjects[i].name);
            if (matchingIndex >= 0)
            {
                // A matching SV_ObjectIcon was found at index matchingIndex
                SV_IconList[matchingIndex].TotalObjects++;
                SV_IconList[matchingIndex].updateCollectedText();

            }



        }


    }








    ///// <summary>
    ///// Method called when the player tap on active hidden object
    ///// </summary>
    ///// <param name="index">Name of hidden object</param>
    //public void CheckSelectedHiddenObject(Transform objtrans)
    //{

    //    string SelectedObjName = objtrans.gameObject.name;
    //    for (int i = 0; i < SV_IconList.Count; i++) //loop through the list
    //    {
    //        if (SelectedObjName == SV_IconList[i].name)      //check if index is same as name [our name is a number]
    //        {


    //           // ScrollToTarget(SV_IconList[i].GetComponent<Transform>());
    //           // Debug.Log("Found a Match "+ SelectedObjName);
    //            string id = objtrans.parent.name + "_" + objtrans.name + objtrans.GetSiblingIndex() + "_IsTaken";

    //            PlayerPrefs.SetInt((id), 1);
    //          //  AutoScroller.SnapToElement(SV_IconList[0].GetComponent<RectTransform>());

    //            Sv_fillText.text = 0+"/" +0;

              
    //            string IconName = SV_IconList[i].name;
    //            PlayerPrefs.SetInt((IconName + "Collected"), PlayerPrefs.GetInt((IconName + "Collected"), 0) + 1);




    //            SV_IconList[i].updateCollectedText();

               


    //            break;
    //        }
    //    }
    //}





    public void NextButton() //Method called when NextButton is clicked
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       //load the scene
    }


    public void DropDownTheSV()
    {

        Debug.Log("sv down ");

        SvAnim.SetTrigger("Down");
        SVUpBtn.SetActive(true);
        SVDownBtn.SetActive(false);
    }


    public void UpTheSV()
    {

        Debug.Log("sv up ");

        SvAnim.SetTrigger("UP");
        SVUpBtn.SetActive(false);
        SVDownBtn.SetActive(true);


    }



    [SerializeField]
    public ScrollRect scrollRect; //your scroll rect component
    [SerializeField]
    RectTransform _container; //content transform of the scrollrect




    public float scrollSpeed = 5f;

    public void ScrollToTarget(Transform targetPosition)
    {
        if (scrollRect == null || targetPosition == null)
            return;
        Vector2 targetPos = new Vector2(targetPosition.localPosition.x, targetPosition.localPosition.y);
        StartCoroutine(ScrollTo(targetPos));
    }

    private IEnumerator ScrollTo(Vector2 targetPos)
    {
        while (Vector2.Distance(scrollRect.content.localPosition, targetPos) > 0.01f)
        {
            scrollRect.content.localPosition = Vector2.Lerp(scrollRect.content.localPosition, targetPos, Time.deltaTime * scrollSpeed);

            yield return null;
        }

        // Once we're done scrolling, make sure we snap to the target position
        scrollRect.content.localPosition = targetPos;
    }

    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }


    public void HintButton()
    {
        //TODO: Using Coroutine is not recommended, try using TweenEngine. Eg:- DOtween, iTween
        StartCoroutine(LevelManagerScav.instance.HintObject());
    }
}
