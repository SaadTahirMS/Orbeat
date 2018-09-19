using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameplayContoller : Singleton<GameplayContoller>
{
    public GameplayRefs gameplayRefs;
    private GameplayTransitionController gameplayTransitionController;
    private GameplayViewController gameplayViewController;
    private PlayerController playerController;
    private List<HurdleController> hurdleControllers;
    private MainOrbitController mainOrbitController;
    private List<OrbitController> orbitControllers;
    private ColorController colorController;

    [HideInInspector]public GameState gameState;

    private int score = 0;
    int randomSpecialValue = 0;
    bool addInitialDistance = false;
    float scaleSpeed = 1.5f;
    private float normalModeTimer = Constants.normalModeTime;

    private void ResetGame()
    {
        score = 0;
        PlayerData.CurrentScore = score;
        normalModeTimer = Constants.normalModeTime;
        gameplayViewController.SetScore(score);
        ResetOrbitList();
    }

    public void Open()
    {
        Application.targetFrameRate = 60;
		InitializeSoundEffect ();
        InitializeGameplayControllers();
        InitializePlayer();
        InitializeHurdles();
        InitializeOrbit();
        InitializeColors();
        ResetOrbitList();
        ChangeGameState(GameState.Start);
    }

	private void InitializeSoundEffect()
	{
		SoundController.Instance.SetPitch(1f,true);
		SoundController.Instance.SetVolume(1f);
	}

    private void InitializeGameplayControllers(){
        gameplayViewController = new GameplayViewController(gameplayRefs);
        gameplayTransitionController = new GameplayTransitionController(gameplayRefs);
    }

    private void InitializePlayer(){
        gameplayRefs.playerController.Initialize();
        playerController = gameplayRefs.playerController;
        PlayerCollisions();
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
        colorController = gameplayRefs.colorController;
    }

    public void ChangeGameState(GameState state){
        gameState = state;
        switch(state){
            case GameState.Start:
                print("Start Game");
                ResetGame();
                ProgressionCurves();
                gameplayRefs.inputController.GameStart(true);
                SoundController.Instance.SetPitch(1f,false);
                SoundController.Instance.SetVolume(1f);
                playerController.ChangeState(GameState.Start);
                SetHurdleFillAmount();
                mainOrbitController.ChangeState(GameState.Start);
                gameplayTransitionController.ChangeState(GameState.Start);
                ChangeColors();
                break;
            case GameState.Revive:
                print("Revive Game");
                playerController.ChangeState(GameState.Start);
                mainOrbitController.ChangeState(GameState.Start);
                gameplayTransitionController.ChangeState(GameState.Start);
                //ChangeGameState(GameState.Start);
                break;
            case GameState.Quit:
                print("Game Over");
                playerController.ChangeState(GameState.Quit);
                mainOrbitController.ChangeState(GameState.Quit);
                gameplayTransitionController.ChangeState(GameState.Quit);
                gameplayRefs.inputController.GameStart(false);
                ResetHurdleFillAmount();
                break;
            
        }
    }

    public void ReviveGame(){

        ChangeGameState(GameState.Revive);
    }

    public void ResetOrbitList()
    {
        orbitControllers = mainOrbitController.GetOrbits();
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            orbitControllers[i] = gameplayRefs.intialOrbitList[i];
            orbitControllers[i].transform.SetAsFirstSibling();
        }
    }

    private void Update()
    {
        normalModeTimer -= Time.deltaTime;

        if (gameplayViewController != null && gameState == GameState.Start)
            gameplayViewController.LookAtTransform(playerController.transform.position, Constants.cameraOffset);
        

		#if UNITY_ANDROID
          
		if (Input.GetKeyDown (KeyCode.Escape)) {
			EventManager.DoFireBackButtonEvent ();
		}

		#endif
    }

    private void SetHurdleFillAmount(){
        for (int i = 0; i < hurdleControllers.Count;i++){
            hurdleControllers[i].SetFillAmount(Constants.hurdleFillAmount,Constants.transitionTime);
        }
    }

    private void ResetHurdleFillAmount()
    {
        for (int i = 0; i < hurdleControllers.Count; i++)
        {
            hurdleControllers[i].SetFillAmount(0f, Constants.transitionTime);
        }
    }

    //Sets hurdle size
    private void SetIndividualHurdleFillAmount(OrbitController orbit)
    {
        orbit.hurdleController.SetFillAmount(Constants.hurdleFillAmount, Constants.fillAmountTime);
    }

    public void PlayerHitHurdle()
    {
        Debug.Log("Player collided with hurdle");
        ExplosionParticles();
        ChangeGameState(GameState.Quit);
    }

    private void PlayerCollisions(){
        playerController.SetCollisions(gameplayRefs.playerCollision);
    }

    public void HurdleHitWall()
    {
        NormalMode();

        if (IsNormalModeChanged())
        {
            SpecialMode();
        }
    }

    private void NormalMode()
    {
        orbitControllers = mainOrbitController.GetOrbits();
        gameplayViewController.OrbitPunchFade();
        ProgressionCurves();
        AddScore(1);
        //Applying progression settings
        mainOrbitController.SetNewScale(addInitialDistance, scaleSpeed);
        addInitialDistance = false;
        SetIndividualHurdleFillAmount(orbitControllers[0]); //Set the fill amount of this orbit
        mainOrbitController.AssignNewRotation();
        orbitControllers[0].StopSpecialRotation();
        mainOrbitController.SortHurdleOrbit();  //set first list element as last sibling in hierarchy
        mainOrbitController.SortOrbits();   //sort all the orbits 

    }

    private void SpecialMode()
    {
        normalModeTimer = Constants.normalModeTime;

        randomSpecialValue++;// = Random.Range(1, 4);

        if(randomSpecialValue >=4)
        {
            randomSpecialValue = 1;
        }

        addInitialDistance = true;
        mainOrbitController.StopScale();
        Constants.hurdlesDistance = Vector3.one;
        Constants.hurdleFillAmount = 0.55f;
        mainOrbitController.SetNewRotations(0, randomSpecialValue == (int)ModeType.AntiClockWise ? -1 : 1, 2);

        for (int i = 0; i < mainOrbitController.GetOrbits().Count; i++)
        {
            SetIndividualHurdleFillAmount(orbitControllers[i]);
        }

        mainOrbitController.ScaleTo(Vector3.one * 5f,3).OnComplete(OnScaleComplete) ;
    }

    private void OnScaleComplete()
    {
        switch(randomSpecialValue)
        {
            case (int)ModeType.AntiClockWise:
                AntiClockWiseMode();
                break;
            case (int)ModeType.ClockWise:
                ClockWiseMode();
                break;
            case (int)ModeType.PingPongMode:
                PingPongMode();
                break;
        }
    }

    private void ClockWiseMode()
    {
        mainOrbitController.SetNewRotations(0, 1, 7.5f, false);
        Constants.scaleSpeed = 1.5f;

        scaleSpeed = Constants.scaleSpeed;
        mainOrbitController.Scale();
    }

    private void AntiClockWiseMode()
    {
        mainOrbitController.SetNewRotations(0, -1, 7.5f, false);
        Constants.scaleSpeed = 1.5f;

        scaleSpeed = Constants.scaleSpeed;
        mainOrbitController.Scale();
    }

    private void PingPongMode()
    {
        mainOrbitController.SetPingPongRotation(0, 1, 2, false);
        Constants.scaleSpeed = 3f;
        scaleSpeed = Constants.scaleSpeed;
        mainOrbitController.Scale();
    }


    private void ProgressionCurves(){
        float y = score / Constants.difficultyLevel;
        float value;

        value = gameplayRefs.hurdleDistanceCurve.Evaluate(y) * gameplayRefs.maxHurdleDistance;
        Constants.hurdlesDistance = Vector3.one * Mathf.Clamp(value, gameplayRefs.minHurdleDistance, gameplayRefs.maxHurdleDistance);

        value = gameplayRefs.hurdleFillAmountCurve.Evaluate(y) * gameplayRefs.maxHurdleFillAmount;
        Constants.hurdleFillAmount = Mathf.Clamp(value, gameplayRefs.minHurdleFillAmount, gameplayRefs.maxHurdleFillAmount);

        value = gameplayRefs.maxScaleSpeed - gameplayRefs.scaleSpeedCurve.Evaluate(y) * gameplayRefs.maxScaleSpeed;
        Constants.scaleSpeed = Mathf.Clamp(value, gameplayRefs.minScaleSpeed, gameplayRefs.maxScaleSpeed);

        value = gameplayRefs.rotationOffsetCurve.Evaluate(y) * gameplayRefs.maxRotationOffset;
        Constants.rotationOffset = Mathf.Clamp(value, gameplayRefs.minRotationOffset, gameplayRefs.maxRotationOffset);

        //Max rotation curve and min value will be -x of max value
        value = gameplayRefs.orbitRotationCurve.Evaluate(y) * gameplayRefs.maxOrbitRotateSpeed;
        Constants.maxRotateSpeed = Mathf.Clamp(value, gameplayRefs.minOrbitRotateSpeed, gameplayRefs.maxOrbitRotateSpeed);
        value = Constants.maxRotateSpeed - 5f;
        Constants.minRotateSpeed = Mathf.Clamp(value, gameplayRefs.minOrbitRotateSpeed, gameplayRefs.maxOrbitRotateSpeed);

    }

    private void ExplosionParticles()
    {
        Vector3 pos = new Vector3(playerController.transform.position.x, playerController.transform.position.y, 0f);
        Instantiate(gameplayRefs.triangleParticles, pos, Quaternion.identity);
        Instantiate(gameplayRefs.hexagonParticles, pos, Quaternion.identity);
    }

    private void AddScore(int s){
        score += s;
		PlayerData.CurrentScore = score;
		EventManager.DoFireScoreUpdatedEvent ();
        gameplayViewController.SetScore(score);
    }

    private void ChangeColors()
    {
        //ColorSet colorSet = colorController.GetRandomColorSet();
        ColorSet colorSet = colorController.GetIncrementalColorSet();
        gameplayViewController.ChangeColorSet(colorSet);
    }

    public bool IsNormalModeChanged()
    {
        return normalModeTimer <= 0 ? true : false;
    }

}
