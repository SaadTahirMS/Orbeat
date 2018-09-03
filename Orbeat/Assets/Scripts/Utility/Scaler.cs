using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Scaler : MonoBehaviour {

    public RectTransform inner;
    private RectTransform rect;

    private void Start()
    {
        rect = (RectTransform)gameObject.transform;
    }

    private void Update()
    {
        inner.sizeDelta = rect.sizeDelta - Constants.orbitsDistance;
    }
}
