using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotate : MonoBehaviour {
    
    public bool canRotate = true;
    Sequence rotationSequence;
    private Vector3 rotation = new Vector3(0f, 0f, 360f);
    public void DoRotate(int direction,float duration)
    {
        if(canRotate){
            StopRotate();
            rotationSequence = DOTween.Sequence();
            rotationSequence.Append(transform.DORotate((transform.localEulerAngles + rotation * direction), duration, RotateMode.FastBeyond360).SetEase(Ease.Linear));
            rotationSequence.SetLoops(-1, LoopType.Incremental);
            rotationSequence.Play();
        }
    }

    public void StopRotate(){
        rotationSequence.Kill();
    }
}
