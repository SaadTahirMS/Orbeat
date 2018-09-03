using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Scaler : MonoBehaviour {

    public RectTransform inner;
    //public RectTransform target;
    private RectTransform rect;

    private void Start()
    {
        rect = (RectTransform)gameObject.transform;
    }

    private void Update()
    {
        //if(target!=null)
            //target.sizeDelta = rect.sizeDelta;
        inner.sizeDelta = rect.sizeDelta - Constants.orbitsDistance;
    }
}
