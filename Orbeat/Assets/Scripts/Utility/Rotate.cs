using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotate : MonoBehaviour {
    
    public bool canRotate;
    Sequence rotationSequence;
    private Vector3 rotation = new Vector3(0f, 0f, 360f);

    public int direction;
    [HideInInspector]
    public float speed;
    //public void DoRotate(int direction,float duration)
    //{
    //    if(canRotate){
    //        StopRotate();
    //        rotationSequence = DOTween.Sequence();
    //        rotationSequence.Append(transform.DORotate((transform.localEulerAngles + rotation * direction), duration, RotateMode.FastBeyond360).SetEase(Ease.Linear));
    //        rotationSequence.SetLoops(-1, LoopType.Incremental);
    //        rotationSequence.Play();
    //    }
    //}

    public void DoRotate(int direction,float speed){
        this.direction = direction;
        this.speed = speed;
    }

    private void Update()
    {
        if(canRotate)
            gameObject.transform.Rotate(Vector3.forward * direction * speed);
    }


}
