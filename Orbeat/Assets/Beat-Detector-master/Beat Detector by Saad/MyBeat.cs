using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyBeat : MonoBehaviour
{
    public bool canBeat = true;
    public float initialScale = 1f;//since we need to retain our scale if no beat detected
    public float endValue, duration;
    Sequence s;

    private void Start()
    {
        s = DOTween.Sequence();
    }
    public void DoBeat(float value)
    {
        if(canBeat){
            s.Append(transform.DOScale(initialScale + value * endValue, duration).SetLoops(2, LoopType.Yoyo));
            s.Play(); 
        }
    }
    public void StopBeat(){
        s.Kill();
    }

}