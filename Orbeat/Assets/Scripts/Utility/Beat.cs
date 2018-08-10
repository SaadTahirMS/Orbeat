using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Beat : MonoBehaviour {
    
    public bool canBeat = true;
    Sequence beatSequence;

    public void DoBeat(float duration,int sign){
        if(canBeat){
            StopBeat();
            beatSequence = DOTween.Sequence();
            transform.localScale = Vector3.one; //reset the scale
            beatSequence.Append(transform.DOScaleX(transform.localScale.x + Constants.beatScale * sign, duration));
            beatSequence.Join(transform.DOScaleY(transform.localScale.y + Constants.beatScale * sign, duration));
            beatSequence.SetLoops(-1, LoopType.Yoyo);
            beatSequence.Play();
        }
    }

    public void StopBeat(){
        beatSequence.Kill();
    }

}
