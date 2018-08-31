using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateAngle : MonoBehaviour {

    private Image target;
    private ArcCollider2D arcCollider2D;

    private void Start()
    {
        target = GetComponent<Image>();
        arcCollider2D = GetComponent<ArcCollider2D>();
    }

    public void Initialize(int angle){
        target.fillAmount = angle / 360f;   //assign fill amount to target image
        arcCollider2D.totalAngle = angle;   //assign total angle to the arc
    }

}
