using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class CharacterBehaviour : MonoBehaviour {
    
    //Rotation Variables
    private Vector3 rotation;
    private float rotateSpeed;
    private int direction;
    public Rotate rotationComponent;

    //Zoom Variables

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
        AssignRandomDirection();
        AssignRotateSpeed();
        rotationComponent.DoRotate(rotation, direction, rotateSpeed);
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


    protected void ZoomIn(){
        transform.localScale = Vector3.zero;
    }

    protected void ZoomOut(){
        transform.localScale = Vector3.one;
    }
}
