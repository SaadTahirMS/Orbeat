﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameplayTransitionController : MonoBehaviour {

    TargetController targetController;
    PlayerController playerController;
    OrbitController orbitController;
    //public Image targetImg;

    public Beat scoreBeat;
    public Text scoreText;

    public Transform playerOrbit;

    private Sequence levelTransitionOnStartSeq; //called when game starts at the beginning or after the level transition
    private Sequence levelTransitionOnTargetHitSeq;   //called on target hit
    private Sequence timerMovement;
    private Sequence levelTransitionOnEndSeq;

    List<Vector3> initialScales = new List<Vector3>();

    //private TargetController currentTargetController;

    public void Initialize(TargetController targetController,PlayerController playerController,OrbitController orbitController){
        this.targetController = targetController;
        this.playerController = playerController;
        this.orbitController = orbitController;
    }

    public void LevelTransitionOnStart(bool isFirstTime = false){
        
        StopLevelTransitionOnStart();
        StopLevelTransitionOnEnd();
        levelTransitionOnStartSeq = DOTween.Sequence();

        CheckOrbitScale();
        SetOrbitIndividualScales();
        //SoundController.Instance.SetPitch(1);

        //Create tweens 
        //Target tweens
        targetController.gameObject.SetActive(true);
        Tween targetScaleTween = TargetScale();
        //Tween targetPositionTween = TargetPosition(targetController.Position);
        targetController.transform.localPosition = targetController.Position; 
        Tween targetFadeInTween = TargetFadeIn();
        //Player tweens
        playerController.gameObject.SetActive(true);
        Tween playerScaleTween = PlayerScale();

        Tween playerPositionTween;

        if (isFirstTime)
        {
            playerPositionTween = PlayerPosition(playerController.Position, Constants.transitionTime);
        }
        else
        {
            playerPositionTween = PlayerPosition(playerController.Position, 0);
        }

        //Orbits tweens
        //orbits.localPosition = Vector3.zero;
        //ResetOrbitScale();
        Tween orbitsScaleTween = OrbitsScale(Vector3.one);
        //PlayerOrbit 
        playerOrbit.localScale = Vector3.one*0.5f;
        //Score
        Tween scoreScale = InitialScoreScale(Constants.scoreInitialScale);
        ScoreScale(Constants.scoreInitialScaleWH);

        //InitialScoreScale(Constants.scoreInitialScale);
        Tween scorePosition = ScorePosition(Constants.scoreInitialPosition);
        //Add Tweens to Sequence
        levelTransitionOnStartSeq.Append(targetFadeInTween)
        .Join(targetScaleTween)
        //.Join(targetPositionTween)
        .Join(playerScaleTween)
        .Join(playerPositionTween)
        //.Join(orbitsScaleTween)
        .Join(scoreScale)
        .Join(scorePosition);
        levelTransitionOnStartSeq.SetEase(Ease.Linear)
        .OnComplete(()=>StartTransitionComplete())
        .Play();

    }



    private void StopLevelTransitionOnStart()
    {
        levelTransitionOnStartSeq.Kill();
    }

    private Tween TargetScale(){
        targetController.transform.localScale = Vector3.zero; 
        return targetController.transform.DOScale(Vector3.one, Constants.transitionTime);
    }

    private Tween TargetPosition(Vector3 pos)
    {
        //target.transform.localPosition = Vector3.zero; 
        return  targetController.transform.DOLocalMove(pos, Constants.transitionTime);
    }

    private Tween TargetFadeIn()
    {
        Image targetImg = targetController.GetComponent<Image>();
        return targetImg.DOFade(1, Constants.transitionTime);
    }

   

    private Tween PlayerScale()
    {
        playerController.transform.localScale = Vector3.zero; 
        return playerController.transform.DOScale(Vector3.one, Constants.transitionTime);
    }

    private Tween PlayerPosition(Vector3 pos,float transitionTime)
    {
        playerController.transform.localPosition = Vector3.zero; 
        return playerController.transform.DOLocalMove(pos, transitionTime);
    }

    private Tween OrbitsScale(Vector3 scale)
    {
        return orbitController.transform.DOScale(scale, Constants.transitionTime);
    }

    private void ResetOrbitScale(){
        orbitController.transform.localScale = Vector3.zero;
    }

    private void ScoreBeat(){
        scoreBeat.DoBeat(Constants.scoreInitialScale,Constants.scoreBeatScale,Constants.scoreBeatTime, 1);    //plays the infinite beating
    }

    private Tween InitialScoreScale(Vector3 value){
        return scoreText.transform.DOScale(value, Constants.transitionTime);
    }

    private void ScoreScale(Vector2 value){
        scoreText.rectTransform.sizeDelta = value;
        //return scoreText.transform.DOScale(value, Constants.transitionTime);
    }

    private Tween ScorePosition(Vector3 value){
        return scoreText.transform.DOLocalMove(value, Constants.transitionTime);
    }

    private Tween ScoreAlpha()
    {
        return scoreText.DOFade(1f, Constants.transitionTime);
    }

    private void StartTransitionComplete(){
        GameplayContoller.Instance.IsAllowedToShot = true;
        GameplayContoller.Instance.playerController.SetCollisions(true);

        //SetOrbitIndividualScales(orbitController);

        ScoreBeat();
        TimerMovement();
    }

    private void SetOrbitIndividualScales(){
        //Orbits individual scales
        List<Transform> orbitsTransform = orbitController.GetOrbits();
        for (int i = 0; i < orbitsTransform.Count; i++)
        {
            initialScales.Add(orbitController.GetCurrentScale(i));
            //print(i + " : " + initialScales[i]);
            //orbitsTransform[i].localScale = initialScales[i];
            orbitsTransform[i].DOScale(initialScales[i], .1f);

        }
    }

    private void TimerMovement(){
        StopTimerMovement();
        timerMovement = DOTween.Sequence();
        //Player Orbit tweens
        Tween playerOrbitScale = PlayerOrbitScale();
        //Player Movement tweens
        Tween playerMovement = PlayerMovement();
        Tween targetMovement = TargetMovement();

        timerMovement.Append(playerOrbitScale);
        timerMovement.Join(playerMovement);
        timerMovement.Join(targetMovement);

        //Orbits Scale
        List<Transform> orbitScales = orbitController.GetOrbits();//list of orbits in the game
        for (int i = 0; i < orbitScales.Count;i++){
            Vector3 scale = orbitController.GetCurrentScale(i);
            Vector3 value = scale - Vector3.one * 0.5f;
            timerMovement.Join(orbitScales[i].DOScale(value, Constants.orbitsScaleSpeed));
            //timerMovement.Join(orbitScales[i].DOScale(Vector3.one*0.5f, Constants.playerOrbitScaleSpeed));

        }
        timerMovement.Play();
    }

    public void StopTimerMovement(){
        timerMovement.Kill();
    }


    private Tween PlayerOrbitScale()
    {
        playerOrbit.localScale = Vector3.one*0.5f;
        return playerOrbit.DOScale(Vector3.zero, Constants.playerOrbitScaleSpeed);
    }

    private Tween PlayerMovement(){
        return playerController.transform.DOLocalMove(Vector3.zero, Constants.playerMoveSpeed);
    }

    private Tween TargetMovement()
    {
        float speed = Constants.targetMoveSpeed + (Constants.targetMoveSpeed * targetController.GetOrbit());
        return targetController.transform.DOLocalMove(Vector3.zero, speed);
    }

    //public void LevelTransitionOnTargetHit(Vector3 targetScreenPos){

    public void LevelTransitionOnTargetHit(TargetController targetController,OrbitController orbitController,Vector3 playerShotPos)
    {
        int targetOrbitPos = targetController.GetOrbit();
        StopLevelTransitionOnTargetHit();

        levelTransitionOnTargetHitSeq = DOTween.Sequence();

        ////Create tweens
        ////Target tweens
        //Tween targetMoveToCenterTween = TargetMoveToCenter();
        ////Tween targetFadeOutTween = TargetFadeOut();
        ////Player tweens
        //Tween playerMoveToCenterTween = PlayerMoveToCenter();
        //Tween playerScaleToZeroTween = PlayerScaleToZero();
        ////Orbit tweens
        //Vector3 direction = GetDirection(targetScreenPos);
        //Tween orbitMoveToTarget = OrbitMoveToTarget(direction.x,direction.y);
        //Tween orbitScale = OrbitsScale(Vector3.one * 2);
        ////Add tweens
        //levelTransitionOnTargetHitSeq.Append(targetMoveToCenterTween)
        ////.Join(targetFadeOutTween)
        //.Join(playerMoveToCenterTween)
        //.Join(playerScaleToZeroTween)
        //.Join(orbitMoveToTarget)
        //.Join(orbitScale)
        //.SetEase(Ease.Linear)
        //.OnComplete(TargetHitTransitionComplete)
        //.Play();

        List<Transform> orbitsTransform = orbitController.GetOrbits();
        //Tween playerScaleToZeroTween = PlayerScaleToZero();
        Tween targetScaleToValueTween = TargetScaleToValue(0.5f);
        Tween targetMoveToPositionTween = TargetMoveToPosition(playerShotPos);
        levelTransitionOnTargetHitSeq.Append(playerOrbit.DOScale(0f, Constants.transitionTime));
        playerController.gameObject.SetActive(false);
        levelTransitionOnTargetHitSeq.Join(targetMoveToPositionTween);
        levelTransitionOnTargetHitSeq.Join(targetScaleToValueTween);
        //levelTransitionOnTargetHitSeq.Join(playerScaleToZeroTween);
        //Setting pitch to zero that will result in stopping beats
        //SoundController.Instance.SetPitch(0);
        //scale down till the target orbit pos
        Vector3 scaleValue = Constants.orbitsDistance * targetOrbitPos; //if pos is 3 then 1.5 of scale will be reduced of each orbit
        orbitController.StopBeats();
        for (int loopCount = 0; loopCount < targetOrbitPos; loopCount++)
        {
            for (int i = 0; i < orbitsTransform.Count; i++)
            {
                //Vector3 scale = orbitsTransform[i].transform.localScale - scaleValue;
                Vector3 scale = initialScales[i] - scaleValue;
                if (scale.y <= 0f)
                    orbitsTransform[i].GetComponent<Image>().DOFade(0f, 1f);
                Tween scaleTween = orbitController.ScaleDown(i, scale);
                levelTransitionOnTargetHitSeq.Join(scaleTween);
            }

            levelTransitionOnTargetHitSeq.SetEase(Ease.Linear);
            levelTransitionOnTargetHitSeq.OnComplete(() => TargetHitTransitionComplete());
            levelTransitionOnTargetHitSeq.Play();
        }
        //RecursiveTransition(targetOrbitPos, orbitController);
    }

    //private void RecursiveTransition(int targetOrbitPos, OrbitController orbitController)
    //{
    //    if (targetOrbitPos <= 0)
    //    {
    //        GameplayContoller.Instance.ChangeGameState(GameState.Start);
    //        return;
    //    }
    //    List<Transform> orbitsTransform = orbitController.GetOrbits();

    //    for (int i = 0; i < orbitsTransform.Count; i++)
    //    {
    //        Vector3 scale = orbitsTransform[i].transform.localScale - Constants.orbitsDistance;
    //        Tween scaleTween = orbitController.ScaleDown(i, scale);
    //        levelTransitionOnTargetHitSeq.Join(scaleTween);
    //    }

    //    levelTransitionOnTargetHitSeq.SetEase(Ease.Linear);
    //    levelTransitionOnTargetHitSeq.OnComplete(delegate {

    //        TargetHitTransitionComplete(orbitController, orbitsTransform);
    //        Dummy(targetOrbitPos, orbitController);
    //    });
    //    levelTransitionOnTargetHitSeq.Play();

    //}

    //private void Dummy(int targetOrbitPos, OrbitController orbitController)
    //{
    //    levelTransitionOnTargetHitSeq.Kill();
    //    RecursiveTransition(--targetOrbitPos, orbitController);
    //}

    private void StopLevelTransitionOnTargetHit(){
        levelTransitionOnTargetHitSeq.Kill();
    }

    private Tween TargetMoveToPosition(Vector3 pos)
    {
        return targetController.transform.DOLocalMove(pos, Constants.transitionTime);
    }

    private Tween TargetFadeOut()
    {
        Image targetImg = targetController.GetComponent<Image>();
        return targetImg.DOFade(0, Constants.transitionTime);
    }

    private Tween TargetScaleToValue(float value)
    {
        return targetController.transform.DOScale(Vector3.one*value, Constants.transitionTime);
    }

    private Tween PlayerMoveToCenter()
    {
        return playerController.transform.DOLocalMove(Vector3.zero, Constants.transitionTime);
    }

    private Tween PlayerScaleToZero()
    {
        return playerController.transform.DOScale(Vector3.zero, Constants.transitionTime);
    }

    private Vector3 GetDirection(Vector3 pos)
    {
        return new Vector3(Screen.width / 2 - pos.x, Screen.height / 2 - pos.y, 0f);
    }

    private Tween OrbitMoveToTarget(float x, float y)
    {
        return orbitController.transform.DOLocalMove(new Vector3(x * 10, y * 10, 0f), Constants.transitionTime);
    }

    private void TargetHitTransitionComplete(){
        SortOrbits();
        CheckOrbitScale();
        GameplayContoller.Instance.ChangeGameState(GameState.Start);
    }

    private void SortOrbits(){
        List<Transform> orbitTransforms = orbitController.GetOrbits();
        for (int i = 0; i < targetController.GetOrbit();i++){
            int j;
            Transform key = orbitTransforms[0];
            for (j = 0; j < orbitTransforms.Count-1; j++)
            {
                //shift left
                orbitTransforms[j] = orbitTransforms[j + 1];
            }
            orbitTransforms[j] = key;
        }
        orbitController.SetOrbits(orbitTransforms);
    }

    private void CheckOrbitScale(){

        List<Transform> orbitsTransform = orbitController.GetOrbits();
        for (int i = 0; i < orbitsTransform.Count;i++){

            if (orbitsTransform[i].transform.localScale.x <= Constants.orbitResetScale.x)
            {
                //so this is the one to be set to initial scale
                orbitsTransform[i].GetComponent<Image>().DOFade(0f, Constants.transitionTime);
                //orbitsTransform[i].gameObject.SetActive(false);
                orbitsTransform[i].transform.localScale = orbitController.GetCurrentScale(i) + Constants.intialOrbitScale;
                //orbitsTransform[i].gameObject.SetActive(true);
                orbitsTransform[i].GetComponent<Image>().DOFade(1f, 5f);
                //orbitsTransform[i].transform.DOScale(Constants.intialOrbitScale.x, Constants.transitionTime);

            }
        }
    }

    public void LevelTransitionOnEnd(){
        StopLevelTransitionOnEnd();
        levelTransitionOnEndSeq = DOTween.Sequence();
        playerController.gameObject.SetActive(false);
        targetController.gameObject.SetActive(false);
        scoreBeat.StopBeat();
        Tween scorePosition = ScorePosition(Constants.scoreGameOverPos);
        //Tween scoreScale = ScoreScale(Constants.scoreGameOverScale);
        ScoreScale(Constants.scoreGameOverScale);
        Tween scoreAlpha = ScoreAlpha();
        levelTransitionOnEndSeq
        .Append(scorePosition)
        //.Join(scoreScale)
        .Join(scoreAlpha)
        .SetEase(Ease.Linear)
        .Play();
    }

    private void StopLevelTransitionOnEnd(){
        levelTransitionOnEndSeq.Kill();
    }

}
