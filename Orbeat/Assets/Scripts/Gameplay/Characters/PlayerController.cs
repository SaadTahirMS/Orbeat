using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerController: MonoBehaviour {
    
    public Transform player;
    public Transform orbit;
    public Rigidbody2D playerRB; //used for collisions turn on off
    //public Rigidbody2D playerOrbitRB; //used for collisions turn on off

    //public Slider slider;

    private Vector3 position;
    public Vector3 Position
    {
        get
        {
            return position;
        }
        set
        {
            position = value;
        }
    }

    private float rotationSpeed;
    public float RotationSpeed
    {
        get
        {
            return rotationSpeed;
        }
        set
        {
            rotationSpeed = value;    
        }

    }

    public void Initialize()
    {
        AssignPosition();
        AssignRotationSpeed();
    }

    private void AssignPosition(){
        Position = Constants.playerInitialPosition;
    }

    private void AssignRotationSpeed(){
        RotationSpeed = Constants.playerRotationSpeed;
    }

    public void ChangeState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.SetActive(true);
                break;
            case GameState.Restart:
                break;
            case GameState.Quit:
                break;
        }
    }

    ////Slider control
    //public void RotatePlayerSlider()
    //{
    //    player.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -slider.value * 360f));
    //}


    public void RotatePlayerV1(float deltaPosition)
    {
        //print(value);
        player.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -deltaPosition * 360f));

    }

    public void RotatePlayerV2(float deltaPosition)
    {
        player.Rotate(Vector3.back * Time.deltaTime * deltaPosition * Constants.playerScrollRotationSpeed);
    }

    public void MoveLeft(){
        player.Rotate(Vector3.forward * RotationSpeed);
    }

    public void MoveRight(){
        player.Rotate(Vector3.back * RotationSpeed);
    }

    public void MoveTapLeft()
    {
        player.Rotate(Vector3.forward * Constants.playerTapRotateSpeed);
    }

    public void MoveTapRight()
    {
        player.Rotate(Vector3.back * Constants.playerTapRotateSpeed);
    }

    public void SetCollisions(bool state)
    {
        playerRB.isKinematic = state; 
        //playerOrbitRB.isKinematic = !state; 
    }



}
