using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerScav : MonoBehaviour
{
    public static UIManagerScav instance;


    public Image crossImg;
    public Image infoImg;

    [SerializeField] private GameObject hiddenObjectIconHolder;     //reference to Icon Holder object
    [SerializeField] private SV_ObjectIcon hiddenObjectIconPrefab;     //reference to Icon prefab
    [SerializeField] private GameObject gameCompleteObj;            //reference to GameComplete panel
    [SerializeField] private Text timerText;                        //reference to time text
    private List<SV_ObjectIcon> SV_IconList;                  //list to store Icons of active hidden objects

    public Text Sv_fillText;
    public Image Sv_FillBar;
    public Animator SvAnim;


    public GameObject GameCompleteObj { get => gameCompleteObj; }
    public Text TimerText { get => timerText; }
    public GameObject SVUpBtn, SVDownBtn;




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




    public void PopulateHiddenObjectIcons(List<AreaObjectPropertiesClass> AreaObjects)
    {

        SV_IconList.Clear();
        for (int i = 0; i < AreaObjects.Count; i++)
        {


            if (!SV_IconList.Exists(x => x.name == AreaObjects[i].ObjItself.name))
            {
                SV_ObjectIcon icon = Instantiate(hiddenObjectIconPrefab, hiddenObjectIconHolder.transform);
                icon.setIconProperties(AreaObjects[i].ObjItself.name, AreaObjects[i].ObjItself.GetComponent<SpriteRenderer>().sprite);
                SV_IconList.Add(icon);


            }


            int matchingIndex = SV_IconList.FindIndex(x => x.name == AreaObjects[i].ObjItself.name);
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
    public void CheckSelectedHiddenObject(Transform objtrans)
    {

        string SelectedObjName = objtrans.gameObject.name;
        for (int i = 0; i < SV_IconList.Count; i++) //loop through the list
        {
            if (SelectedObjName == SV_IconList[i].name)      //check if index is same as name [our name is a number]
            {
                string id = objtrans.parent.name + "_" + objtrans.name + objtrans.GetSiblingIndex() + "_IsTaken";

                PlayerPrefs.SetInt((id), 1);



                string IconName = SV_IconList[i].name;
                PlayerPrefs.SetInt((IconName + "Collected"), PlayerPrefs.GetInt((IconName + "Collected"), 0) + 1);

                SV_IconList[i].updateCollectedText();





                break;
            }
        }
    }





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










    public void HintButton()
    {
        //TODO: Using Coroutine is not recommended, try using TweenEngine. Eg:- DOtween, iTween
        StartCoroutine(LevelManagerScav.instance.HintObject());
    }
}
