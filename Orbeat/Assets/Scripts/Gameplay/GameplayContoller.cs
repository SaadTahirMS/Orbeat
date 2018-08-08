using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayContoller : Singleton<GameplayContoller>, IController
{
    public PlayerController playerController;
    public TargetController targetController;

    public void Open()
    {
        InitializePlayer();
        InitializeTarget();
        StartGame();
    }

    void InitializePlayer(){
        playerController.Initialize();
    }

    void InitializeTarget()
    {
        targetController.Initialize();
    }

    public void StartGame()
    {
        ChangeGameState(GameState.Start);
    }

    void ChangeGameState(GameState state){

        switch(state){
            case GameState.Start:
                playerController.ChangeState(GameState.Start);
                targetController.ChangeState(GameState.Start);
                print("Start Game");
                break;
            case GameState.End:
                playerController.ChangeState(GameState.End);
                targetController.ChangeState(GameState.End);
                print("Game Over");
                MainMenuController.Instance.ActivateRestartBtn();
                break;
        }
    }

    //Event called on tap
    public void ShotPlayer(){
        if(playerController.isAllowedToShot)
            playerController.ChangeState(GameState.Shot);
        else{
            print("Not allowed to shot");
        }
    }

    public void PlayerCollidedWithTarget(){
        print("Player collided with target");
        ChangeGameState(GameState.Start);
    }

    public void PlayerCollidedWithTimer()
    {
        print("Player collided with Timer");
        ChangeGameState(GameState.End);
    }

    public void PlayerCollidedWithBoundary()
    {
        print("Player collided with Boundary");
        ChangeGameState(GameState.End);
    }

}
