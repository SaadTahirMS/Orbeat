using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerController : CharacterBehaviour {
    
    public Transform player;
    //public List<Transform> targetsObj;
    public Rigidbody2D playerRb;
    Vector3 playerPos;//this is used by gameplayController for transitions
    Quaternion playerRot;
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
                StopRotation();
                break;
            case GameState.TargetHit:
                shotFlag = false;
                SetCollisions(false);
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
        playerPos = transform.localPosition;
        playerRot = transform.localRotation;
        shotFlag = true;
    }

    private void FixedUpdate()
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
        switch (collision.gameObject.tag)
        {
            //case "Target":
                //shotFlag = false;
                //SetCollisions(false);
                //GameplayContoller.Instance.PlayerCollidedWithTarget();
                //break;
            case "Timer":
                SetCollisions(false);
                GameplayContoller.Instance.PlayerCollidedWithTimer();
                break;
            case "Warning":
                GameplayContoller.Instance.TimerWarning();
                break;
        }
    }

    public void SetCollisions(bool state)
    {
        playerRb.isKinematic = !state; //inversed just for understanding of function
    }

    //private bool CheckPerfectHit(int index){
    //  float angleDifference = targetsObj[index].eulerAngles.z - player.eulerAngles.z;
    //    if(angleDifference >= -Constants.perfectHitThreshold && angleDifference <= Constants.perfectHitThreshold){
    //        print("Perfect Hit");
    //        return true;
    //    }
    //    return false;
    //}

    private void CollisionWithBoundary(){
        //Vector3 position = Camera.main.ScreenToViewportPoint(transform.position);
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < (0 - Constants.boundaryOffset) || pos.x > (1+Constants.boundaryOffset) || pos.y < (0-Constants.boundaryOffset) || pos.y > (1+Constants.boundaryOffset))
        {
            SetCollisions(false);
            GameplayContoller.Instance.PlayerCollidedWithBoundary();
        }
    }

    public Vector3 GetPosition(){
        return playerPos;
    }

    public Quaternion GetRotation(){
        return playerRot;
    }


}
