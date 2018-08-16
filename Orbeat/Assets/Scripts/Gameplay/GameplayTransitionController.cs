using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameplayTransitionController : MonoBehaviour {
    
    public Beat scoreBeat;
    public Text scoreText;

    public Transform playerOrbit;

    private Sequence levelTransitionOnStartSeq; //called when game starts at the beginning or after the level transition
    private Sequence levelTransitionOnTargetHitSeq;   //called on target hit
    private Sequence timerMovement;
    private Sequence levelTransitionOnEndSeq;

    public void LevelTransitionOnStart(TargetController target,PlayerController player,OrbitController orbit,List<HurdleController> hurdles,int hurdleCount){
        StopLevelTransitionOnStart();
        StopLevelTransitionOnEnd();
        levelTransitionOnStartSeq = DOTween.Sequence();
        
        //Create tweens 
        //Target tweens
        target.gameObject.SetActive(true);
        Tween targetScaleTween = TargetScale(target.transform);
        Tween targetPositionTween = TargetPosition(target.transform,target.Position);
        Tween targetFadeInTween = TargetFadeIn(target.GetComponent<Image>());
        //Player tweens
        player.gameObject.SetActive(true);
        Tween playerScaleTween = PlayerScale(player.transform);
        Tween playerPositionTween = PlayerPosition(player.transform, player.Position);
        //Orbits tweens
        orbit.transform.localPosition = Vector3.zero;
        ResetOrbitScale(orbit.transform);
        Tween orbitsScaleTween = OrbitsScale(orbit.transform,Vector3.one);
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
        .Join(scorePosition);

        for (int i = 0; i < hurdleCount;i++){
            hurdles[i].gameObject.SetActive(true);
            levelTransitionOnStartSeq.Join(HurdleScale(hurdles[i].transform))
                                     .Join(HurdlePosition(hurdles[i].transform,hurdles[i].Position))
                                     .Join(HurdleFadeIn(hurdles[i].GetComponent<Image>()));
        }

        levelTransitionOnStartSeq.SetEase(Ease.Linear)
        .OnComplete(()=>StartTransitionComplete(player.transform))
        .Play();

    }

    private void StopLevelTransitionOnStart()
    {
        levelTransitionOnStartSeq.Kill();
    }

    private Tween TargetScale(Transform target){
        target.localScale = Vector3.zero; 
        return target.DOScale(Vector3.one, Constants.transitionTime);
    }

    private Tween TargetPosition(Transform target,Vector3 pos)
    {
        target.localPosition = Vector3.zero; 
        return  target.DOLocalMove(pos, Constants.transitionTime);
    }

    private Tween TargetFadeIn(Image targetImg)
    {
        return targetImg.DOFade(1, Constants.transitionTime);
    }

    private Tween HurdleScale(Transform hurdle)
    {
        hurdle.localScale = Vector3.zero;
        return hurdle.DOScale(Vector3.one, Constants.transitionTime);
    }

    private Tween HurdlePosition(Transform hurdle, Vector3 pos)
    {
        hurdle.localPosition = Vector3.zero;
        return hurdle.DOLocalMove(pos, Constants.transitionTime);
    }

    private Tween HurdleFadeIn(Image hurdleImg)
    {
        return hurdleImg.DOFade(1, Constants.transitionTime);
    }

   

    private Tween PlayerScale(Transform player)
    {
        player.localScale = Vector3.zero; 
        return player.DOScale(Vector3.one, Constants.transitionTime);
    }

    private Tween PlayerPosition(Transform player,Vector3 pos)
    {
        player.localPosition = Vector3.zero; 
        return player.DOLocalMove(pos, Constants.transitionTime);
    }

    private Tween OrbitsScale(Transform orbits,Vector3 scale)
    {
        return orbits.DOScale(scale, Constants.transitionTime);
    }

    private void ResetOrbitScale(Transform orbits){
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

    private void StartTransitionComplete(Transform player){
        GameplayContoller.Instance.IsAllowedToShot = true;
        GameplayContoller.Instance.playerController.SetCollisions(true);
        ScoreBeat();
        TimerMovement(player);
    }

    private void TimerMovement(Transform player){
        StopTimerMovement();
        timerMovement = DOTween.Sequence();
        //Player Orbit tweens
        Tween playerOrbitScale = PlayerOrbitScale();
        //Player Movement tweens
        Tween playerMovement = PlayerMovement(player);

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

    private Tween PlayerMovement(Transform player){
        return player.DOLocalMove(Vector3.zero, Constants.playerMoveSpeed);
    }

    public void LevelTransitionOnTargetHit(TargetController target, PlayerController player, OrbitController orbit,List<HurdleController> hurdles,int hurdleCount, Vector3 targetScreenPos){
        StopLevelTransitionOnTargetHit();

        levelTransitionOnTargetHitSeq = DOTween.Sequence();

        //Create tweens
        //Target tweens
        Tween targetMoveToCenterTween = TargetMoveToCenter(target.transform);
        //Tween targetFadeOutTween = TargetFadeOut();
        //Player tweens
        Tween playerMoveToCenterTween = PlayerMoveToCenter(player.transform);
        Tween playerScaleToZeroTween = PlayerScaleToZero(player.transform);
        //Orbit tweens
        Vector3 direction = GetDirection(targetScreenPos);
        Tween orbitMoveToTarget = OrbitMoveToTarget(orbit.transform,direction.x,direction.y);
        Tween orbitScale = OrbitsScale(orbit.transform, Vector3.one * 2);
        //Add tweens
        levelTransitionOnTargetHitSeq.Append(targetMoveToCenterTween)
        //.Join(targetFadeOutTween)
        //.Join(playerMoveToCenterTween)
        .Join(playerScaleToZeroTween)
        .Join(orbitMoveToTarget)
                                     .Join(orbitScale);

        for (int i = 0; i < hurdleCount;i++){
            levelTransitionOnTargetHitSeq.Join(HurdleMoveToCenter(hurdles[i].transform));
                                         //.Join(HurdleFadeOut(hurdles[i].GetComponent<Image>()));
                                    

        }

        levelTransitionOnTargetHitSeq.SetEase(Ease.Linear)
        .OnComplete(TargetHitTransitionComplete)
        .Play();


    }

    private void StopLevelTransitionOnTargetHit(){
        levelTransitionOnTargetHitSeq.Kill();
    }

    private Tween TargetMoveToCenter(Transform target)
    {
        return target.DOLocalMove(Vector3.zero, Constants.transitionTime);
    }

    private Tween TargetFadeOut(Image targetImg)
    {
        return targetImg.DOFade(0, Constants.transitionTime);
    }

    private Tween HurdleMoveToCenter(Transform hurdle)
    {
        return hurdle.DOLocalMove(Vector3.zero, Constants.transitionTime);
    }

    private Tween HurdleFadeOut(Image hurdleImg)
    {
        return hurdleImg.DOFade(0, Constants.transitionTime);
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

    private Tween OrbitMoveToTarget(Transform orbits,float x, float y)
    {
        return orbits.DOLocalMove(new Vector3(x * 10, y * 10, 0f), Constants.transitionTime);
    }

    private void TargetHitTransitionComplete(){
        GameplayContoller.Instance.ChangeGameState(GameState.Start);
    }

    public void LevelTransitionOnEnd(PlayerController player, TargetController target,List<HurdleController> hurdles,int hurdleCount){
        StopLevelTransitionOnEnd();
        levelTransitionOnEndSeq = DOTween.Sequence();
        player.gameObject.SetActive(false);
        target.gameObject.SetActive(false);
        for (int i = 0; i < hurdleCount;i++){
            hurdles[i].gameObject.SetActive(false);
        }
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
