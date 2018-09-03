using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Scaler : MonoBehaviour {

    public List<RectTransform> childs;
    private RectTransform rect;

    private void Start()
    {
        rect = (RectTransform)gameObject.transform;
    }

    private void Update()
    {
        for (int i = 0; i < childs.Count;i++){
            childs[i].sizeDelta = rect.sizeDelta - Constants.orbitsDistance;
        }
    }
}
