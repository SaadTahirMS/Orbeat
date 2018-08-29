using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MyFade : MonoBehaviour {
    public float initialFade;//since we need to retain our scale if no beat detected
    public float endValue, duration;
    Sequence s;
    Image img;
    private void Start()
    {
        s = DOTween.Sequence();
        img = gameObject.GetComponent<Image>();
    }
    public void DoFade(float value)
    {
        s.Append(img.DOFade(initialFade + value * endValue, duration).SetLoops(2, LoopType.Yoyo));       
        s.Play();
    }
}
