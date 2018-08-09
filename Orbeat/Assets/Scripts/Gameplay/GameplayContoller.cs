using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayContoller : Singleton<GameplayContoller>, IController
{
    public PlayerController playerController;
    public TargetController targetController;
    public OrbitController orbitController;
    public GameplayTransitionController gameplayTransitionController;
    private Vector3 targetScreenPos;


    private bool isAllowedToShot;
    public bool IsAllowedToShot
    {
        get
        {
            return isAllowedToShot;
        }
        set
        {
            isAllowedToShot = value;
        }
    }

    public void Open()
    {
        InitializePlayer();
        InitializeTarget();
        InitializeOrbits();
        ChangeGameState(GameState.Start);
    }

    void InitializePlayer(){
        playerController.Initialize();
    }

    void InitializeTarget()
    {
        targetController.Initialize();
    }

    void InitializeOrbits(){
        orbitController.Initialize();
    }

    public void ChangeGameState(GameState state){

        switch(state){
            case GameState.Start:
                playerController.ChangeState(GameState.Start);
                targetController.ChangeState(GameState.Start);
                orbitController.ChangeState(GameState.Start);
                gameplayTransitionController.LevelTransitionOnStart(targetController.Position,playerController.Position,orbitController.Position);
                print("Start Game");
                break;
            case GameState.End:
                gameplayTransitionController.StopTimerMovement();
                playerController.ChangeState(GameState.End);
                targetController.ChangeState(GameState.End);
                orbitController.ChangeState(GameState.End);
                print("Game Over");
                MainMenuController.Instance.ActivateRestartBtn();
                gameplayTransitionController.LevelTransitionOnEnd();
                break;
            case GameState.Shot:
                gameplayTransitionController.StopTimerMovement();
                playerController.ChangeState(GameState.Shot); //this initiates a player shot
                break;
            case GameState.TargetHit:
                targetScreenPos = targetController.GetScreenPosition(); //Get WorldToScreenPoint coordinates
                gameplayTransitionController.LevelTransitionOnTargetHit(targetScreenPos);
                break;
        }
    }

    //Event called on tap
    public void ShotPlayer(){
        if(isAllowedToShot){
            isAllowedToShot = false;
            ChangeGameState(GameState.Shot);
        }
        else{
            print("Not allowed to shot");
        }
    }

    public void PlayerCollidedWithTarget(){
        print("Player collided with target");
        isAllowedToShot = false;
        ChangeGameState(GameState.TargetHit);
    }

    public void PlayerCollidedWithTimer()
    {
        print("Player collided with Timer");
        isAllowedToShot = false;
        ChangeGameState(GameState.End);
    }

    public void PlayerCollidedWithBoundary()
    {
        print("Player collided with Boundary");
        isAllowedToShot = false;
        ChangeGameState(GameState.End);
    }

}
