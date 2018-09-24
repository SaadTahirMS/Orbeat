using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HurdleController : MonoBehaviour {

    public ArcCollider2D arcCollider;
    public EdgeCollider2D edgeCollider;
    public Image hurdleImage;
    private bool fillAmountChangeFlag = true;
    private float fillAmount;

    public void SetFillAmount(float fillAmount , float duration){
        //hurdleImage.fillAmount = 0f;
        this.fillAmount = fillAmount;
        hurdleImage.DOFillAmount(fillAmount, duration);
        arcCollider.totalAngle = (int)(fillAmount * 360);
        edgeCollider.points = arcCollider.getPoints(edgeCollider.offset);
        fillAmountChangeFlag = true;
    }

    public void ChangeFillAmount(){
        //print("Changing fill amount");
        float value = fillAmount - 0.5f;
        hurdleImage.DOFillAmount(value, 0.25f);//.SetLoops(2,LoopType.Yoyo);
        arcCollider.totalAngle = (int)(value * 360);
        edgeCollider.points = arcCollider.getPoints(edgeCollider.offset);
    }

    public float GetFillAmount(){
        return fillAmount;
    }
}
