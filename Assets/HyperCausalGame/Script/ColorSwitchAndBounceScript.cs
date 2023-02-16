using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class ModelMeshData
{
    [Header("Mesh Renderer")]
    public Renderer meshrenderer;
    [Header("Model Materials")]
    public Material[] modelMaterial;
    [Header("Other Temp Materials")]
    public Material[] OtherMaterial;
}

public class ColorSwitchAndBounceScript : MonoBehaviour
{
    #region Variables
    [Header("Scale Up Down Data")]
    public Vector3 ObjectNewScale = new Vector3(1.25f, 1.5f, 1.25f);
    public Vector3 ObjectDefaultScale = new Vector3(1, 1, 1);
    public float ScaleUpDownDuration = 0.15f;
    [Header("ModelObject")]
    public GameObject ObjectMesh;
    [Header("Particle Object")]
    public GameObject ParticleObject;
    [Header("Material Switch Duration")]
    public float SwitchDelay = 0.25f;
    [Header("Model Mesh Data")]
    public List<ModelMeshData> modelmeshdata = new List<ModelMeshData>();
    [Header("Effect Color")]
    public Color color1 = Color.white;
    [Header("Color Change Duration")]
    public float duration = 0.35f;
    [Header("Upgrade Particle Area")]
    public int TotalParticleSpawn = 5;
    public List<GameObject> UpgradeParticleList = new List<GameObject>();
    int ParticleCount = 0;
    bool StartWorking = false;


    #endregion

    #region Awake Function
    private void Awake()
    {
        for (int i = 0; i < TotalParticleSpawn; i++)
        {
            GameObject obj = Instantiate(ParticleObject, ParticleObject.transform.position, ParticleObject.transform.rotation, transform);
            obj.SetActive(false);
            UpgradeParticleList.Add(obj);
        }
        StartCoroutine(TimeDelay());
    }

    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(1f);
        StartWorking = true;
    }
    #endregion

    #region Color_Change_Function
    public void ColorChangeFunction()
    {
        if(!StartWorking)
        {
            return;
        }
        //ParticleObject.SetActive(true);
        for (int i = 0; i < modelmeshdata.Count; i++)
        {
            modelmeshdata[i].meshrenderer.sharedMaterials = modelmeshdata[i].OtherMaterial;
        }
        StartCoroutine(ScaleTimeDelay());
        StartCoroutine(MaterialSwitcchTimeDelay());
        StopCoroutine("ColorChangeTimeDelay");
        StartCoroutine("ColorChangeTimeDelay");
    }
    #endregion

    #region Color_Change_Coroutine
    IEnumerator ColorChangeTimeDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            for (int j = 0; j < modelmeshdata.Count; j++)
            {
                for (int i = 0; i < modelmeshdata[j].OtherMaterial.Length; i++)
                {
                    var t = Mathf.PingPong(Time.time, duration) / duration;
                    modelmeshdata[j].OtherMaterial[i].color = Color.Lerp(modelmeshdata[j].modelMaterial[i].color, color1, t);
                }
            }
        }
    }
    #endregion

    #region Material_Switch_Coroutine
    IEnumerator MaterialSwitcchTimeDelay()
    {
        yield return new WaitForSeconds(SwitchDelay);
        if(UpgradeParticleList.Count > 0)
        {
            UpgradeParticleList[ParticleCount].SetActive(true);
            ParticleCount++;
            if (ParticleCount >= UpgradeParticleList.Count)
            {
                ParticleCount = 0;
            }
        }
        for (int j = 0; j < modelmeshdata.Count; j++)
        {
            modelmeshdata[j].meshrenderer.sharedMaterials = modelmeshdata[j].modelMaterial;
        }
        StopCoroutine("ColorChangeTimeDelay");
        yield return new WaitForSeconds(SwitchDelay);
    }
    #endregion

    #region Scale Coroutine
    IEnumerator ScaleTimeDelay()
    {
        ObjectMesh.transform.DOScale(ObjectNewScale, ScaleUpDownDuration);
        yield return new WaitForSeconds(ScaleUpDownDuration);
        ObjectMesh.transform.DOScale(ObjectDefaultScale, ScaleUpDownDuration);
    }
    #endregion


    public void StorageAreaParticlePositionSet(float value)
    {
        Debug.Log("Value :::: " + value);
        UpgradeParticleList[ParticleCount].transform.localPosition = new Vector3(UpgradeParticleList[ParticleCount].transform.localPosition.x, UpgradeParticleList[ParticleCount].transform.localPosition.y, -12 + (0.6944f * value));
    }
}
