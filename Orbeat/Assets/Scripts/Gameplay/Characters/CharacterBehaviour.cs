using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class CharacterBehaviour : MonoBehaviour {
    
    //Rotation Variables
    private Vector3 rotation;
    private float rotateSpeed;
    protected int direction;
    public Rotate rotationComponent;

    public abstract float MinRotateSpeed
    {
        get;
    }

    public abstract float MaxRotateSpeed
    {
        get;
    }

    public virtual void Initialize(){
            rotationComponent.gameObject.SetActive(true);

        InitializeRotation();
    }

    private void InitializeRotation()
    {
        rotation = new Vector3(0, 0, 360);
    }


    protected void Rotate()
    {
        StopRotation();
        float ran = AssignRandomRotation();
        AssignRandomDirection();
        AssignRotateSpeed();
            rotationComponent.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, ran);
            rotationComponent.DoRotate(rotation, direction, rotateSpeed);

    }

    private float AssignRandomRotation(){
        int ran = Random.Range(0, 5);//0,90,180,270
        switch(ran){
            case 1:
                return 0f;
            case 2:
                return 90f;
            case 3:
                return 180f;
            case 4:
                return -90f;
            default:
                return -1;
        }
    }

    private void AssignRandomDirection()
    {
        direction = Random.Range(0, 2);
        direction = direction > 0 ? 1 : -1;
    }

    private void AssignRotateSpeed()
    {
        rotateSpeed = Random.Range(MinRotateSpeed, MaxRotateSpeed);
    }

   
    protected void StopRotation()
    {
        
            rotationComponent.StopRotate();
         
    }

    protected virtual void SetPosition(){}


}
