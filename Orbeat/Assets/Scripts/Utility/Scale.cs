using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scale : MonoBehaviour {

    public bool canScale = true;
    Sequence scale;

    public void DoScale(Vector3 initialScale)
    {
        if(canScale){
            StopScale();
            scale = DOTween.Sequence();
            transform.localScale = initialScale;
            scale.Append(gameObject.transform.DOScale(Constants.endScale, Constants.scaleSpeed))
                 .SetEase(Ease.Linear)
                 .Play()
                 .OnComplete(() => DoScale(Vector3.one));
        }
    }

    public void StopScale()
    {
        scale.Kill();
    }
}
