using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class CharacterBehaviour : MonoBehaviour {
    
    //Rotation Variables
    private Vector3 rotation;
    private float rotateSpeed;
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


    protected void Rotate()
    {
        StopRotation();
        AssignRandomDirection();
        AssignRotateSpeed();
        for (int i = 0; i < rotationComponent.Count;i++){
            rotationComponent[i].DoRotate(rotation, direction, rotateSpeed);
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
        for (int i = 0; i < rotationComponent.Count; i++)
        {
            rotationComponent[i].StopRotate();
        } 
    }

    protected virtual void SetPosition(){}


}
