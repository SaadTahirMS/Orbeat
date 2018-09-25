using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HurdleController : MonoBehaviour {

    public ArcCollider2D arcCollider;
    public EdgeCollider2D edgeCollider;
    public Image hurdleImage;
    private float fillAmount;

    public void SetFillAmount(float fillAmount , float duration){
        //hurdleImage.fillAmount = 0f;
        this.fillAmount = fillAmount;
        hurdleImage.DOFillAmount(fillAmount, duration);
        arcCollider.totalAngle = (int)(fillAmount * 360);
        edgeCollider.points = arcCollider.getPoints(edgeCollider.offset);
    }

    public void SetFillAmountWithTime(float fillAmount, float changeDuration)
    {
        print("SetFillAmountWithTime");
        //hurdleImage.fillAmount = 0f;
        this.fillAmount = fillAmount;
        hurdleImage.DOFillAmount(fillAmount, 0.1f);
        arcCollider.totalAngle = (int)(fillAmount * 360);
        edgeCollider.points = arcCollider.getPoints(edgeCollider.offset);
        StartCoroutine(ChangeFillAmountv2(changeDuration));
    }

    public void ChangeFillAmount(){
        //print("Changing fill amount");
        float value = fillAmount - 0.5f;
        hurdleImage.DOFillAmount(value, 0.25f);//.SetLoops(2,LoopType.Yoyo);
        arcCollider.totalAngle = (int)(value * 360);
        edgeCollider.points = arcCollider.getPoints(edgeCollider.offset);
    }

    IEnumerator ChangeFillAmountv2(float changeDuration)
    {
        //print("Changing fill amount");
        print("ChangeFillAmountv2");
        yield return new WaitForSeconds(changeDuration);
        float value = fillAmount - 0.5f;
        hurdleImage.DOFillAmount(value, 0.35f);//.SetLoops(2,LoopType.Yoyo);
        arcCollider.totalAngle = (int)(value * 360);
        edgeCollider.points = arcCollider.getPoints(edgeCollider.offset);
        print("Changed");
    }

    public float GetFillAmount(){
        return fillAmount;
    }

    public void StartFade(){
        hurdleImage.DOFade(0, Constants.hurdleFadeTime).SetLoops(6, LoopType.Yoyo);
    }

    public void ResetFade(){
        //hurdleImage.DOKill();
        hurdleImage.DOFade(1, 0.1f);
    }
}
