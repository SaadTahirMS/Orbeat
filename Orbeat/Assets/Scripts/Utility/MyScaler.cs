using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyScaler : MonoBehaviour {

    public Transform outer;
    public Transform target;
    public Transform inner;

    Sequence scaleSequence;

    public Tween DoScale(Vector3 endValue, float duration){
        scaleSequence = DOTween.Sequence();

        scaleSequence.Append(outer.DOScale(endValue, duration));
        scaleSequence.Join(target.DOScale(endValue, duration));
        scaleSequence.Join(inner.DOScale(endValue - Constants.innerOuterDistance, duration));

        scaleSequence.SetEase(Ease.Linear);
        return scaleSequence;
    }

    public void StopScale()
    {
        scaleSequence.Kill();
    }

}
