using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerController : CharacterBehaviour {
    
    public Transform player;
    public Transform targetObj;
    public Transform playerOrbit;


    //Movement (to center) Variables
    float moveSpeed;
    Sequence movementSequence;

    //Scaling
    private float scaleSpeed;
    Sequence scaleSequence;

    //Shooting
    float shotSpeed;
    bool shotFlag = false;
    public bool isAllowedToShot = false;   //player can tap or not


    //Collision Flag
    bool isAllowedToCollide = false; //All Collisions flag 

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
        InitializeMovement();
        InitializeOrbitScale();
        InitializeShot();
    }

    private void InitializeMovement(){
        moveSpeed = Constants.playerMoveSpeed;
    }

    private void InitializeOrbitScale()
    {
        scaleSpeed = Constants.playerOrbitScaleSpeed;
    }

    private void InitializeShot()
    {
        float currentWidth = Screen.width;
        float referenceWidth = Constants.referenceWidth;
        float a = currentWidth / referenceWidth;
        shotSpeed = Constants.playerShotSpeed * 100 * a;
        Constants.playerShotSpeed = shotSpeed;
    }

    public void ChangeState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                ResetParent();
                OrbitScale();
                Position();
                Rotate();
                break;
            case GameState.Shot:
                InitiateShot();
                break;
            case GameState.End:
                StopRotation();
                StopMovement();
                StopOrbitScale();
                StopShot();
                DisableCollisions();
                Reset();
                break;
            
        }

    }

    private void Movement()
    {
        StopMovement();
        movementSequence = DOTween.Sequence();
        movementSequence.Append(transform.DOLocalMove(Vector3.zero, moveSpeed).SetEase(Ease.Linear));
        movementSequence.SetLoops(-1, LoopType.Incremental);
        movementSequence.Play();
    }

    private void OrbitScale()
    {
        StopOrbitScale();
        playerOrbit.localScale = Vector3.one;
        scaleSequence = DOTween.Sequence();
        scaleSequence.Append(playerOrbit.DOScale(Vector3.zero, scaleSpeed)).SetEase(Ease.Linear);
        scaleSequence.SetLoops(-1, LoopType.Incremental);
        scaleSequence.Play();
    }

    private void Position(){
        transform.DOLocalMove(Constants.playerInitialPosition, 1f).OnComplete(PlayerAtPosition);
    }

    //Movement OrbitScale and Shot is allowed when the player is on its orbit or position
    private void PlayerAtPosition(){
        Movement();
        Shot();
        EnableCollisions();
    }

    public void Shot()
    {
        shotFlag = false;
        isAllowedToShot = true;
    }

    private void EnableCollisions(){
        isAllowedToCollide = true;
    }

    private void DisableCollisions()
    {
        isAllowedToCollide = false;
    }

    void InitiateShot(){
        transform.SetParent(player.transform.parent);
        StopMovement();
        StopOrbitScale();
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

    private void Reset()
    {
        ResetPosition();
    }


    private void ResetPosition()
    {
        transform.DOLocalMove(Vector3.zero, 1f);
    }

    private void ResetParent(){
        transform.SetParent(player);
    }

    private void StopMovement()
    {
        movementSequence.Kill();
    }

    private void StopOrbitScale(){
        scaleSequence.Kill();
    }

    private void StopShot(){
        shotFlag = false;
        isAllowedToShot = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isAllowedToCollide){
            print("Allowed to Collide");
            if (collision.gameObject.tag == "Target")
            {
                CheckPerfectHit();
                DisableCollisions();
                StopShot();
                GameplayContoller.Instance.PlayerCollidedWithTarget();
            }
            else if (collision.gameObject.tag == "Timer")
            {
                DisableCollisions();
                GameplayContoller.Instance.PlayerCollidedWithTimer();
            }
        }
    }

    void CheckPerfectHit(){
      float angleDifference = targetObj.eulerAngles.z - player.eulerAngles.z;
        if(angleDifference >= -Constants.perfectHitThreshold && angleDifference <= Constants.perfectHitThreshold){
            print("Perfect Hit");
        }
    }

    void CollisionWithBoundary(){
        Vector3 position = Camera.main.ScreenToViewportPoint(transform.position);
        if (position.x < 0 || position.x > 1 || position.y < 0 || position.y > 1)
        {
            DisableCollisions();
            GameplayContoller.Instance.PlayerCollidedWithBoundary();
        }
    }

    //void CollisionWithCenter(){
    //    float distance = Vector3.Distance(transform.localPosition, Vector3.zero);
    //    if(distance < Constants.playerCenterCollisionDistance){
    //        print("Collided with center");
    //        isCollisionWithCenter = true;
    //        GameplayContoller.Instance.HasCollidedWithCenter();
    //    }
    //}

}
