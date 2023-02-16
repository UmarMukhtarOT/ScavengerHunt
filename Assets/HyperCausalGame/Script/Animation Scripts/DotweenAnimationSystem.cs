using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DotweenAnimationSystem : MonoBehaviour
{
    [Header("Loop count '-1' make loop infinte")]
    public Vector3 Movement;
    public Vector3 Rotation_;
    public Vector3 Scale;

    public Ease easePosition = Ease.Linear;
    public bool playOnEnable = true;
    public bool enablePositionAnimation = false;
    public bool localPosition = false;
    public float positionSpeed = 1f;
    public int positionLoop = 0;
    public LoopType positionLoopType = LoopType.Yoyo;
    public float positionDelay = 0f;
    public bool enableRotationAnimation = false;
    public Ease easeRotation = Ease.Linear;
    public bool localRotation = false;
    public int rotationLoop = 0;


    public float rotationSpeed = 1f;
    public LoopType rotationLoopType = LoopType.Yoyo;
    public float rotationDelay = 0f;



    public bool enableScaleAnimation = false;
    public Ease easeScale = Ease.Linear;
    public LoopType scaleLoopType = LoopType.Yoyo;
    public float scaleDelay = 0f;
    public float scaleSpeed = 1f;
    public int scaleLoop = 0;
    private void OnEnable()
    {
        if (playOnEnable)
            PlayAnimation();
    }

    public void PlayAnimation()
    {
        if (enablePositionAnimation)
        {
            if (localPosition)
                this.transform.DOLocalMove(Movement, positionSpeed).SetEase(easePosition).SetLoops(positionLoop, positionLoopType).SetDelay(positionDelay);
            else
                this.transform.DOMove(Movement, positionSpeed).SetEase(easePosition).SetLoops(positionLoop, positionLoopType).SetDelay(positionDelay);
        }
        if (enableRotationAnimation)
        {
            if (localRotation)
                this.transform.DOLocalRotate(Rotation_, rotationSpeed).SetEase(easeRotation).SetLoops(rotationLoop, rotationLoopType).SetDelay(rotationDelay);
            else
                this.transform.DORotate(Rotation_, rotationSpeed).SetEase(easeRotation).SetLoops(rotationLoop, rotationLoopType).SetDelay(rotationDelay);
        }


        if (enableScaleAnimation)
        {

            this.transform.DOScale(Scale, scaleSpeed).SetEase(easeScale).SetLoops(scaleLoop, rotationLoopType).SetDelay(scaleDelay);

        }
    }
    public void StopAnimation()
    {

        this.transform.DOPause();


    }
    private void OnDestroy()
    {
        StopAnimation();
    }
    private void OnDisable()
    {
        StopAnimation();
    }
}