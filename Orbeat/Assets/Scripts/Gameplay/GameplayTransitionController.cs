using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameplayTransitionController : MonoBehaviour {

    public Transform target;
    public Transform player;
    public Transform orbits;
    public Image targetImg;

    public Beat scoreBeat;
    public Text scoreText;

    public Transform playerOrbit;

    private Sequence levelTransitionOnStartSeq; //called when game starts at the beginning or after the level transition
    private Sequence levelTransitionOnTargetHitSeq;   //called on target hit
    private Sequence timerMovement;
    private Sequence levelTransitionOnEndSeq;

    public void LevelTransitionOnStart(Vector3 targetPos,Vector3 playerPos,Vector3 orbitPos){
        StopLevelTransitionOnStart();
        StopLevelTransitionOnEnd();
        levelTransitionOnStartSeq = DOTween.Sequence();

        //Create tweens 
        //Target tweens
        target.gameObject.SetActive(true);
        Tween targetScaleTween = TargetScale();
        Tween targetPositionTween = TargetPosition(targetPos);
        Tween targetFadeInTween = TargetFadeIn();
        //Player tweens
        player.gameObject.SetActive(true);
        Tween playerScaleTween = PlayerScale();
        Tween playerPositionTween = PlayerPosition(playerPos);
        //Orbits tweens
        orbits.localPosition = Vector3.zero;
        Tween orbitsScaleTween = OrbitsScale();
        //PlayerOrbit 
        playerOrbit.localScale = Vector3.one;
        //Score
        ScoreBeat();
        Tween scoreScale = ScoreScale(Vector3.one);
        Tween scorePosition = ScorePosition(Vector3.zero);

        //Add Tweens to Sequence
        levelTransitionOnStartSeq.Append(targetFadeInTween)
        .Join(targetScaleTween)
        .Join(targetPositionTween)
        .Join(playerScaleTween)
        .Join(playerPositionTween)
        .Join(orbitsScaleTween)
        .Join(scoreScale)
        .Join(scorePosition)
        .SetEase(Ease.Linear)
        .OnComplete(StartTransitionComplete)
        .Play();

    }

    private void StopLevelTransitionOnStart()
    {
        levelTransitionOnStartSeq.Kill();
    }

    private Tween TargetScale(){
        target.transform.localScale = Vector3.zero; 
        return target.DOScale(Vector3.one, Constants.transitionTime);
    }

    private Tween TargetPosition(Vector3 pos)
    {
        target.transform.localPosition = Vector3.zero; 
        return  target.transform.DOLocalMove(pos, Constants.transitionTime);
    }

    private Tween TargetFadeIn()
    {
        return targetImg.DOFade(1, Constants.transitionTime);
    }

    private Tween PlayerScale()
    {
        player.transform.localScale = Vector3.zero; 
        return player.DOScale(Vector3.one, Constants.transitionTime);
    }

    private Tween PlayerPosition(Vector3 pos)
    {
        player.transform.localPosition = Vector3.zero; 
        return player.transform.DOLocalMove(pos, Constants.transitionTime);
    }

    private Tween OrbitsScale()
    {
        orbits.localScale = Vector3.zero;
        return orbits.DOScale(Vector3.one, Constants.transitionTime/2);
    }

    private void ScoreBeat(){
        scoreBeat.DoBeat(Constants.beatScale,Constants.scoreBeatTime, 1);    //plays the infinite beating
    }

    private Tween ScoreScale(Vector3 value){
        return scoreText.transform.DOScale(value, Constants.transitionTime);
    }

    private Tween ScorePosition(Vector3 value){
        return scoreText.transform.DOLocalMove(value, Constants.transitionTime);
    }

    private void StartTransitionComplete(){
        GameplayContoller.Instance.IsAllowedToShot = true;
        GameplayContoller.Instance.playerController.SetCollisions(true);
        TimerMovement();
    }

    private void TimerMovement(){
        StopTimerMovement();
        timerMovement = DOTween.Sequence();
        //Player Orbit tweens
        Tween playerOrbitScale = PlayerOrbitScale();
        //Player Movement tweens
        Tween playerMovement = PlayerMovement();

        timerMovement.Append(playerOrbitScale);
        timerMovement.Join(playerMovement);

        timerMovement.Play();
    }

    public void StopTimerMovement(){
        timerMovement.Kill();
    }

    private Tween PlayerOrbitScale()
    {
        playerOrbit.localScale = Vector3.one;
        return playerOrbit.DOScale(Vector3.zero, Constants.playerOrbitScaleSpeed);
    }

    private Tween PlayerMovement(){
        return player.transform.DOLocalMove(Vector3.zero, Constants.playerMoveSpeed);
    }

    public void LevelTransitionOnTargetHit(Vector3 targetScreenPos){
        StopLevelTransitionOnTargetHit();

        levelTransitionOnTargetHitSeq = DOTween.Sequence();

        //Create tweens
        //Target tweens
        Tween targetMoveToCenterTween = TargetMoveToCenter();
        Tween targetFadeOutTween = TargetFadeOut();
        //Player tweens
        Tween playerMoveToCenterTween = PlayerMoveToCenter();
        Tween playerScaleToZeroTween = PlayerScaleToZero();
        //Orbit tweens
        Vector3 direction = GetDirection(targetScreenPos);
        Tween orbitMoveToTarget = OrbitMoveToTarget(direction.x,direction.y);

        //Add tweens
        levelTransitionOnTargetHitSeq.Append(targetMoveToCenterTween)
        .Join(targetFadeOutTween)
        .Join(playerMoveToCenterTween)
        .Join(playerScaleToZeroTween)
        .Join(orbitMoveToTarget)
        .SetEase(Ease.Linear)
        .OnComplete(TargetHitTransitionComplete)
        .Play();


    }

    private void StopLevelTransitionOnTargetHit(){
        levelTransitionOnTargetHitSeq.Kill();
    }

    private Tween TargetMoveToCenter()
    {
        return target.DOLocalMove(Vector3.zero, Constants.transitionTime);
    }

    private Tween TargetFadeOut()
    {
        return targetImg.DOFade(0, Constants.transitionTime);
    }

    private Tween PlayerMoveToCenter()
    {
        return player.DOLocalMove(Vector3.zero, Constants.transitionTime);
    }

    private Tween PlayerScaleToZero()
    {
        return player.DOScale(Vector3.zero, Constants.transitionTime);
    }

    private Vector3 GetDirection(Vector3 pos)
    {
        return new Vector3(Screen.width / 2 - pos.x, Screen.height / 2 - pos.y, 0f);
    }

    private Tween OrbitMoveToTarget(float x, float y)
    {
        return orbits.DOLocalMove(new Vector3(x * 5, y * 5, 0f), Constants.transitionTime);
    }

    private void TargetHitTransitionComplete(){
        GameplayContoller.Instance.ChangeGameState(GameState.Start);
    }

    public void LevelTransitionOnEnd(){
        StopLevelTransitionOnEnd();
        levelTransitionOnEndSeq = DOTween.Sequence();

        player.gameObject.SetActive(false);
        target.gameObject.SetActive(false);

        scoreBeat.StopBeat();
        Tween scorePosition = ScorePosition(Constants.scoreGameOverPos);
        Tween scoreScale = ScoreScale(Constants.scoreGameOverScale);
        levelTransitionOnEndSeq
        .Append(scorePosition)
        .Join(scoreScale)
        .SetEase(Ease.Linear)
        .Play();
    }

    private void StopLevelTransitionOnEnd(){
        levelTransitionOnEndSeq.Kill();
    }

}
