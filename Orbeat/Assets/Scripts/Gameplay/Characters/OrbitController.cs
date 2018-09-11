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

    public void Initialize(HurdleController hurdleController)
    {
        this.hurdleController = hurdleController;
    }

    public Tween DoScale(Vector3 endValue, float duration){
        scaleSequence = DOTween.Sequence();
        scaleSequence.Join(hurdleController.transform.DOScale(endValue, duration));
        scaleSequence.Join(inner.DOScale(endValue - Constants.hurdleWidth, duration));
        scaleSequence.SetEase(Ease.Linear);
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
        inner.transform.localScale = value - Constants.hurdleWidth;
    }

    public void DoRotate(int direction, float duration)
    {
        rotate.DoRotate(direction, duration);
    }

    public float GetHurdleScale()
    {
        return hurdleController.transform.localScale.x;
    }
}
