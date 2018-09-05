﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayContoller : Singleton<GameplayContoller>, IController
{
    GameplayViewController gameplayViewController;

    public GameplayRefs gameplayRefs;

    public PlayerController playerController;
    public List<MyTargetController> targetIDs;
    public MainOrbitController orbitController;
    public ColorController colorController;
    public GameplayTransitionController gameplayTransitionController;
   
    [Range(0, 99)]
    public List<int> targetsProbabilty;
    [Range(0.025f, 0.25f)]
    public float targetFillAmount;
    //private Vector3 targetScreenPos;

    private GameState gameState;

    private bool isPerfectHit = false;   //perfect hit check passed by player
    private int score = 0;   //simple score
    private int level = 0;   //game level
    private int targetHitCount = 0;  //how many targets hit in order to level up
    private int comboCount = 0;  //chain of perfect hits
    private int comboTextCount = 0;
    private bool isAllowedToShot;
    private Vector3 playerShotPos;

    private bool isFirstTime = false;

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
        Application.targetFrameRate = 60;

        isFirstTime = true;

        InitializeGameplayViewController();
        InitializePlayer();
        InitializeTargetIDs();
        InitializeOrbits();
        InitializeColors();
        InitializeBeats();
        InitializeTransitionController();
        SpawnTargetsWithProbabilty(Constants.totalTargets);
        //SetTargetsSize();
        ChangeGameState(GameState.Start);
    }

    private void InitializeGameplayViewController(){
        gameplayViewController = new GameplayViewController(gameplayRefs);
    }

    private void InitializePlayer(){
        playerController.Initialize();
    }

    private void InitializeTargetIDs()
    {
        for (int i = 0; i < targetIDs.Count;i++){
            targetIDs[i].Id = i+1;
        }
    }

    private void InitializeOrbits(){
        orbitController.Initialize();
    }

    private void InitializeColors(){
        colorController = new ColorController();
        colorController.Initialize();
        //ChangeColors(); //call initially and then after level up
    }

    private void InitializeBeats(){
        gameplayRefs.loudness.Initialize();
    }

    private void InitializeTransitionController(){
        gameplayTransitionController.Initialize(gameplayRefs, playerController,orbitController,targetIDs);
    }

    public void ChangeGameState(GameState state){
        gameState = state;
        switch(state){
            case GameState.Start:
                SoundController.Instance.SetPitch(1f,false);
                SoundController.Instance.SetVolume(1f);
                ResetAvailableOrbitList();
                playerController.ChangeState(GameState.Start);
                orbitController.ChangeState(GameState.Start);
                if(!isFirstTime)
                    SpawnTargetsWithProbabilty(Constants.targetID); //if it is not the first time, then spawn based on target hit index
                gameplayTransitionController.LevelTransitionOnStart(isFirstTime);
                isFirstTime = false;
                print("Start Game");
                gameplayViewController.StopTimerWarningSequence();

                SetTargetsSize();
                break;
            case GameState.Restart:
                isFirstTime = true;
                SpawnTargetsWithProbabilty(Constants.totalTargets);//spawn all targets with probability
                ResetScoring();
                //ChangeColors();
                gameplayViewController.SetCenterOrbits(true);
                gameplayViewController.SetArrowAlpha(1f);
                print("Restart Game");
                ChangeGameState(GameState.Start);
                break;
            case GameState.End:
                SoundController.Instance.SetPitch(.5f,false);
                SoundController.Instance.SetVolume(1f);
                gameplayViewController.Flash(Color.white, Constants.flashTime);
                gameplayViewController.Shake(Constants.shakeTime);
                gameplayTransitionController.StopTimerMovement();
                playerController.ChangeState(GameState.End);
                orbitController.ChangeState(GameState.End);
                ResetCameraPosition();
                print("Game Over");
                gameplayViewController.SetCenterOrbits(false);
                MainMenuController.Instance.ActivateRestartBtn();
                gameplayViewController.SetScore("SCORE:" + score);
                gameplayTransitionController.LevelTransitionOnEnd();
                Vibration.Vibrate();
                targetFillAmount = Constants.maxTargetFillAmount;
                ResetAllTargetSize();
                break;
            case GameState.Shot:
                gameplayTransitionController.StopTimerMovement();
                playerController.ChangeState(GameState.Shot); //this initiates a player shot
                //orbitController.ChangeState(GameState.Shot);
                playerShotPos = playerController.GetPosition();
                //print(playerShotPos);
                break;
            case GameState.TargetHit:
                Scoring(isPerfectHit,Constants.targetID);
                playerController.ChangeState(GameState.TargetHit);
                orbitController.ChangeState(GameState.TargetHit);
                //for (int i = 0; i < targetsController.Count; i++){
                //    targetsController[i].ChangeState(GameState.TargetHit);
                //    gameplayTransitionController.LevelTransitionOnTargetHit(playerShotPos,i);
                //}
                //ChangeGameState(GameState.Start);
                gameplayTransitionController.LevelTransitionOnTargetHit(playerShotPos,Constants.targetID);
                Vibration.Vibrate();
                //ResetTargetSize();
                //SetTargetsSize();
                break;
        }
    }

    public void SetTargetIDs(List<MyTargetController> targetIDs){
        this.targetIDs = targetIDs;
        InitializeTargetIDs();
    }

    private void ChangeColors(){
        ColorSet colorSet = colorController.GetRandomColorSet();
        gameplayViewController.ChangeColorSet(colorSet);
        ArrowColor();
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
        if(gameplayViewController!=null && gameState == GameState.Start)
            gameplayViewController.LookAtTransform(playerController.transform.position, Constants.cameraOffset);
    }

    private void ResetCameraPosition(){
        gameplayViewController.LookAtTransform(Vector3.zero, Constants.cameraOffset);
    }

    public void PlayerCollidedWithTarget(int targetID){
        print("Player collided with target: " + targetID);
        isAllowedToShot = false;
        Constants.targetID = targetID;
        targetIDs[targetID - 1].gameObject.SetActive(false);
        SetTargetParticles();
        //isPerfectHit = perfectHit;
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
        ChangeGameState(GameState.End);
    }

    public void TimerWarning(){
        print("Player close to timer");
        gameplayViewController.TimerWarningSequence(Constants.timerWarningColor, Constants.warningSpeed);
    }

    //private void ChangeArrowAlpha(){
    //    float alpha = 1 - targetHitCount / 4f;
    //    gameplayViewController.SetArrowAlpha(alpha);
    //}

    private void SetTargetParticles(){
        Vector3 pos = new Vector3(playerController.transform.position.x, playerController.transform.position.y,0f);
        Instantiate(gameplayRefs.triangleParticles,pos,Quaternion.identity);
        Instantiate(gameplayRefs.hexagonParticles,pos, Quaternion.identity);
        //gameplayRefs.triangleParticles.transform.position = pos;
        //gameplayRefs.hexagonParticles.transform.position = pos;
        //gameplayRefs.triangleParticles.SetActive(true);
        //gameplayRefs.hexagonParticles.SetActive(true);
    }

    private void ArrowColor(){
        gameplayViewController.ChangeArrowColor();
    }

    private void Scoring(bool perfectHit, int targetID){
        //increment target hit in order to level up
        targetHitCount += 1;
        //if (targetHitCount <= 4)
            //ChangeArrowAlpha();

        if (CheckLevelUp())
        {
            LevelUp();
        }
        ////Add +5 and incr when perfect hit
        //if (perfectHit)
        //{
        //    SoundController.Instance.PlaySFXSound(SFX.PerfectHit);
        //    AddScore(5 + comboCount);
        //    SetPerfectHitText(GetPerfectHitArray());
        //    comboCount ++;
        //    comboTextCount++;
        //    gameplayViewController.Flash(gameplayRefs.flashColor, Constants.flashTime);
        //}
        //else
        //{ //else add +1 with 0 comboCount
        //    AddScore(1);
        //    comboCount = 0;
        //    comboTextCount = 0;
        //}
        AddScore(targetID);//add score based on target u hit, 1 for first orbit
        comboCount = 0;
        comboTextCount = 0;
        //ChangeColors();
        gameplayViewController.ScoreColor();

    }

    private string GetPerfectHitArray(){
        if (comboTextCount == Constants.perfectHitArray.Length)
            comboTextCount = 0;
        return Constants.perfectHitArray[comboTextCount];
    }

    //private void TargetOrbitAlpha(){
    //    int targetPos = targetController.GetOrbit();
    //    gameplayViewController.SetTargetOrbitAlpha(targetPos);
    //}



    private void AddScore(int s){
        score += s;
        gameplayViewController.SetScore(score.ToString());
    }

    private void SetPerfectHitText(string text){
        gameplayViewController.SetPerfectHit(text);
    }

    private void ResetScoring(){
        score = 0;
        targetHitCount = 0;
        level = 0;
        comboCount = 0;
        isPerfectHit = false;
        comboTextCount = 0;
        AddScore(0);
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

    public int GetOrbitIndex()
    {
        int random = Random.Range(0, Constants.availablePositions.Count);

        int orbit = Constants.availablePositions[random];

        Constants.availablePositions.RemoveAt(random);

        return orbit;
    }

    private void ResetAvailableOrbitList()
    {
        Constants.availablePositions = new List<int>() { 1, 2, 3 };//array of positions
    }

    private void SpawnTargetsWithProbabilty(int targetHitIndex){
        SortTargetProbabilities(targetHitIndex);
        int index = Constants.totalTargets - targetHitIndex;//targetHitIndex=1,totaltargets=7
        List<RectTransform> orbits = orbitController.GetOrbits();
        for (int i = index; i < Constants.totalTargets; i++)
        {
            GameObject target = orbits[i].Find("Target").gameObject;
            int ran = Random.Range(0, 100); //0 - 99 random number
            target.SetActive(false);
            if (ran < targetsProbabilty[i])
            { //if generated ran number is less than the orbit's target probabilty, then spawn it
                target.SetActive(true);
            }
        }
    }

    private GameObject GetClosestTarget(){
        List<RectTransform> orbits = orbitController.GetOrbits();
        for (int i = 0; i < orbits.Count; i++)
        {
            GameObject target = orbits[i].Find("Target").gameObject;
            if (target.activeInHierarchy)
                return target;
        }
        return null;

    }

    private void SortTargetProbabilities(int targetHitIndex)
    {
        for (int i = 0; i < targetHitIndex; i++)
        {
            int j;
            int key = targetsProbabilty[0];
            for (j = 0; j < targetsProbabilty.Count - 1; j++)
            {
                //shift left
                targetsProbabilty[j] = targetsProbabilty[j + 1];
            }
            targetsProbabilty[j] = key;
        }
    }

    //Set the targets size 
    private void SetTargetsSize(){
        for (int i = 0; i < targetIDs.Count;i++){
            targetIDs[i].SetSize(targetFillAmount);
        }
        targetFillAmount -= Constants.targetReduceAmount;
        targetFillAmount = Mathf.Clamp(targetFillAmount, Constants.minTargetFillAmount, Constants.maxTargetFillAmount);
    }

    private void ResetTargetSize()
    {
        //for (int i = 0; i < targetIDs.Count; i++)
        //{
        //    targetIDs[i].ResetSize();
        //}
        targetIDs[Constants.targetID-1].ResetSize();
    }

    private void ResetAllTargetSize()
    {
        for (int i = 0; i < targetIDs.Count; i++)
        {
            targetIDs[i].ResetSize();
        }
        //targetIDs[Constants.targetID - 1].ResetSize();
    }

}
