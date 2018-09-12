using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrbitController : MonoBehaviour {

    public Transform inner;
    public Rotate rotate;
    [HideInInspector]
    public HurdleController hurdleController;
    private Sequence scaleSequence;
    private int direction;
    private float duration;

    public void Initialize(HurdleController hurdleController)
    {
        this.hurdleController = hurdleController;
    }

    public Tween DoScale(Vector3 endValue, float duration){
       
        scaleSequence = DOTween.Sequence();
        scaleSequence.Join(hurdleController.transform.DOScale(endValue, duration).SetEase(Ease.Linear));
        scaleSequence.Join(inner.DOScale(endValue - Constants.hurdleWidth, duration).SetEase(Ease.Linear));
        return scaleSequence;
    }

    public void StopScale()
    {
        scaleSequence.Kill();
    }

    public void StopRotate()
    {
        rotate.StopRotate();
    }

    public void SetScale(Vector3 value)
    {
        StopScale();
        hurdleController.transform.localScale = value;
        inner.transform.localScale = value - Constants.hurdleWidth;
    }

    public void DoRotate(int direction, float duration)
    {
        this.duration = duration;
        this.direction = direction;
        rotate.DoRotate(direction, duration);
    }

    public float GetHurdleScale()
    {
        return hurdleController.transform.localScale.x;
    }

    public void RotationOffset(float value){
        StopRotate();
        gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, value);
        DoRotate(direction, duration);
    }
}
