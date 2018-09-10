using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayContoller : Singleton<GameplayContoller>, IController
{
    public GameplayRefs gameplayRefs;
    private GameplayTransitionController gameplayTransitionController;
    private GameplayViewController gameplayViewController;
    private PlayerController playerController;
    //private MainOrbitController orbitController;
    //private List<HurdleController> hurdleControllers;
    private GameState gameState;
    private bool isFirstTime = false;
    private float targetFillAmount;

    public void Open()
    {
        Application.targetFrameRate = 60;

        isFirstTime = true;
        InitializeGameplayControllers();
        InitializePlayer();
        //InitializeOrbit();
        //InitializeHurdles();
        ChangeGameState(GameState.Start);
    }

    private void InitializeGameplayControllers(){
        gameplayViewController = new GameplayViewController(gameplayRefs);
        gameplayTransitionController = new GameplayTransitionController(gameplayRefs);
    }

    private void InitializePlayer(){
        gameplayRefs.playerController.Initialize();
        playerController = gameplayRefs.playerController;
    }

 

    //private void InitializeOrbit(){
    //    gameplayRefs.orbitController.Initialize();
    //    orbitController = gameplayRefs.orbitController;
    //}



    //private void InitializeHurdles(){
    //    hurdleControllers = gameplayRefs.hurdleControllers;
    //    for (int i = 0; i < hurdleControllers.Count;i++){
    //        //hurdleControllers[i].Initialize();
    //    }
    //}

    public void ChangeGameState(GameState state){
        gameState = state;
        switch(state){
            case GameState.Start:
                SoundController.Instance.SetPitch(1f,false);
                SoundController.Instance.SetVolume(1f);
                gameplayTransitionController.ChangeState(GameState.Start);
                playerController.ChangeState(GameState.Start);
                //orbitController.ChangeState(GameState.Start);
                isFirstTime = false;
                print("Start Game");
                //SetTargetsSize();
                break;
            case GameState.Restart:
                isFirstTime = true;
                print("Restart Game");
                ChangeGameState(GameState.Start);
                break;
            case GameState.End:
                SoundController.Instance.SetPitch(.5f,false);
                SoundController.Instance.SetVolume(1f);
                playerController.ChangeState(GameState.End);
                //orbitController.ChangeState(GameState.End);
                print("Game Over");
                MainMenuController.Instance.ActivateRestartBtn();
                //targetFillAmount = Constants.maxTargetFillAmount;
                //ResetAllTargetSize();
                break;
          
        }
    }


    ////Set the targets size 
    //private void SetTargetsSize(){
    //    for (int i = 0; i < targetControllers.Count;i++){
    //        targetControllers[i].SetSize(targetFillAmount);
    //    }
    //    targetFillAmount -= Constants.targetReduceAmount;
    //    targetFillAmount = Mathf.Clamp(targetFillAmount, Constants.minTargetFillAmount, Constants.maxTargetFillAmount);
    //}

}
