using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Scaler : MonoBehaviour {

    public Tween DoScale(Vector3 value){
        return transform.DOScale(value, Constants.transitionTime);
    }

    private void Update()
    {
        if (transform.localScale.x <= Constants.orbitScaleThreshold)
            gameObject.SetActive(false);
    }
}
