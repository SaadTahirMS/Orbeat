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
    private List<HurdleController> hurdleControllers;
    private MainOrbitController mainOrbitController;
    private GameState gameState;
    //private bool isFirstTime = false;
    private float hurdleFillAmount;

    public void Open()
    {
        Application.targetFrameRate = 60;

        //isFirstTime = true;
        InitializeGameplayControllers();
        InitializePlayer();
        InitializeGameControls();
        InitializeHurdles();
        InitializeOrbit();
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

    private void InitializeGameControls(){
        gameplayRefs.gameControls.GameStart(true);
    }
 
    private void InitializeHurdles()
    {
        hurdleControllers = gameplayRefs.hurdleControllers;
        hurdleFillAmount = gameplayRefs.hurdleFillAmount;
    }

    private void InitializeOrbit(){
        gameplayRefs.mainOrbitController.Initialize(gameplayRefs);
        mainOrbitController = gameplayRefs.mainOrbitController;
    }

    public void ChangeGameState(GameState state){
        gameState = state;
        switch(state){
            case GameState.Start:
                SoundController.Instance.SetPitch(1f,false);
                SoundController.Instance.SetVolume(1f);
                playerController.ChangeState(GameState.Start);
                SetHurdleFillAmount(hurdleFillAmount);
                mainOrbitController.ChangeState(GameState.Start);
                gameplayTransitionController.ChangeState(GameState.Start);
                //orbitController.ChangeState(GameState.Start);
                //isFirstTime = false;
                print("Start Game");
                break;
            case GameState.Restart:
                //isFirstTime = true;
                print("Restart Game");
                playerController.ChangeState(GameState.Restart);
                mainOrbitController.ChangeState(GameState.Restart);
                ChangeGameState(GameState.Start);
                break;
            case GameState.End:
                SoundController.Instance.SetPitch(.5f,false);
                SoundController.Instance.SetVolume(1f);
                playerController.ChangeState(GameState.End);
                mainOrbitController.ChangeState(GameState.End);
                print("Game Over");
                MainMenuController.Instance.ActivateRestartBtn();
                //targetFillAmount = Constants.maxTargetFillAmount;
                SetHurdleFillAmount(0f);
                break;
            
        }
    }

    //Set the targets size 
    private void SetHurdleFillAmount(float fillAmount){
        for (int i = 0; i < hurdleControllers.Count;i++){
            hurdleControllers[i].SetFillAmount(fillAmount,Constants.transitionTime);
        }
    }

    private void IncreaseHurdleArea()
    {
        this.hurdleFillAmount += Constants.hurdleIncreaseAmount;
        this.hurdleFillAmount = Mathf.Clamp(this.hurdleFillAmount, Constants.minHurdleFillAmount, Constants.maxHurdleFillAmount);
    }

    public void PlayerHitHurdle()
    {
        Debug.Log("Player collided with hurdle");
        ChangeGameState(GameState.End);
    }

    public void HurdleHitWall()
    {
        Debug.Log("Hurdle collided with wall");
        mainOrbitController.SortHurdleOrbit(); //set first list element as last sibling in hierarchy
        mainOrbitController.ResetHurdleOrbitScale();    //reset the first list element scale
        mainOrbitController.SortOrbits();   //sort all the orbits 
    }
}
