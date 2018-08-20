using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameplayTransitionController : MonoBehaviour {

    public Transform orbits;
    public Image targetImg;

    public Beat scoreBeat;
    public Text scoreText;

    public Transform playerOrbit;

    private Sequence levelTransitionOnStartSeq; //called when game starts at the beginning or after the level transition
    private Sequence levelTransitionOnTargetHitSeq;   //called on target hit
    private Sequence levelTransitionOnEndSeq;

    public void LevelTransitionOnStart(TargetController target,PlayerController player,OrbitController orbit){
        StopLevelTransitionOnStart();
        StopLevelTransitionOnEnd();
        levelTransitionOnStartSeq = DOTween.Sequence();
        
        //Create tweens 
        //Target tweens
        target.gameObject.SetActive(true);
        Tween targetScaleTween = TargetScale(target.transform);
        Tween targetPositionTween = TargetPosition(target.transform,target.Position);
        Tween targetFadeInTween = TargetFadeIn();
        //Player tweens
        player.gameObject.SetActive(true);
        Tween playerScaleTween = PlayerScale(player.transform);
        Tween playerPositionTween = PlayerPosition(player.transform,player.Position);
        //Orbits tweens
        orbits.localPosition = Vector3.zero;
        ResetOrbitScale();
        Tween orbitsScaleTween = OrbitsScale(Vector3.one);
        //PlayerOrbit 
        playerOrbit.localScale = Vector3.one;
        //Score
        Tween scoreScale = ScoreScale(Constants.scoreInitialScale);
        Tween scorePosition = ScorePosition(Constants.scoreInitialPosition);

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
        .OnComplete(() => StartTransitionComplete(player,target,orbit))
        .Play();

    }

    private void StopLevelTransitionOnStart()
    {
        levelTransitionOnStartSeq.Kill();
    }

    private Tween TargetScale(Transform target){
        target.transform.localScale = Vector3.zero; 
        return target.DOScale(Vector3.one, Constants.transitionTime);
    }

    private Tween TargetPosition(Transform target,Vector3 pos)
    {
        target.transform.localPosition = Vector3.zero; 
        return  target.transform.DOLocalMove(pos, Constants.transitionTime);
    }

    private Tween TargetFadeIn()
    {
        return targetImg.DOFade(1, Constants.transitionTime);
    }

    private Tween PlayerScale(Transform player)
    {
        player.transform.localScale = Vector3.zero; 
        return player.DOScale(Vector3.one, Constants.transitionTime);
    }

    private Tween PlayerPosition(Transform player,Vector3 pos)
    {
        player.transform.localPosition = Vector3.zero; 
        return player.transform.DOLocalMove(pos, Constants.transitionTime);
    }

    private Tween OrbitsScale(Vector3 scale)
    {
        return orbits.DOScale(scale, Constants.transitionTime);
    }

    private void ResetOrbitScale(){
        orbits.localScale = Vector3.zero;
    }

    private void ScoreBeat(){
        scoreBeat.DoBeat(Constants.scoreInitialScale,Constants.scoreBeatScale,Constants.scoreBeatTime, 1);    //plays the infinite beating
    }

    private Tween ScoreScale(Vector3 value){
        return scoreText.transform.DOScale(value, Constants.transitionTime);
    }

    private Tween ScorePosition(Vector3 value){
        return scoreText.transform.DOLocalMove(value, Constants.transitionTime);
    }

    private Tween ScoreAlpha()
    {
        return scoreText.DOFade(1f, Constants.transitionTime);
    }

    private void StartTransitionComplete(PlayerController player,TargetController target,OrbitController orbit){
        GameplayContoller.Instance.IsAllowedToShot = true;
        GameplayContoller.Instance.playerController.SetCollisions(true);
        ScoreBeat();
        player.Movement();
        target.Movement();
    }

    public void LevelTransitionOnTargetHit(PlayerController player,TargetController target){
        StopLevelTransitionOnTargetHit();
        levelTransitionOnTargetHitSeq = DOTween.Sequence();
        //Target tweens
        //Tween targetMoveToCenterTween = TargetMoveToCenter(target.transform);
        ////Tween targetFadeOutTween = TargetFadeOut();
        ////Player tweens
        //Tween playerMoveToCenterTween = PlayerMoveToCenter(player.transform);
        //Tween playerScaleToZeroTween = PlayerScaleToZero(player.transform);
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
        // .Join(orbitScale)
        //.SetEase(Ease.Linear)
        //.OnComplete(TargetHitTransitionComplete)
        //.Play();
        levelTransitionOnTargetHitSeq.Play();
    }

    private void StopLevelTransitionOnTargetHit(){
        levelTransitionOnTargetHitSeq.Kill();
    }

    private Tween TargetMoveToCenter(Transform target)
    {
        return target.DOLocalMove(Vector3.zero, Constants.transitionTime);
    }

    private Tween TargetFadeOut()
    {
        return targetImg.DOFade(0, Constants.transitionTime);
    }

    private Tween PlayerMoveToCenter(Transform player)
    {
        return player.DOLocalMove(Vector3.zero, Constants.transitionTime);
    }

    private Tween PlayerScaleToZero(Transform player)
    {
        return player.DOScale(Vector3.zero, Constants.transitionTime);
    }

    private Vector3 GetDirection(Vector3 pos)
    {
        return new Vector3(Screen.width / 2 - pos.x, Screen.height / 2 - pos.y, 0f);
    }

    private Tween OrbitMoveToTarget(float x, float y)
    {
        return orbits.DOLocalMove(new Vector3(x * 10, y * 10, 0f), Constants.transitionTime);
    }

    private void TargetHitTransitionComplete(){
        GameplayContoller.Instance.ChangeGameState(GameState.Start);
    }

    public void LevelTransitionOnEnd(PlayerController player,TargetController target){
        StopLevelTransitionOnEnd();
        levelTransitionOnEndSeq = DOTween.Sequence();
        player.gameObject.SetActive(false);
        target.gameObject.SetActive(false);
        scoreBeat.StopBeat();
        Tween scorePosition = ScorePosition(Constants.scoreGameOverPos);
        Tween scoreScale = ScoreScale(Constants.scoreGameOverScale);
        Tween scoreAlpha = ScoreAlpha();
        levelTransitionOnEndSeq
        .Append(scorePosition)
        .Join(scoreScale)
        .Join(scoreAlpha)
        .SetEase(Ease.Linear)
        .Play();
    }

    private void StopLevelTransitionOnEnd(){
        levelTransitionOnEndSeq.Kill();
    }

}
