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

    [HideInInspector] public GameState gameState;

    private int score = 0;
    int randomSpecialValue = 0;
    bool addInitialDistance = false;
    float scaleSpeed = 1.5f;
    private float normalModeTimer;
    private float colorTimer;
    private int orbitHitId = 0;
    private bool hurdleFadeFlag = false;
    private float hurdleFadeTimer = Constants.minFadeTimer;
    private int hurdleFadeHitID = -1;
    private bool hurdleFillFlag = false;
    private float hurdleFillTimer = Constants.minFillTimer;
    private bool changeColorFlag = true;
    private bool reviveGame;
    private float beatValue = Constants.minBeatValue;
    private int level = 0;
    private int levelupMod;

    public bool Revive
    {
        set
        {
            reviveGame = value;
        }
    }

    private void ResetScore()
    {
        score = 0;
        level = 0;
        gameplayViewController.SetScore(score);
    }

    private void ResetGame()
    {
        PlayerData.CurrentScore = score;
        normalModeTimer = Random.Range(Constants.minNormalModeTime, Constants.maxNormalModeTime);
        gameplayViewController.SetScore(score);
        addInitialDistance = false;
        scaleSpeed = 0.18f;
        hurdleCount = 0;
        specialMode = false;
        ResetOrbitList();
        SetRandomColorTimer();
        ResetHurdleFillSettings();
        ResetHurdleFadeSettings();
        StopAllCoroutinesOnHurdles();
        StopFadeTweenOnHurdles();
        changeColorFlag = true;
    }

    private bool isHighScoreMade;

    public void Open()
    {
        Application.targetFrameRate = 60;
        goSoundCrWait = new WaitForSeconds(goSoundCrDelay);
        InitializeSoundEffect();
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
        //PlayerPrefs.DeleteKey("Tutorial");
        SoundController.Instance.SetVolume(0.7f);
        if(PlayerPrefs.HasKey("Tutorial")){
            //print("Has tutorial key");
            StartCoroutine(GameStartTimeScaleCoroutine());
        }
        else{
            SoundController.Instance.PlayDialogSound(SFX.Ready);
        }
    }

    private IEnumerator PlayGoSoundCR()
    {
        //		Time.timeScale = 0.1f;
        yield return goSoundCrWait;
        //		Time.timeScale = 1;
        SoundController.Instance.PlayDialogSound(SFX.Go);
    }

    private IEnumerator GameStartTimeScaleCoroutine()
    {
        SoundController.Instance.PlayDialogSound(SFX.Ready);
        yield return new WaitForSeconds(0.2f);



        Time.timeScale = 0.3f;

        //		while(Time.timeScale < 1)
        //		{
        //			Time.timeScale += 0.05f;

        yield return new WaitForSeconds(0.2f);
        //		}
        SoundController.Instance.PlayDialogSound(SFX.Go);

        Time.timeScale = 1;
    }

    private void InitializeGameplayControllers()
    {
        gameplayViewController = new GameplayViewController(gameplayRefs);
        gameplayTransitionController = new GameplayTransitionController(gameplayRefs);
    }

    private void InitializePlayer()
    {
        gameplayRefs.playerController.Initialize();
        playerController = gameplayRefs.playerController;
        PlayerCollisions();
    }

    private void InitializeHurdles()
    {
        hurdleControllers = gameplayRefs.hurdleControllers;
    }

    private void InitializeOrbit()
    {
        gameplayRefs.mainOrbitController.Initialize(gameplayRefs);
        mainOrbitController = gameplayRefs.mainOrbitController;
    }

    private void InitializeColors()
    {
        colorController = gameplayRefs.colorController;
    }

    private void InitializeBeats()
    {
        gameplayRefs.loudness.Initialize();
        ResetBeatValue();
    }

    public void ChangeGameState(GameState state)
    {
        gameState = state;
        switch (state)
        {
            case GameState.Start:
                //print("Start Game");
                ResetGame();
                ResetScore();
                ResetBeatValue();
                UpdateLevel();
                ProgressionCurves();
                SoundController.Instance.SetPitch(1f, true);
                SoundController.Instance.SetVolume(0.7f);
                //SoundController.Instance.SetAudioTime(0.03f);
                SoundController.Instance.PlayMusic();
                playerController.ChangeState(GameState.Start);
                gameplayTransitionController.ChangeState(GameState.Start);
                gameplayRefs.inputController.GameStart(true);
                CheckTutorial();
                break;
            case GameState.Revive:
                //print("Revive Game");
                ResetGame();
                CheckLevelForUpdate();
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
                SoundController.Instance.SetPitch(0f, true);
                //SoundController.Instance.SetVolume(0f);
                gameplayRefs.tutorialBtns.SetActive(false);
                break;

        }
    }

    private IEnumerator TimeScaleCoroutine()
    {
        yield return new WaitForSeconds(0.2f);


        Time.timeScale = 0.1f;

        while (Time.timeScale < 1)
        {
            Time.timeScale += 0.05f;

            yield return new WaitForSeconds(0.05f);
        }

        Time.timeScale = 1;
    }

    private void UpdateLevel(){
        level++;
        levelupMod = 5 * level + score;
        print(levelupMod+ " "+level);
    }

    private void CheckLevelForUpdate(){
        if(levelupMod == score){
            //means that the player died during special mode
            levelupMod += 5;
        }
    }

    private void CheckTutorial(){
        if (PlayerPrefs.HasKey("Tutorial"))
        {
            mainOrbitController.ChangeState(GameState.Start);
            SetHurdleFillAmount();
            SetRandomColor();
            tutorialComplete = true;
        }
        else
        {
            tutorialComplete = false;
            gameplayRefs.tutorialBtns.SetActive(true);
        }
    }

    public void ReviveGame()
    {
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

    bool tutorialComplete = false;

    public void TutorialBtn(){
        //tutorialComplete = true;
        if(!PlayerPrefs.HasKey("Tutorial")){
            SoundController.Instance.PlayDialogSound(SFX.Go);
            PlayerPrefs.SetInt("Tutorial", 1);
            tutorialComplete = true;
            mainOrbitController.ChangeState(GameState.Start);
            SetHurdleFillAmount();
            SetRandomColor();
        }

    }

    private void Update()
    {
        if (tutorialComplete)
        {
            if (gameState == GameState.Start || gameState == GameState.Revive)
            {
                normalModeTimer -= Time.deltaTime;
            }

            //the score is reached and is not special mode, so start timer and allow this Mode on timer complete
            if ((gameState == GameState.Start || gameState == GameState.Revive) && !specialMode && !hurdleFillFlag && IsHurdleFillScoreReached()){
                hurdleFillTimer -= Time.deltaTime;
                //print("hurdleFillTimer: " + hurdleFillTimer);
                if (hurdleFillTimer <= 0f){
                    hurdleFillFlag = true; //now the next hurdle that spawns at the last will get this ModeSettings in normalMode
                }
            }

            if ((gameState == GameState.Start || gameState == GameState.Revive) && !specialMode && !hurdleFadeFlag && IsHurdleFadeScoreReached())
            {
                hurdleFadeTimer -= Time.deltaTime;
                //print("hurdleFadeTimer: " + hurdleFadeTimer);
                if (hurdleFadeTimer <= 0f)
                {
                    hurdleFadeFlag = true;
                }
            }

            if (gameplayViewController != null && gameState == GameState.Start)
                gameplayViewController.LookAtTransform(playerController.transform.position, Constants.cameraOffset);

        }

        if (colorController != null && (gameState == GameState.Start || gameState == GameState.Revive) && changeColorFlag)
        {
            ChangeColors();
        }

        if(reviveGame)
        {
            reviveGame = false;
            ReviveGame();
        }
       

#if UNITY_ANDROID

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventManager.DoFireBackButtonEvent();
        }

#endif
    }

    private void SetHurdleFillAmount()
    {
        for (int i = 0; i < mainOrbitController.GetOrbits().Count; i++)
        {
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

    private void SetIndividualHurdleFillAmountWithTime(OrbitController orbit,float changeTime)
    {
        orbit.hurdleController.SetFillAmountWithTime(1f, changeTime);
    }

    public void PlayerHitHurdle()
    {
        SoundController.Instance.PlayDialogSound(SFX.GameOver);
        ExplosionParticles();
        ChangeGameState(GameState.Quit);
    }

    private void PlayerCollisions()
    {
        playerController.SetCollisions(gameplayRefs.playerCollision);
    }

    public void HurdleHitWall()
    {
        Vibration.Vibrate(20);
        //SoundController.Instance.PlaySFXSound(SFX.Pop);
        NormalMode();
        if (IsNormalModeScoreReached() && !specialMode)
        {
            SpecialMode();
        }

    }

    private void NormalMode()
    {

        orbitControllers = mainOrbitController.GetOrbits();
        gameplayViewController.HurdleHitWallTween();
        //changeColorFlag = true;
        orbitControllers[0].hurdleController.ResetFade();
        orbitControllers[0].StartRotate();
        //begin rotate of the fade orbit
        if(hurdleFadeHitID == orbitControllers[0].id){
            //orbitControllers[0].hurdleController.ResetFade();
            //orbitControllers[0].StartRotate();
            changeColorFlag = true;
        }

        if (!specialMode)
        {
            ProgressionCurves();
            AddScore(1);

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
                mainOrbitController.StartRotate();
                AddScore(5);
                UpdateBeatValue(1);
                UpdateLevel();
            }
        }
        Constants.hurdlesDistance = Vector3.one * 10;
       
        //timer has completed and is not in special mode and not in fade mode
        if(hurdleFillFlag && !specialMode){
            print("HurdleFillMode");
            SetIndividualHurdleFillAmountWithTime(orbitControllers[0], orbitControllers[0].GetHurdleScale().x * 3.25f * Constants.scaleSpeed); //Set the fill amount of this orbit
            ResetHurdleFillSettings();
        }
        else
            SetIndividualHurdleFillAmount(orbitControllers[0]); //Set the fill amount of this orbit


        //timer has completed and is not in special mode and not in fill mode
        if (hurdleFadeFlag && !specialMode)
        {
            HurdleFadeMode();
            ResetHurdleFadeSettings();
        }

        mainOrbitController.AssignNewRotation();
        orbitControllers[0].StopSpecialRotation();

        //Applying progression settings
        if (addInitialDistance)
        {
            Constants.scaleSpeed /= 3;
        }
        mainOrbitController.SetNewScale(addInitialDistance, scaleSpeed);
        addInitialDistance = false;
        mainOrbitController.SortHurdleOrbit();  //set first list element as last sibling in hierarchy
        mainOrbitController.SortOrbits();   //sort all the orbits 
    }

    private void HurdleFadeMode(){
        print("HurdleFadeMode");
        changeColorFlag = false;
        orbitControllers[0].hurdleController.StartFade();
        orbitControllers[0].StopRotate();
        hurdleFadeHitID = orbitControllers[0].id;
    }

    private void ResetHurdleFillSettings(){
        if (level % 4 == 0)
        {
            print("Fill Frequency high");
            hurdleFillTimer = Random.Range(Constants.minFillTimerHigh, Constants.maxFillTimerHigh);
        }
        else
        {
            print("Fill Frequency Low");
            hurdleFillTimer = Random.Range(Constants.minFillTimer, Constants.maxFillTimer);
        }

        hurdleFillFlag = false;

    }

    private void ResetHurdleFadeSettings(){
        if(level%2 == 0){
            print("Fade Frequency high");
            hurdleFadeTimer = Random.Range(Constants.minFadeTimerHigh, Constants.maxFadeTimerHigh);
        }
        else{
            print("Fade Frequency Low");
            hurdleFadeTimer = Random.Range(Constants.minFadeTimer, Constants.maxFadeTimer);
        }

        hurdleFadeFlag = false;

    }

    bool specialMode = false;
    int hurdleCount = 0;

    private void SpecialMode()
    {
        print("SpecialMode");
        //if fill mode was set to change and special mode came so we need to stop the coroutine
        StopAllCoroutinesOnHurdles();
        //Kill fade tween if applied on hurdle and special mode has arrived 
        StopFadeTweenOnHurdles();

        hurdleCount = 0;
        specialMode = true;
        orbitHitId = orbitControllers[0].id;

        normalModeTimer = Random.Range(Constants.minNormalModeTime, Constants.maxNormalModeTime);

        randomSpecialValue = Random.Range(1, 4);

        addInitialDistance = true;
        mainOrbitController.StopScale();
        Constants.hurdlesDistance = Vector3.one;
        Constants.hurdleFillAmount = 0.55f;
        mainOrbitController.SetNewRotations(0, randomSpecialValue == (int)ModeType.AntiClockWise ? -1 : 1, randomSpecialValue == (int)ModeType.PingPongMode ? 0f : 2f);
        mainOrbitController.StartRotate();
        for (int i = 0; i < mainOrbitController.GetOrbits().Count; i++)
        {
            SetIndividualHurdleFillAmount(orbitControllers[i]);
        }

        mainOrbitController.ScaleTo(Vector3.one * 5f, 3).OnComplete(OnScaleComplete);

    }

    private void StopAllCoroutinesOnHurdles(){
        for (int i = 0; i < orbitControllers.Count;i++){
            orbitControllers[i].hurdleController.StopAllCoroutines();
        }
    }

    private void StopFadeTweenOnHurdles()
    {
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            orbitControllers[i].hurdleController.ResetFade();
        }
    }

    private void OnScaleComplete()
    {
        normalModeTimer = Random.Range(Constants.minNormalModeTime, Constants.maxNormalModeTime);

        DecideSpecialMode();
    }

    private void DecideSpecialMode(){
        switch (randomSpecialValue)
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

        Constants.hurdlesDistance = Vector3.one * gameplayRefs.hurdleDistanceCurve.Evaluate(x);// * gameplayRefs.maxHurdleDistance;
        Constants.hurdleFillAmount = gameplayRefs.hurdleFillAmountCurve.Evaluate(x);// * gameplayRefs.maxHurdleFillAmount;
        Constants.scaleSpeed = gameplayRefs.scaleSpeedCurve.Evaluate(x);
        Constants.rotationOffset = gameplayRefs.rotationOffsetCurve.Evaluate(x);
        Constants.maxRotateSpeed = gameplayRefs.orbitRotationCurve.Evaluate(x);
        Constants.minRotateSpeed = Constants.maxRotateSpeed - 1f;


    }

    private void UpdateBeatValue(float value){
        beatValue += value;
        beatValue = Mathf.Clamp(beatValue, Constants.minBeatValue, Constants.maxBeatValue);
        gameplayRefs.loudness.ChangeCamZoomEndValue(beatValue);
    }

    private void ResetBeatValue(){
        beatValue = Constants.minBeatValue;
        gameplayRefs.loudness.ChangeCamZoomEndValue(beatValue);
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

    public bool IsNormalModeScoreReached()
    {
        return score % levelupMod == 0 ? true : false;
    }

    public bool IsHurdleFadeScoreReached()
    {
        return score >= Constants.fadeScore ? true : false;
    }

    public bool IsHurdleFillScoreReached()
    {
        return score >= Constants.fillScore ? true : false;
    }

}
