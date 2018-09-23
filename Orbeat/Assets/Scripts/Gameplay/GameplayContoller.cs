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

	private WaitForSeconds goSoundCrWait;
	private float goSoundCrDelay = 1f;

    [HideInInspector]public GameState gameState;

    private int score = 0;
    int randomSpecialValue = 0;
    bool addInitialDistance = false;
    float scaleSpeed = 1.5f;
    private float normalModeTimer;
    private float colorTimer;
    private int orbitHitId = 0;

    private void ResetScore(){
        score = 0;
        gameplayViewController.SetScore(score);
    }

    private void ResetGame()
    {
        PlayerData.CurrentScore = score;
        normalModeTimer = Random.Range(Constants.minNormalModeTime, Constants.maxNormalModeTime);
        gameplayViewController.SetScore(score);
        addInitialDistance = false;
        scaleSpeed = 0.18f;
        ResetOrbitList();
        SetRandomColorTimer();
    }

	private bool isHighScoreMade;

    public void Open()
    {
        Application.targetFrameRate = 60;
		goSoundCrWait = new WaitForSeconds (goSoundCrDelay);
		InitializeSoundEffect ();
        InitializeGameplayControllers();
        InitializePlayer();
        InitializeHurdles();
        InitializeOrbit();
        InitializeColors();
        InitializeBeats();
        ResetOrbitList();
        ChangeGameState(GameState.Start);
    }

	private void InitializeSoundEffect()
	{
		//SoundController.Instance.SetPitch(1f,true);
		SoundController.Instance.SetVolume(0.7f);

		StartCoroutine (GameStartTimeScaleCoroutine ());
	}

	private IEnumerator PlayGoSoundCR()
	{
//		Time.timeScale = 0.1f;
		yield return goSoundCrWait;
//		Time.timeScale = 1;
		SoundController.Instance.PlayDialogSound (SFX.Go);
	}

	private IEnumerator GameStartTimeScaleCoroutine()
	{
		SoundController.Instance.PlayDialogSound (SFX.Ready);
		yield return new WaitForSeconds(0.2f);



		Time.timeScale = 0.3f;

//		while(Time.timeScale < 1)
//		{
//			Time.timeScale += 0.05f;

			yield return new WaitForSeconds(0.2f);
//		}
		SoundController.Instance.PlayDialogSound (SFX.Go);

		Time.timeScale = 1;
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

    private void InitializeBeats(){
        gameplayRefs.loudness.Initialize();
    }

    public void ChangeGameState(GameState state){
        gameState = state;
        switch(state){
            case GameState.Start:
                //print("Start Game");
                ResetGame();
                ResetScore();
                ProgressionCurves();
                SoundController.Instance.SetPitch(1f,false);
                SoundController.Instance.SetVolume(0.7f);
                SoundController.Instance.SetAudioTime(0.05f);
                SoundController.Instance.PlayMusic();
                playerController.ChangeState(GameState.Start);
                mainOrbitController.ChangeState(GameState.Start);
                gameplayTransitionController.ChangeState(GameState.Start);
                gameplayRefs.inputController.GameStart(true);
                SetHurdleFillAmount();
                SetRandomColor();
                break;
            case GameState.Revive:
                //print("Revive Game");
                ResetGame();
                ProgressionCurves();
                SoundController.Instance.SetPitch(1f, false);
                SoundController.Instance.SetVolume(0.7f);
                playerController.ChangeState(GameState.Start);
                mainOrbitController.ChangeState(GameState.Start);
                gameplayTransitionController.ChangeState(GameState.Start);
                gameplayRefs.inputController.GameStart(true);
                SetHurdleFillAmount();
                SetRandomColor();
                StartCoroutine(TimeScaleCoroutine());
                break;
            case GameState.Quit:
                //print("Game Over");
                playerController.ChangeState(GameState.Quit);
                mainOrbitController.ChangeState(GameState.Quit);
                gameplayTransitionController.ChangeState(GameState.Quit);
                gameplayRefs.inputController.GameStart(false);
                //ResetHurdleFillAmount();
                SoundController.Instance.SetPitch(0.95f, true);
                SoundController.Instance.SetVolume(0.25f);
                break;
            
        }
    }

    private IEnumerator TimeScaleCoroutine()
    {
        yield return new WaitForSeconds(0.2f);


        Time.timeScale = 0.1f;

        while(Time.timeScale < 1)
        {
            Time.timeScale += 0.05f;

            yield return new WaitForSeconds(0.05f);
        }

        Time.timeScale = 1;
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
        if(gameState == GameState.Start){
            normalModeTimer -= Time.deltaTime;
        }

        if(colorController != null && (gameState == GameState.Start || gameState == GameState.Revive)){
            ChangeColors();
        }

        if (gameplayViewController != null && gameState == GameState.Start)
            gameplayViewController.LookAtTransform(playerController.transform.position, Constants.cameraOffset);
        

		#if UNITY_ANDROID
          
		if (Input.GetKeyDown (KeyCode.Escape)) {
			EventManager.DoFireBackButtonEvent ();
		}

		#endif
    }

    private void SetHurdleFillAmount(){
        for (int i = 0; i < mainOrbitController.GetOrbits().Count;i++){
            SetIndividualHurdleFillAmount(orbitControllers[i]);
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

    private void ChangeHurdleFillAmount(){
        print("Fill Amount: " + Constants.hurdleFillAmount);
        orbitControllers[1].hurdleController.ChangeFillAmount();
    }

    public void PlayerHitHurdle()
    {
		SoundController.Instance.PlayDialogSound (SFX.GameOver);
        ExplosionParticles();
        ChangeGameState(GameState.Quit);
    }

    private void PlayerCollisions(){
        playerController.SetCollisions(gameplayRefs.playerCollision);
    }

    public void HurdleHitWall()
    {
        NormalMode();
		ChangeColors();
        if (IsNormalModeChanged())
        {
            SpecialMode();
        }
    }

    private void NormalMode()
    {

        orbitControllers = mainOrbitController.GetOrbits();
        gameplayViewController.HurdleHitWallTween();
        mainOrbitController.StartRotate();
        if (!specialMode)
        {
            ProgressionCurves();
            if (score >= Constants.hurdleFillChangeScore && !specialMode)
            {
                orbitControllers[1].StopRotate();
                ChangeHurdleFillAmount();
            }
        }
        else
        {

            hurdleCount++;

            if (hurdleCount >= 4)
            {
                SoundController.Instance.PlayDialogSound(SFX.LevelUp);
                ProgressionCurves();
                specialMode = false;
                mainOrbitController.StopScale();
                mainOrbitController.Scale();
            }
        }


        Constants.hurdlesDistance = Vector3.one * 10;
        AddScore(1);
        //float p = 1 + score / 1000f;
        //SoundController.Instance.SetPitch(p,false);

        //Applying progression settings
        if (addInitialDistance)
        {
            Constants.scaleSpeed /= 3;
        }

        mainOrbitController.SetNewScale(addInitialDistance, scaleSpeed);


        addInitialDistance = false;
        SetIndividualHurdleFillAmount(orbitControllers[0]); //Set the fill amount of this orbit

        mainOrbitController.AssignNewRotation();
        orbitControllers[0].StopSpecialRotation();
        mainOrbitController.SortHurdleOrbit();  //set first list element as last sibling in hierarchy
        mainOrbitController.SortOrbits();   //sort all the orbits 

    }

    bool specialMode = false;
    int hurdleCount = 0;

    private void SpecialMode()
    {
        hurdleCount = 0;
        specialMode = true;
        orbitHitId = orbitControllers[0].id;

        normalModeTimer = 1000;

        randomSpecialValue = Random.Range(1, 4);

        //randomSpecialValue = 2;

        addInitialDistance = true;
        mainOrbitController.StopScale();
        Constants.hurdlesDistance = Vector3.one;
        Constants.hurdleFillAmount = 0.55f;
        mainOrbitController.SetNewRotations(0, randomSpecialValue == (int)ModeType.AntiClockWise ? -1 : 1,randomSpecialValue == (int)ModeType.PingPongMode? 0f : 2f);
        mainOrbitController.StartRotate();
        for (int i = 0; i < mainOrbitController.GetOrbits().Count; i++)
        {
            SetIndividualHurdleFillAmount(orbitControllers[i]);
        }

        mainOrbitController.ScaleTo(Vector3.one * 5f,3).OnComplete(OnScaleComplete) ;
    }

    private void OnScaleComplete()
    {
        normalModeTimer = Random.Range(Constants.minNormalModeTime, Constants.maxNormalModeTime);

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
        mainOrbitController.SetPingPongRotation(0, 1, 3, false);
        Constants.scaleSpeed = 3f;
        scaleSpeed = Constants.scaleSpeed;
        mainOrbitController.Scale();
    }


    private void ProgressionCurves(){
        float x = score / Constants.difficultyLevel;
        //float value;

        Constants.hurdlesDistance = Vector3.one * gameplayRefs.hurdleDistanceCurve.Evaluate(x);// * gameplayRefs.maxHurdleDistance;
         //= Vector3.one * Mathf.Clamp(value, gameplayRefs.minHurdleDistance, gameplayRefs.maxHurdleDistance);

        Constants.hurdleFillAmount = gameplayRefs.hurdleFillAmountCurve.Evaluate(x);// * gameplayRefs.maxHurdleFillAmount;
        //Constants.hurdleFillAmount = Mathf.Clamp(value, gameplayRefs.minHurdleFillAmount, gameplayRefs.maxHurdleFillAmount);
        //print(x + " score and amount is "+ Constants.hurdleFillAmount);
        //value = gameplayRefs.maxScaleSpeed - gameplayRefs.scaleSpeedCurve.Evaluate(x) * gameplayRefs.maxScaleSpeed;
        //Constants.scaleSpeed = Mathf.Clamp(value, gameplayRefs.minScaleSpeed, gameplayRefs.maxScaleSpeed);
        Constants.scaleSpeed = gameplayRefs.scaleSpeedCurve.Evaluate(x);

        //value = gameplayRefs.rotationOffsetCurve.Evaluate(x) * gameplayRefs.maxRotationOffset;
        //Constants.rotationOffset = Mathf.Clamp(value, gameplayRefs.minRotationOffset, gameplayRefs.maxRotationOffset);
        Constants.rotationOffset = gameplayRefs.rotationOffsetCurve.Evaluate(x);

        //Max rotation curve and min value will be -x of max value
        //value = gameplayRefs.orbitRotationCurve.Evaluate(x) * gameplayRefs.maxOrbitRotateSpeed;
        //Constants.maxRotateSpeed = Mathf.Clamp(value, gameplayRefs.minOrbitRotateSpeed, gameplayRefs.maxOrbitRotateSpeed);
        //value = Constants.maxRotateSpeed - 5f;
        //Constants.minRotateSpeed = Mathf.Clamp(value, gameplayRefs.minOrbitRotateSpeed, gameplayRefs.maxOrbitRotateSpeed);
        Constants.maxRotateSpeed = gameplayRefs.orbitRotationCurve.Evaluate(x);
        Constants.minRotateSpeed = Constants.maxRotateSpeed - 1f;

        //Debug.Log("Score: " + x);
        //Debug.Log("Hurdle Distance: " + Constants.hurdlesDistance);
        //Debug.Log("Fillamount: " + Constants.hurdleFillAmount);
        //Debug.Log("scaleSpeed: " + Constants.scaleSpeed);
        //Debug.Log("rotationOffset: " + Constants.rotationOffset);
        //Debug.Log("maxRotateSpeed: " + Constants.maxRotateSpeed);
        //Debug.Log("minRotateSpeed: " + Constants.minRotateSpeed);


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
		if (PlayerData.IsHighScoreChanged () && !isHighScoreMade) {
			isHighScoreMade = true;
			SoundController.Instance.PlayDialogSound (SFX.HighScore);
		}
	
		EventManager.DoFireScoreUpdatedEvent ();
        gameplayViewController.SetScore(score);
    }

    private void ResetVariables(){
		isHighScoreMade = false;
        score = 0;
		PlayerData.CurrentScore = score;
        gameplayViewController.SetScore(score);
    }

    private void ChangeColors(){
        //ColorSet colorSet = colorController.GetRandomColorSet();
        colorTimer -= Time.deltaTime;
        if(colorTimer<=0){
            SetRandomColor();
            SetRandomColorTimer();
            //print("Colors Changed");
        }
        //ColorSet colorSet = colorController.GetIncrementalColorSet();
    }

    private void SetRandomColor(){
        ColorSet colorSet = colorController.GetRandomColorSet();
        gameplayViewController.ChangeColorSet(colorSet);
    }

    private void SetRandomColorTimer(){
        colorTimer = Random.Range(Constants.minColorTimer, Constants.maxColorTimer);
    }

    public bool IsNormalModeChanged()
    {
        return normalModeTimer <= 0 ? true : false;
    }

}
