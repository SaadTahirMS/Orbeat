using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyBeat : MonoBehaviour
{
    public float endValue, duration;
    Sequence s;
    private void Start()
    {
        s = DOTween.Sequence();
    }
    public void DoBeat(float value)
    {
        s.Kill();
        s.Append(transform.DOScale(value * endValue, duration).SetLoops(2, LoopType.Yoyo));
        s.Play();
    }

}
