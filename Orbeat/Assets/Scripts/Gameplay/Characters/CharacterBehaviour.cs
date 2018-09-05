using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class CharacterBehaviour : MonoBehaviour {
    
    //Rotation Variables
    protected Vector3 rotation;
    protected float rotateSpeed;
    protected int direction;
    public List<Rotate> rotationComponent = new List<Rotate>();

    public abstract float MinRotateSpeed
    {
        get;
    }

    public abstract float MaxRotateSpeed
    {
        get;
    }

    public virtual void Initialize(){
        for (int i = 0; i < rotationComponent.Count;i++){
            rotationComponent[i].gameObject.SetActive(true);
        }
        InitializeRotation();
    }

    private void InitializeRotation()
    {
        rotation = new Vector3(0, 0, 360);
    }


    protected virtual void Rotate()
    {
        
        for (int i = 0; i < rotationComponent.Count; i++)
        {
            rotationComponent[i].StopRotate();
            AssignRandomDirection();
            AssignRotateSpeed();
            rotationComponent[i].DoRotate(rotation, direction, rotateSpeed);
        }
    }

    protected void AssignRandomDirection()
    {
        direction = Random.Range(0, 2);
        direction = direction > 0 ? 1 : -1;
    }

    protected void AssignRotateSpeed()
    {
        rotateSpeed = Random.Range(MinRotateSpeed, MaxRotateSpeed);
    }


   
    protected void StopRotation()
    {
        for (int i = 0; i < rotationComponent.Count; i++)
        {
            rotationComponent[i].StopRotate();
        } 
    }

    protected virtual void SetPosition(){}


}
