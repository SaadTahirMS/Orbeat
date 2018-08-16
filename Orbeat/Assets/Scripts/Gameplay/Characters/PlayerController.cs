using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerController : CharacterBehaviour {
    
    public Transform player;
    public Transform targetObj;
    public Rigidbody2D playerRb;

    //Shooting
    float shotSpeed;
    bool shotFlag = false;

    private Vector3 position;

    public Vector3 Position
    {
        get
        {
            return position;
        }
    }


    public override float MinRotateSpeed
    {
        get
        {
            return Constants.playerMinRotationSpeed;
        }
    }

    public override float MaxRotateSpeed
    {
        get
        {
            return Constants.playerMaxRotationSpeed;
        }
    }


    public override void Initialize()
    {
        base.Initialize();
        InitializeShot();
    }



    private void InitializeShot()
    {
        //float currentWidth = Screen.width;
        //float referenceWidth = Constants.referenceWidth;
        //float a = currentWidth / referenceWidth;
        //shotSpeed = Constants.playerShotSpeed * 100 * a;
        //Constants.playerShotSpeed = shotSpeed;
        shotSpeed = Constants.playerShotSpeed;
    }

    public void ChangeState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                SetCollisions(false); //this will be turned on after transition
                ResetParent();
                SetPosition();
                Rotate();
                break;
            case GameState.Shot:
                InitiateShot();
                break;
            case GameState.End:
                StopRotation();
                shotFlag = false;
                break;            
        }

    }

    protected override void SetPosition(){
        position = AssignPosition();
    }

    private Vector3 AssignPosition()
    {
        return Constants.playerInitialPosition;
    }

    void InitiateShot(){
        transform.SetParent(player.transform.parent);
        StopRotation();
        shotFlag = true;
    }

    private void Update()
    {
        if (shotFlag)
        {
            transform.Translate(Vector3.right * Time.deltaTime * shotSpeed);
            CollisionWithBoundary();
        }
    }



    private void ResetParent(){
        transform.SetParent(player);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            shotFlag = false;
            SetCollisions(false);
            GameplayContoller.Instance.PlayerCollidedWithTarget(CheckPerfectHit());
        }
        else if (collision.gameObject.tag == "Timer")
        {
            SetCollisions(false);
            GameplayContoller.Instance.PlayerCollidedWithTimer();
        }
        else if (collision.gameObject.tag == "Warning")
        {
            GameplayContoller.Instance.TimerWarning();
        }
        else if (collision.gameObject.tag == "Hurdle")
        {
            GameplayContoller.Instance.PlayerCollidedWithHurdle();
        }
    }

    public void SetCollisions(bool state)
    {
        playerRb.isKinematic = !state; //inversed just for understanding of function
    }

    private bool CheckPerfectHit(){
      float angleDifference = targetObj.eulerAngles.z - player.eulerAngles.z;
        if(angleDifference >= -Constants.perfectHitThreshold && angleDifference <= Constants.perfectHitThreshold){
            print("Perfect Hit");
            return true;
        }
        return false;
    }

    private void CollisionWithBoundary(){
        //Vector3 position = Camera.main.ScreenToViewportPoint(transform.position);
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1)
        {
            SetCollisions(false);
            GameplayContoller.Instance.PlayerCollidedWithBoundary();
        }
    }


}
