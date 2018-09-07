using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Scaler : MonoBehaviour
{
    public RectTransform target;
    public RectTransform inner;
    public RectTransform rect;
    public ArcCollider2D arcCollider;
    public EdgeCollider2D edgeCollider;

    //private void Start()
    //{
    //    rect = (RectTransform)gameObject.transform;
    //    arcCollider = target.GetComponent<ArcCollider2D>();
    //    edgeCollider = target.GetComponent<EdgeCollider2D>();
    //}

    //private void Update()
    //{
    //    target.sizeDelta = rect.sizeDelta;
    //    inner.sizeDelta = rect.sizeDelta - Vector2.one * 100f;
    //    arcCollider.radius = rect.sizeDelta.x / 2f;
    //    edgeCollider.points = arcCollider.getPoints(edgeCollider.offset);
    //}

    //public void SetCollider()
    //{
    //    arcCollider.radius = rect.sizeDelta.x / 2f - 50f;
    //    edgeCollider.points = arcCollider.getPoints(edgeCollider.offset);
    //}


}
