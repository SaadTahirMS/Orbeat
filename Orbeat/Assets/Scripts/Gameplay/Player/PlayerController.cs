using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject playerObj;
    public Transform gameplayObj;

    //Rotation Variables
    Vector3 rotation;
    bool rotateFlag = false;
    float rotateSpeed;
    int direction;

    //Movement (to center) Variables
    Vector3 movement;
    bool moveFlag = false;
    float moveSpeed;

    //Shooting
    Vector3 shootMovement;
    bool shootFlag = false;
    float shootSpeed;
   
    //This rotates the Player around its axis with speed and distance from center (0,0)
    public void StartRotation(float speed,int direction){
        rotation = new Vector3(0, 0, -1);
        rotateSpeed = speed;
        this.direction = direction;
        rotateFlag = true;
    }

    void Rotation(){
        transform.Rotate(rotation * direction * Time.deltaTime * rotateSpeed); //rotates to right
    }

    //moves the playerObj towards the axis of rotation
    public void StartMovement(float speed){
        movement = new Vector3(-1, 0, 0);
        moveSpeed = speed;
        moveFlag = true;
    }

    void Movement(){
        playerObj.transform.Translate(movement * Time.deltaTime * moveSpeed); //moves playerObj towards the axis
    }

    //Shoots the playerObj in its relative direction
    public void StartShoot(float speed){
        shootSpeed = speed;
        shootMovement = new Vector3(1, 0, 0);
        playerObj.transform.parent = gameplayObj;

        moveFlag = false;
        rotateFlag = false;
        shootFlag = true;
    }

    void Shoot(){
        playerObj.transform.Translate(shootMovement * Time.deltaTime * shootSpeed);
    }

    private void Update()
    {
        if(rotateFlag){
            Rotation();
        }
        if(moveFlag){
            Movement();
        }
        if(shootFlag){
            Shoot();
        }
    }

}
