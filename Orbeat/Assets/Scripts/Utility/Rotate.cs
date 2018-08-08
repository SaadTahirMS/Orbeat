﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotate : MonoBehaviour {
    Sequence rotationSequence;

    public void DoRotate(Vector3 endValue,int direction,float duration)
    {
        StopRotate();
        rotationSequence = DOTween.Sequence();
        rotationSequence.Append(transform.DORotate((transform.localEulerAngles + endValue*direction) , duration, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        rotationSequence.SetLoops(-1, LoopType.Incremental);
        rotationSequence.Play();
    }

    public void StopRotate(){
        rotationSequence.Kill();
    }

}
