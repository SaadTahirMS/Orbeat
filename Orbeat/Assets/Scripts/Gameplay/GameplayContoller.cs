using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayContoller : Singleton<GameplayContoller>, IController
{

    public GameObject player;
    PlayerController playerController;

    //gameplay variables
    public float playerRotateSpeed = 100f;
    public float playerMovementSpeed = 10f; 
    public int playerDirection = 1; //1 means right
    public float playerShootSpeed = 500f;

    bool canShootFlag = false;

    public void Open()
    {
        InitializePlayer();
        InitializePlayerRotation();
        InitializePlayerMovement();
        AllowToShoot();
    }

    void InitializePlayer(){
        player.SetActive(true);
        playerController = player.GetComponent<PlayerController>();
    }

    //Rotation around the axis
    void InitializePlayerRotation(){
        playerController.StartRotation(playerRotateSpeed,playerDirection);
    }

    //Movement towards the center
    void InitializePlayerMovement(){
        playerController.StartMovement(playerMovementSpeed);
    }

    void AllowToShoot(){
        canShootFlag = true;
    }

    //Shooting the player
    void InitializeShoot(){
        playerController.StartShoot(playerShootSpeed);
    }


    void Update(){
        if(Input.GetMouseButtonDown(0) && canShootFlag){
            InitializeShoot();
        }
    }




   
}
