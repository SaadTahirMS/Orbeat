using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Scaler : MonoBehaviour {

    public Tween DoScale(Vector3 value){
        return transform.DOScale(value, Constants.transitionTime);
    }

    public void DoHeightWidth(float height, float width){
        RectTransform rt = (RectTransform)transform;
        //Mathf.Lerp(rt.rect.height, height, Constants.transitionTime);
        //Mathf.Lerp(rt.rect.width, width, Constants.transitionTime);
        float currentHeight = rt.rect.height;
        float currentWidth = rt.rect.width;
        // Tween a float called currentHeight to height in transition time
        DOTween.To(() => currentHeight, x => currentHeight = x, height, Constants.transitionTime);
        DOTween.To(() => currentWidth, x => currentWidth = x, width, Constants.transitionTime);

    }

    private void Update()
    {
        if (transform.localScale.x <= Constants.orbitScaleThreshold)
            gameObject.SetActive(false);
    }
}
