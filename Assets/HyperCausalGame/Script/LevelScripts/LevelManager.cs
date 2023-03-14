using System.Collections;
using System.Collections.Generic;
using TinyK.Common;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    [HideInInspector]
    public LevelInfo CurrentLevel = null;
    public LeanConstrainToColliders ColliderContainer;
    public List<LevelInfo> Levels = new List<LevelInfo>();
    GameObject LevelParent = null;
    public bool TestLevel = false;
    public int levelnumber = 1;


    public bool allowLevelsRandomizationOrderAfterAllLevelsPlayed = true;


    public delegate void LevelCreateFunc(int Level);
    public LevelCreateFunc levelCreateFuncEvent;

    private void Awake()
    {
        instance = this;
        CreateLevelParentObject();

    }



    void CreateLevelParentObject()
    {
        LevelParent = new GameObject();
        LevelParent.transform.position = Vector3.zero;
        LevelParent.transform.eulerAngles = Vector3.zero;
        LevelParent.name = "Level Parent";
    }
    void CheckRandamizationOrderStatus()
    {
        try
        {

            LevelRandomization LR = null;
            if (!GameData.instance.LevelRandomizationOrderHasKey())
            {

                LR = new LevelRandomization();
                LR.order = new int[Levels.Count];
                for (int i = 0; i < LR.order.Length; i++)
                {
                    LR.order[i] = i;
                }
                GameData.instance.SetLevelRandomizationOrder(LR);




            }

            LR = null;
            LR = GameData.instance.GetLevelRandomizationOrder();
            List<LevelInfo> temp = new List<LevelInfo>();


            if (LR.order.Length < Levels.Count)
            {
                LR = new LevelRandomization();
                LR.order = new int[Levels.Count];
                for (int i = 0; i < LR.order.Length; i++)
                {
                    LR.order[i] = i;
                }
                GameData.instance.SetLevelRandomizationOrder(LR);
            }


            for (int x = 0; x < Levels.Count; x++)
            {
                temp.Add(Levels[x]);
            }
            for (int i = 0; i < Levels.Count; i++)
            {
                Levels[i] = temp[LR.order[i]];
                Levels[i].name = "" + LR.order[i];

            }





        }
        catch (System.Exception e)
        {
            Debug.LogError(e.ToString());

        }
    }

    public void LevelsArrayReshuffle()
    {
        if (allowLevelsRandomizationOrderAfterAllLevelsPlayed)
        {
            for (int t = 0; t < Levels.Count; t++)
            {
                LevelInfo tmp = Levels[t];
                int r = Random.Range(t, Levels.Count);
                Levels[t] = Levels[r];
                Levels[r] = tmp;
            }
        }
        LevelRandomization LR = null;
        LR = GameData.instance.GetLevelRandomizationOrder();
        string b = string.Empty;
        for (int i = 0; i < Levels.Count; i++)
        {
            b = string.Empty;
            for (int z = 0; z < Levels[i].name.Length; z++)
            {

                if (System.Char.IsDigit(Levels[i].name[z]))
                    b += Levels[i].name[z];
            }

            LR.order[i] = int.Parse(b);
        }





        GameData.instance.SetLevelRandomizationOrder(LR);
    }
    private void Start()
    {
        LevelStart();
    }

    public void LevelStart()
    {

        CheckRandamizationOrderStatus();

        int level = GameData.instance.GetLevelNumber();

        if (level < 0)
            level = 0;









        level = level - 1;

        int indexLevel = GameData.instance.GetLevelNumberIndex();



        if (indexLevel > Levels.Count)
        {
            indexLevel = Levels.Count;
            GameData.instance.SetLevelNumberIndex(Levels.Count);
        }

        indexLevel = indexLevel - 1;


        if (TestLevel)
        {
            level = levelnumber;
            indexLevel = levelnumber;
        }



        if (level < 0)
            level = 0;
        if (indexLevel < 0)
            indexLevel = 0;







        LevelInfo Prefab_ = Levels[indexLevel];
        Prefab_.gameObject.SetActive(false);
        LevelInfo LI = Instantiate(Prefab_, Vector3.zero, Quaternion.identity);
        LI.gameObject.SetActive(true);
        LI.transform.position = Vector3.zero;
        LI.transform.eulerAngles = Vector3.zero;
        LI.transform.localScale = Vector3.one;


      

        

        CurrentLevel = LI;


        LI.transform.SetParent(LevelParent.transform);
        LI.gameObject.SetActive(true);



        levelCreateFuncEvent.Invoke(level + 1);

        ColliderContainer.AssignAreaColliders(LI.GetComponent<AreaHolder>().areaColliders);


    }




}