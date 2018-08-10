using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayContoller : Singleton<GameplayContoller>, IController
{
    GameplayViewController gameplayViewController;

    public GameplayRefs gameplayRefs;

    public PlayerController playerController;
    public TargetController targetController;
    public OrbitController orbitController;
    public ColorController colorController;
    public GameplayTransitionController gameplayTransitionController;
   
    private Vector3 targetScreenPos;

    private bool isPerfectHit = false;   //perfect hit check passed by player
    private int score = 0;   //simple score
    private int level = 0;   //game level
    private int targetHitCount = 0;  //how many targets hit in order to level up
    private int comboCount = 0;  //chain of perfect hits

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
        InitializeGameplayViewController();
        InitializePlayer();
        InitializeTarget();
        InitializeOrbits();
        InitializeColors();
        SoundController.Instance.SetGamePlayMusic(true);
        ChangeGameState(GameState.Start);
    }

    private void InitializeGameplayViewController(){
        gameplayViewController = new GameplayViewController(gameplayRefs);
    }

    private void InitializePlayer(){
        playerController.Initialize();
    }

    private void InitializeTarget()
    {
        targetController.Initialize();
    }

    private void InitializeOrbits(){
        orbitController.Initialize();
    }

    private void InitializeColors(){
        colorController = new ColorController();
        colorController.Initialize();
        ChangeColors(); //call initially and then after level up
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
            case GameState.Restart:
                ResetScoring();
                ChangeColors();
                print("Restart Game");
                ChangeGameState(GameState.Start);
                break;
            case GameState.End:
                gameplayViewController.CameraShake(Constants.shakeTime);
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
                Scoring(isPerfectHit);
                targetScreenPos = targetController.GetScreenPosition(); //Get WorldToScreenPoint coordinates
                gameplayTransitionController.LevelTransitionOnTargetHit(targetScreenPos);
                break;
        }
    }

    private void ChangeColors(){
        ColorSet colorSet = colorController.GetRandomColorSet();
        gameplayViewController.ChangeColorSet(colorSet);
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

    //Spacebar key for PC
    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            ShotPlayer();
        }
    }

    public void PlayerCollidedWithTarget(bool perfectHit){
        print("Player collided with target");
        isAllowedToShot = false;
        isPerfectHit = perfectHit;
        SoundController.Instance.PlaySFXSound(SFX.TargetHit);
        ChangeGameState(GameState.TargetHit);

    }

    public void PlayerCollidedWithTimer()
    {
        print("Player collided with Timer");
        isAllowedToShot = false;
        SoundController.Instance.PlaySFXSound(SFX.PlayerBlast);
        ChangeGameState(GameState.End);
    }

    public void PlayerCollidedWithBoundary()
    {
        print("Player collided with Boundary");
        isAllowedToShot = false;
        SoundController.Instance.PlaySFXSound(SFX.PlayerBlast);
        gameplayViewController.CameraFlash(Color.white, Constants.flashTime);
        ChangeGameState(GameState.End);
    }

    private void Scoring(bool perfectHit){
        //increment target hit in order to level up
        TargetHitCount();
        if (CheckLevelUp())
        {
            LevelUp();
            ChangeColors();

        }
        //Add +5 and incr when perfect hit
        if (perfectHit)
        {
            SoundController.Instance.PlaySFXSound(SFX.PerfectHit);
            AddScore(5 + comboCount);
            comboCount += 1;
            gameplayViewController.CameraFlash(Color.yellow, Constants.flashTime);
        }
        else
        { //else add +1 with 0 comboCount
            AddScore(1);
            comboCount = 0;
            gameplayViewController.CameraFlash(Color.white, Constants.flashTime);
        }
    }


    private void AddScore(int s){
        score += s;
        gameplayViewController.SetScore(score.ToString());
    }

    private void ResetScoring(){
        score = 0;
        targetHitCount = 0;
        level = 0;
        comboCount = 0;
        isPerfectHit = false;
        AddScore(0);
    }

    private void TargetHitCount(){
        targetHitCount += 1;
    }

    private bool CheckLevelUp(){
        if(targetHitCount % Constants.targetHitCount == 0){
            return true;
        }
        return false;
    }

    private void LevelUp(){
        level += 1;
    }


}
