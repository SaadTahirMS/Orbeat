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
    private ColorController colorController;
    public GameState gameState;

    private int score = 0;
    private int level = 0;

    public void Open()
    {
        Application.targetFrameRate = 60;
        InitializeGameplayVariables();
        InitializeGameplayControllers();
        InitializePlayer();
        InitializeHurdles();
        InitializeOrbit();
        InitializeColors();
        ChangeGameState(GameState.Start);
    }

    private void InitializeGameplayVariables()
    {
        Constants.minHurdleFillAmount = gameplayRefs.minHurdleFillAmount;
        Constants.maxHurdleFillAmount = gameplayRefs.maxHurdleFillAmount;
        Constants.cameraOffset = gameplayRefs.cameraOffset;
        Constants.hurdlesDistance = Vector3.one * gameplayRefs.hurdlesDistance;
        Constants.scaleSpeed = gameplayRefs.scaleSpeed;
        Constants.playerRotationSpeed =  gameplayRefs.playerRotationSpeed;
        Constants.playerScrollRotationSpeed = gameplayRefs.playerScrollRotationSpeed;
        Constants.minRotateSpeed = gameplayRefs.minRotateSpeed;
        Constants.maxRotateSpeed = gameplayRefs.maxRotateSpeed;
        Constants.rotationOffset = gameplayRefs.rotationOffset;
        Constants.playerCollision = gameplayRefs.playerCollision;
    }

    private void InitializeGameplayControllers(){
        gameplayViewController = new GameplayViewController(gameplayRefs);
        gameplayTransitionController = new GameplayTransitionController(gameplayRefs);
    }

    private void InitializePlayer(){
        gameplayRefs.playerController.Initialize();
        playerController = gameplayRefs.playerController;
    }
 
    private void InitializeHurdles()
    {
        hurdleControllers = gameplayRefs.hurdleControllers;
    }

    private void InitializeOrbit(){
        gameplayRefs.mainOrbitController.Initialize(gameplayRefs);
        mainOrbitController = gameplayRefs.mainOrbitController;
    }

    private void InitializeColors(){
        colorController = new ColorController();
        colorController.Initialize();
        ChangeColors();
    }

    public void ChangeGameState(GameState state){
        gameState = state;
        switch(state){
            case GameState.Start:
                ResetScore();
                gameplayRefs.inputController.GameStart(true);
                SoundController.Instance.SetPitch(1f,false);
                SoundController.Instance.SetVolume(1f);
                playerController.ChangeState(GameState.Start);
                SetHurdleFillAmount();
                mainOrbitController.ChangeState(GameState.Start);
                gameplayTransitionController.ChangeState(GameState.Start);
                //isFirstTime = false;
                print("Start Game");
                ChangeColors();
                break;
            case GameState.Restart:
                //isFirstTime = true;
                print("Restart Game");
                playerController.ChangeState(GameState.Restart);
                mainOrbitController.ChangeState(GameState.Restart);
                ChangeGameState(GameState.Start);
                break;
            //case GameState.End:
                ////SoundController.Instance.SetPitch(.9f,false);
                //SoundController.Instance.SetVolume(0.75f);
                //playerController.ChangeState(GameState.End);
                //mainOrbitController.ChangeState(GameState.End);
                //gameplayRefs.inputController.GameStart(false);
                //print("Game Over");
                //MainMenuController.Instance.ActivateRestartBtn();
                //ResetHurdleFillAmount();
                //break;
            case GameState.Quit:
                playerController.ChangeState(GameState.Quit);
                mainOrbitController.ChangeState(GameState.Quit);
                gameplayRefs.inputController.GameStart(false);
                print("Game Over");
                MainMenuController.Instance.Open();
                ResetHurdleFillAmount();
                ResetOrbitList();
                break;
            
        }
    }

    private void ResetOrbitList()
    {
        List<OrbitController> orbitControllers = mainOrbitController.GetOrbits();
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            orbitControllers[i] = gameplayRefs.intialOrbitList[i];
            //print(orbitControllers[i].name);
            orbitControllers[i].transform.SetAsFirstSibling();
        }
    }

    private void Update()
    {
        if (gameplayViewController != null && gameState == GameState.Start)
            gameplayViewController.LookAtTransform(playerController.transform.position, Constants.cameraOffset);

        if(Input.GetKey(KeyCode.Escape)){
            ChangeGameState(GameState.Quit);
        }
    }

    //Set the hurdle sizes randomely
    private void SetHurdleFillAmount(){
        for (int i = 0; i < hurdleControllers.Count;i++){
            float fillamount = RandomHurdleFillAmount();
            hurdleControllers[i].SetFillAmount(fillamount,Constants.transitionTime);
        }
    }

    private void ResetHurdleFillAmount()
    {
        for (int i = 0; i < hurdleControllers.Count; i++)
        {
            hurdleControllers[i].SetFillAmount(0f, Constants.transitionTime);
        }
    }

    //Sets hurdle size at index 0
    private void SetIndividualHurdleFillAmount()
    {
        float fillamount = RandomHurdleFillAmount();
        hurdleControllers[0].SetFillAmount(fillamount, Constants.transitionTime);
    }

    private float RandomHurdleFillAmount()
    {
        float ran = Random.Range(Constants.minHurdleFillAmount, Constants.maxHurdleFillAmount);
        return ran;
    }

    public void PlayerHitHurdle()
    {
        Debug.Log("Player collided with hurdle");
        ExplosionParticles();
        ChangeGameState(GameState.Quit);
    }

    public void HurdleHitWall()
    {
        Debug.Log("Hurdle collided with wall");
        mainOrbitController.SortHurdleOrbit();  //set first list element as last sibling in hierarchy
        mainOrbitController.ResetHurdleOrbitScale();    //reset the first list element scale
        SetIndividualHurdleFillAmount();    //set first list element fill amount
        mainOrbitController.SortOrbits();   //sort all the orbits 
        mainOrbitController.RotationOffset();
        AddScore();
        if(CheckLevelUp()){
            ChangeColors();
        }
    }

    private void ExplosionParticles()
    {
        Vector3 pos = new Vector3(playerController.transform.position.x, playerController.transform.position.y, 0f);
        Instantiate(gameplayRefs.triangleParticles, pos, Quaternion.identity);
        Instantiate(gameplayRefs.hexagonParticles, pos, Quaternion.identity);
    }

    private void AddScore(){
        score += 1;
        gameplayViewController.SetScore(score);
    }

    private void ResetScore(){
        score = 0;
        level = 0;
    }

    private void ChangeColors(){
        ColorSet colorSet = colorController.GetRandomColorSet();
        gameplayViewController.ChangeColorSet(colorSet);
    }

    private bool CheckLevelUp(){
        if(score%10 == 0){
            level += 1;
            return true;
        }
        return false;
    }
}
