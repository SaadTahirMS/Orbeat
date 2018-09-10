using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerController: MonoBehaviour {
    
    public Transform player;
    public Transform orbit;
    public Rigidbody2D rb2d; //used for collisions turn on off
    public Slider slider;

    private bool gameStart = false;
    private float screenCenterX; //for left right touch movement
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
        // save the horizontal center of the screen
        screenCenterX = Screen.width * 0.5f;

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
                gameStart = true;
                break;
            case GameState.End:
                break;            
        }
    }

    //Slider control
    public void RotatePlayer()
    {
        player.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -slider.value * 360f));
    }

    //Keyboard control
    private void Update()
    {
        if(gameStart){
            //For Keyboard
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight();
            }

        }

    }

    private void MoveLeft(){
        player.Rotate(Vector3.forward * RotationSpeed);
    }

    private void MoveRight(){
        player.Rotate(Vector3.back * RotationSpeed);
    }

    public void SetCollisions(bool state)
    {
        rb2d.isKinematic = !state; //inversed just for understanding of function
    }

}
