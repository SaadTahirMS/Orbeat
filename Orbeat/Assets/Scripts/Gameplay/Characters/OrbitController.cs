﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrbitController : MonoBehaviour {

    [HideInInspector]
    public int id;

    public Transform inner;
    public Rotate rotate;
    [HideInInspector]
    public HurdleController hurdleController;
    private Sequence scaleSequence;
    private int direction;
    private float duration;

    public void Initialize(HurdleController hurdleController,int id)
    {
        this.hurdleController = hurdleController;
        this.id = id;
    }

    public Tween DoScale(Vector3 endValue, float duration){
        StopScale();
        scaleSequence = DOTween.Sequence();
        scaleSequence.Join(hurdleController.transform.DOScale(endValue, duration).SetEase(Ease.Linear));
        scaleSequence.Join(inner.DOScale(endValue - Constants.hurdleWidth, duration).SetEase(Ease.Linear));
        return scaleSequence;
    }

    public void StopScale()
    {
        scaleSequence.Kill();
    }


    public void SetScale(Vector3 value)
    {
        StopScale();
        hurdleController.transform.localScale = value;
        Vector3 scale = hurdleController.transform.localScale - Vector3.one;
        inner.transform.localScale = scale;

        //hurdleController.transform.DOScale(value,0.1f);
        //inner.transform.DOScale(hurdleController.transform.localScale - Constants.hurdleWidth, 0.1f);
    }

    public void DoRotate(int direction, float duration)
    {
        this.duration = duration;
        this.direction = direction;
        rotate.DoRotate(direction, duration);
    }

    public void ChangeDirection(){
        rotate.direction = -1 * direction;
    }

    public Vector3 GetHurdleScale()
    {
        return hurdleController.transform.localScale;
    }

    public void RotationOffset(float value){
        //StopRotate();
        gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, value);
        DoRotate(direction, duration);
    }
}
