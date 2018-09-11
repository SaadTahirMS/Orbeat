using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HurdleController : MonoBehaviour {

    public ArcCollider2D arcCollider;
    public EdgeCollider2D edgeCollider;
    public Image hurdleImage;

    public void SetFillAmount(float fillAmount , float duration){
        hurdleImage.DOFillAmount(fillAmount, duration);
        arcCollider.totalAngle = (int)(fillAmount * 360);
        edgeCollider.points = arcCollider.getPoints(edgeCollider.offset);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                GameplayContoller.Instance.PlayerHitHurdle();
                break;
            case "Wall":
                GameplayContoller.Instance.HurdleHitWall();
                break;
        }
    }


}
