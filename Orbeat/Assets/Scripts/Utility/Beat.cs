using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Beat : MonoBehaviour {
    
    public bool canBeat = true;
    Sequence beatSequence;

    public void DoBeat(Vector3 from,float beatScale,float duration,int sign){
        if(canBeat){
            StopBeat();
            beatSequence = DOTween.Sequence();
            transform.localScale = from; //reset the scale
            beatSequence.Append(transform.DOScaleX(transform.localScale.x + beatScale * sign, duration));
            beatSequence.Join(transform.DOScaleY(transform.localScale.y + beatScale * sign, duration));
            beatSequence.SetLoops(-1, LoopType.Yoyo);
            beatSequence.SetEase(Ease.Linear);
            beatSequence.Play();
        }
    }

    public void StopBeat(){
        beatSequence.Kill();
    }

}
