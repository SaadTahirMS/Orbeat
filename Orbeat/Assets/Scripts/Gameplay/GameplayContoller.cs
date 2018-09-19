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
    private List<GameplayPattern> gameplayPatterns;
    private GameplayPattern gameplayPattern;

    [HideInInspector]public GameState gameState;

    private int score = 0;
    private int level = 0;
    private bool levelUp = false;
    private Vector3 hurdleInitialResetScale = new Vector3(24f,24f,24f);
    private int orbitHitId;

    public void Open()
    {
        Application.targetFrameRate = 60;
		InitializeSoundEffect ();
        InitializeGameplayControllers();
        InitializePlayer();
        InitializeHurdles();
        InitializeOrbit();
        InitializeColors();
        InitializePattern();
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
        //PlayerCollisions();

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

    private void InitializePattern()
    {
        gameplayPatterns = gameplayRefs.gameplayPatterns;
    }


    public void ChangeGameState(GameState state){
        gameState = state;
        switch(state){
            case GameState.Start:
                ResetVariables();
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
            case GameState.Quit:
                playerController.ChangeState(GameState.Quit);
                mainOrbitController.ChangeState(GameState.Quit);
                gameplayTransitionController.ChangeState(GameState.Quit);
                gameplayRefs.inputController.GameStart(false);
                print("Game Over");
                ResetHurdleFillAmount();
                Constants.hurdlesInitialDistance = hurdleInitialResetScale;
                //ResetScore();
                //ResetOrbitList();
                levelUpTimer = 0;
                levelUp = false;

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


		//if (startRotations){
		//	//StartCoroutine(OrbitRotateDirection());
		//	levelUpTimer += Time.deltaTime;
		//	//print("levelUpTimer " + levelUpTimer);
		//	//if (IsLevelUpTimerComplete())
		//		//LevelUpComplete();
		//}

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

    private GameplayPattern GetGameplayPattern(int levelIndex){
        if (levelIndex == gameplayPatterns.Count){
            levelIndex = 0;
            level = 0;
        }
        return gameplayPatterns[levelIndex];
    }

    private bool startRotations = false;
    private float levelUpTimer = 0f;
    private bool isthisFirstIndex;


    private void NormalMode()
    {
        orbitControllers = mainOrbitController.GetOrbits();
        gameplayViewController.OrbitPunchFade();
        //Debug.Log("Not level up");
        ProgressionCurves();
        AddScore(1);
        //Applying progression settings
        mainOrbitController.SetNewScale(addInitialDistance, scaleSpeed);
        addInitialDistance = false;
        SetIndividualHurdleFillAmount(orbitControllers[0]); //Set the fill amount of this orbit
        mainOrbitController.AssignNewRotation();
        //mainOrbitController.RotationOffset();
        orbitControllers[0].StopSpecialRotation();
        //Sort Orbits
        mainOrbitController.SortHurdleOrbit();  //set first list element as last sibling in hierarchy
        mainOrbitController.SortOrbits();   //sort all the orbits 

    }

    int randomSpecialValue = 0;

    private void SpecialMode()
    {
        

        NormalMode();

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
        for (int i = 0; i < mainOrbitController.GetOrbits().Count; i++){
            SetIndividualHurdleFillAmount(orbitControllers[i]);
        }
        mainOrbitController.ScaleTo(Vector3.one * 5f,3).OnComplete(OnScaleComplete) ;
    }

    private void OnScaleComplete()
    {
        //score = -10;
        //mainOrbitController.SetNewRotations(0, 1, 7.5f,false);
        //Constants.scaleSpeed = 1.5f;
        //mainOrbitController.Scale();

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
        score = -5;
        mainOrbitController.SetNewRotations(0, 1, 7.5f, false);
        Constants.scaleSpeed = 1.5f;

        scaleSpeed = Constants.scaleSpeed;
        mainOrbitController.Scale();
    }

    private void AntiClockWiseMode()
    {
        score = -5;
        mainOrbitController.SetNewRotations(0, -1, 7.5f, false);
        Constants.scaleSpeed = 1.5f;

        scaleSpeed = Constants.scaleSpeed;
        mainOrbitController.Scale();
    }

    private void PingPongMode()
    {
        score = -5;
        mainOrbitController.SetPingPongRotation(0, 1, 2, false);
        Constants.scaleSpeed = 3f;
        scaleSpeed = Constants.scaleSpeed;
        mainOrbitController.Scale();
    }


    bool addInitialDistance = false;
    float scaleSpeed = 1.5f;

    public void HurdleHitWall()
    {
        Debug.Log("Hurdle collided with wall");
        if (!CheckLevelUp())
        {
            NormalMode();
        }
        else
        {
            
            SpecialMode();
        }

        //if(!levelUp){
        //    //LevelUpComplete();
        //    gameplayViewController.OrbitPunchFade(); 
        //    Debug.Log("Not level up");
        //    ProgressionCurves();
        //    AddScore(1);
        //    //Applying progression settings
        //    mainOrbitController.SetNewScale();
        //    SetIndividualHurdleFillAmount(orbitControllers[0]); //Set the fill amount of this orbit
        //    mainOrbitController.AssignNewRotation();
        //    mainOrbitController.RotationOffset();

        //    //Sort Orbits
        //    mainOrbitController.SortHurdleOrbit();  //set first list element as last sibling in hierarchy
        //    mainOrbitController.SortOrbits();   //sort all the orbits 

        //    if (CheckLevelUp())
        //    {
        //        print("Game Level Up");
        //        orbitHitId = orbitControllers[0].id;
        //        print("ID: " + orbitHitId);
        //        //levelUp = true;
        //        ChangeColors();
        //        gameplayPattern = GetGameplayPattern(level);
        //        level += 1;

        //        //Applying Game Pattern
        //        Constants.scaleSpeed = gameplayPattern.scaleSpeed;
        //        Constants.hurdlesInitialDistance = gameplayPattern.hurdleInitialDistance;
        //        Constants.hurdlesDistance = gameplayPattern.hurdleDistance;
        //        Constants.hurdleFillAmount = gameplayPattern.hurdleFillAmount;
        //        Constants.rotationOffset = gameplayPattern.rotationOffset;

        //        //mainOrbitController.SetInitialScale();
        //        //mainOrbitController.Scale();
        //        //isthisFirstIndex = true;
        //        //mainOrbitController.StopScale();
        //        //mainOrbitController.ScaleTo(new Vector3(5f, 5f, 5f));
        //        //for (int i = 0; i < orbitControllers.Count; i++){
        //        //    SetIndividualHurdleFillAmount(orbitControllers[i]);
        //        //}
        //        //mainOrbitController.SetNewRotations(gameplayPattern.initialRotation, gameplayPattern.direction, gameplayPattern.initialRotationSpeed);
        //        //mainOrbitController.SetNewRotationOffset();
        //    }
        //}
        //else if (levelUp && !IsLevelUpTimerComplete())
        //{
            //// do things on level up
            //Debug.Log("Start");

            ////Check if the same hurdle has hit again so that all orbits can rotate
            //if (orbitHitId == orbitControllers[0].id && !startRotations){
            //    //print("Start rotations");
            //    startRotations = true;
            //    orbitHitId = 0;
            //    Debug.Log("first if");
            //}
            //if (startRotations)
            //{
            //    Debug.Log("second if");
            //    mainOrbitController.SetRotateSpeed(gameplayPattern.direction, gameplayPattern.rotationSpeed);
            //    //Constants.scaleSpeed = 2.5f;
            //    mainOrbitController.StopScale();
            //    mainOrbitController.ScaleTo(new Vector3(0f, 0f, 0f));

            //    Constants.hurdlesDistance = Vector3.one * 7;
            //    Constants.scaleSpeed = 1;

            //    //if (isthisFirstIndex)
            //    {
            //        orbitControllers[0].SetScale(hurdleInitialResetScale);
            //        isthisFirstIndex = false;
            //    }
            //    //else
            //    //{
            //    //    mainOrbitController.SetNewScale();
            //    //}

            //    mainOrbitController.ScaleIndividual(Vector3.zero,0);

            //    //Constants.hurdlesInitialDistance = hurdleInitialResetScale;
            //    //mainOrbitController.SetInitialScaleOfZeroOrbit();
            //    //SetIndividualHurdleFillAmount(orbitControllers[0]); //Set the fill amount of this orbit
            //    //mainOrbitController.AssignNewRotation();
            //    //mainOrbitController.RotationOffset();

            //    ////Sort Orbits
            //    mainOrbitController.AssignNewRotation();
            //    mainOrbitController.SortHurdleOrbit();  //set first list element as last sibling in hierarchy
            //    mainOrbitController.SortOrbits(); 

            //}
            //else
            //{
            //    Debug.Log("else");
            //    mainOrbitController.SetNewRotations(0f, gameplayPattern.direction, gameplayPattern.initialRotationSpeed);
            //}

             

            //mainOrbitController.SetNewScale();
    
        //}


    }

    private void LevelUpComplete()
    {
       
        // do things after pattern is complete
        Debug.Log("Pattern complete");
        //gameplayViewController.Flash();
        AddScore(5);
        levelUp = false;


        startRotations = false;
        levelUpTimer = 0f;
        ProgressionCurves();
        return;

        Constants.hurdlesInitialDistance = hurdleInitialResetScale;
        mainOrbitController.SetInitialScale();
        mainOrbitController.Scale();
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            SetIndividualHurdleFillAmount(orbitControllers[i]);
        }
        mainOrbitController.AssignNewRotations();
    }

    IEnumerator OrbitRotateDirection()
    {
        yield return new WaitForSeconds(0.5f);
        if (gameplayPattern.pingpong)
            mainOrbitController.ChangeDirection();
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
        //Constants.playerCollision = gameplayRefs.playerCollision;
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

    private void AddScore(int s){
        score += s;
        gameplayViewController.SetScore(score);
    }

    private void ResetVariables(){
        score = 0;
        level = 0;
        levelUp = false;
        startRotations = false;
        levelUpTimer = 0f;
        gameplayViewController.SetScore(score);
    }

    private void ChangeColors(){
        //ColorSet colorSet = colorController.GetRandomColorSet();
        ColorSet colorSet = colorController.GetIncrementalColorSet();
        gameplayViewController.ChangeColorSet(colorSet);
    }

    private bool CheckLevelUp(){
        if(score > 3){
            return true;
        }
        return false;
    }


    private bool IsLevelUpTimerComplete(){
        if (levelUpTimer > 2.5f)
            return true;
        return false;
    }

}
