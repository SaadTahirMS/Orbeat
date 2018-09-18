using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private int level = 0;
    private bool levelUp = false;

    public void Open()
    {
        Application.targetFrameRate = 60;
        InitializeGameplayControllers();
        InitializePlayer();
        InitializeHurdles();
        InitializeOrbit();
        InitializeColors();
        ChangeGameState(GameState.Start);
    }

    private void InitializeGameplayVariables()
    {
        //Constants.minHurdleFillAmount = gameplayRefs.minHurdleFillAmount;
        //Constants.maxHurdleFillAmount = gameplayRefs.maxHurdleFillAmount;
        //Constants.hurdleFillAmount = gameplayRefs.hurdleFillAmount;
        //Constants.cameraOffset = gameplayRefs.cameraOffset;
        //Constants.hurdlesDistance = Vector3.one * gameplayRefs.hurdlesDistance;
        //Constants.scaleSpeed = gameplayRefs.scaleSpeed;
        //Constants.playerRotationSpeed =  gameplayRefs.playerRotationSpeed;
        //Constants.playerScrollRotationSpeed = gameplayRefs.playerScrollRotationSpeed;
        //Constants.minRotateSpeed = gameplayRefs.minRotateSpeed;
        //Constants.maxRotateSpeed = gameplayRefs.maxRotateSpeed;
        //Constants.rotationOffset = gameplayRefs.rotationOffset;
        //Constants.playerCollision = gameplayRefs.playerCollision;
        //mainOrbitController.CanRotate(gameplayRefs.canRotateOrbits);
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
        colorController = gameplayRefs.colorController;
    }

    public void ChangeGameState(GameState state){
        gameState = state;
        switch(state){
            case GameState.Start:
                //ResetScore();
                ProgressionCurves();
                //GameProgression();
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
                gameplayTransitionController.ChangeState(GameState.Quit);
                gameplayRefs.inputController.GameStart(false);
                print("Game Over");
                ResetHurdleFillAmount();
                //ResetScore();
                //ResetOrbitList();

                break;
            
        }
    }

    private void ResetOrbitList()
    {
        orbitControllers = mainOrbitController.GetOrbits();
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

		#if UNITY_ANDROID

		if (Input.GetKeyDown (KeyCode.Escape)) {
			EventManager.DoFireBackButtonEvent ();
		}
		#endif
    }

    //Set all the hurdle sizes randomely
    private void SetHurdleFillAmount(){
        //for (int i = 0; i < hurdleControllers.Count;i++){
        //    float fillamount = RandomHurdleFillAmount();
        //    hurdleControllers[i].SetFillAmount(fillamount,Constants.transitionTime);
        //}
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
        //float fillamount = RandomHurdleFillAmount();
        //hurdleControllers[0].SetFillAmount(fillamount, Constants.transitionTime);
        //hurdleControllers[0].SetFillAmount(Constants.hurdleFillAmount, Constants.transitionTime);

        orbit.hurdleController.SetFillAmount(Constants.hurdleFillAmount, Constants.fillAmountTime);
    }

    //private float RandomHurdleFillAmount()
    //{
    //    float ran = Random.Range(Constants.minHurdleFillAmount, Constants.maxHurdleFillAmount);
    //    return ran;
    //}

    public void PlayerHitHurdle()
    {
        Debug.Log("Player collided with hurdle");
        ExplosionParticles();
        ChangeGameState(GameState.Quit);
    }

    private void PlayerCollisions(){
        playerController.SetCollisions(Constants.playerCollision);
    }

    private void GamePattern(){
        GameplayPattern gameplayPattern = new GameplayPattern
        {
            direction = 1,
            rotationSpeed = 0,
            rotationOffset = 0,
            hurdleDistance = new Vector3(7f,7f,7f),
            hurdleFillAmount = 0.7f,
            scaleSpeed = 0.12f
        };

    }

    public void HurdleHitWall()
    {
        Debug.Log("Hurdle collided with wall");
        //Other Things
        AddScore();
        gameplayViewController.OrbitFade();
        gameplayViewController.OrbitPunch();


        if(!levelUp){
            Debug.Log("Not level up");
            ProgressionCurves();
            //Applying progression settings
            orbitControllers = mainOrbitController.GetOrbits();
            mainOrbitController.SetNewScale();
            SetIndividualHurdleFillAmount(orbitControllers[0]); //Set the fill amount of this orbit
            mainOrbitController.AssignNewRotation();
            mainOrbitController.RotationOffset();
            PlayerCollisions();

            if (CheckLevelUp())
            {
                levelUp = true;
                ChangeColors();
            }

        }
        else if(levelUp && !IsPatternComplete()){
            // do things on level up
            Debug.Log("Level up");

            GamePattern();
            //Applying new settings
            orbitControllers = mainOrbitController.GetOrbits();
            mainOrbitController.CalculateInitialScale(0);



            SetIndividualHurdleFillAmount(orbitControllers[0]); //Set the fill amount of this orbit
            mainOrbitController.AssignNewRotation();
            mainOrbitController.RotationOffset();
            PlayerCollisions();
        }
        else if(IsPatternComplete()){
            // do things after pattern is complete
            Debug.Log("Pattern complete");
            levelUp = false;
            ProgressionCurves();
            //Applying progression settings
            orbitControllers = mainOrbitController.GetOrbits();
            mainOrbitController.SetNewScale();
            SetIndividualHurdleFillAmount(orbitControllers[0]); //Set the fill amount of this orbit
            mainOrbitController.AssignNewRotation();
            mainOrbitController.RotationOffset();
            PlayerCollisions();
        }

       

        //Sort Orbits
        mainOrbitController.SortHurdleOrbit();  //set first list element as last sibling in hierarchy
        mainOrbitController.SortOrbits();   //sort all the orbits 

    }

    private void ProgressionCurves(){
        //print("Difficulty Level: " + Constants.difficultyLevel);
        float y = score / Constants.difficultyLevel;
        float value;

        value = gameplayRefs.hurdleDistanceCurve.Evaluate(y) * gameplayRefs.maxHurdleDistance;
        Constants.hurdlesDistance = Vector3.one * Mathf.Clamp(value, gameplayRefs.minHurdleDistance, gameplayRefs.maxHurdleDistance);
        //print("New hurdleDistance x y: " + Constants.hurdlesDistance + "," + y);

        value = gameplayRefs.hurdleFillAmountCurve.Evaluate(y) * gameplayRefs.maxHurdleFillAmount;
        Constants.hurdleFillAmount = Mathf.Clamp(value, gameplayRefs.minHurdleFillAmount, gameplayRefs.maxHurdleFillAmount);
        //print("New hurdleFillAmount x y: " + Constants.hurdleFillAmount + "," + y);

        value = gameplayRefs.maxScaleSpeed - gameplayRefs.scaleSpeedCurve.Evaluate(y) * gameplayRefs.maxScaleSpeed;
        Constants.scaleSpeed = Mathf.Clamp(value, gameplayRefs.minScaleSpeed, gameplayRefs.maxScaleSpeed);
        //print("New scaleSpeed x y: " + Constants.scaleSpeed + "," + y);

        value = gameplayRefs.rotationOffsetCurve.Evaluate(y) * gameplayRefs.maxRotationOffset;
        Constants.rotationOffset = Mathf.Clamp(value, gameplayRefs.minRotationOffset, gameplayRefs.maxRotationOffset);
        //print("New rotationOffset x y: " + Constants.rotationOffset + "," + y);

        //Max rotation curve and min value will be -x of max value
        value = gameplayRefs.orbitRotationCurve.Evaluate(y) * gameplayRefs.maxOrbitRotateSpeed;
        Constants.maxRotateSpeed = Mathf.Clamp(value, gameplayRefs.minOrbitRotateSpeed, gameplayRefs.maxOrbitRotateSpeed);
        value = Constants.maxRotateSpeed - 5f;
        Constants.minRotateSpeed = Mathf.Clamp(value, gameplayRefs.minOrbitRotateSpeed, gameplayRefs.maxOrbitRotateSpeed);
        //print("New maxRotateSpeed x y: " + Constants.maxRotateSpeed + "," + y);
        //print("New minRotateSpeed x y: " + Constants.minRotateSpeed + "," + y);

        //Player Collisions
        Constants.playerCollision = gameplayRefs.playerCollision;


    }

    private void GameProgression(){
        //Hurdle Distance
        Constants.hurdlesDistance = Vector3.one * gameplayRefs.hurdlesDistance;

        //Hurdle FillAmount
        Constants.hurdleFillAmount = gameplayRefs.hurdleFillAmount;

        //Orbit Scale Speed
        Constants.scaleSpeed = gameplayRefs.scaleSpeed;

        //Camera Offset
        Constants.cameraOffset = gameplayRefs.cameraOffset;

        //Orbits Rotation
        Constants.minRotateSpeed = gameplayRefs.minRotateSpeed;
        Constants.maxRotateSpeed = gameplayRefs.maxRotateSpeed;

        //Orbit Rotation Offset
        Constants.rotationOffset = gameplayRefs.rotationOffset;

        //Player Rotate Speed
        Constants.playerRotationSpeed = gameplayRefs.playerRotationSpeed;

        //Player Scroll Speed
        Constants.playerScrollRotationSpeed = gameplayRefs.playerScrollRotationSpeed;

        //Player Collisions
        Constants.playerCollision = gameplayRefs.playerCollision;
    }

    private void ExplosionParticles()
    {
        Vector3 pos = new Vector3(playerController.transform.position.x, playerController.transform.position.y, 0f);
        Instantiate(gameplayRefs.triangleParticles, pos, Quaternion.identity);
        Instantiate(gameplayRefs.hexagonParticles, pos, Quaternion.identity);
    }

    private void OrbitParticles(){
        Instantiate(gameplayRefs.orbitParticles, Vector3.zero, Quaternion.identity);
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
        //ColorSet colorSet = colorController.GetRandomColorSet();
        ColorSet colorSet = colorController.GetIncrementalColorSet();
        gameplayViewController.ChangeColorSet(colorSet);
    }

    private bool CheckLevelUp(){
        if(score%5 == 0){
            level += 1;
            return true;
        }
        return false;
    }

    private bool IsPatternComplete(){
        if(score%10 == 0 && levelUp){
            return true;
        }
        return false;
    }
}
