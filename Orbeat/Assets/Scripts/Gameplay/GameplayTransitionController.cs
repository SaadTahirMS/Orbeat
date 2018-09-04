using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameplayTransitionController : MonoBehaviour {

    List<MyTargetController> targetIDs;
    PlayerController playerController;
    MainOrbitController orbitController;
    GameplayRefs gameplayRefs;

    private Sequence levelTransitionOnStartSeq; //called when game starts at the beginning or after the level transition
    private Sequence levelTransitionOnTargetHitSeq;   //called on target hit
    private Sequence timerMovement;
    private Sequence levelTransitionOnEndSeq;

    //List<Vector3> initialScales = new List<Vector3>();
    List<Vector2> initialScales = new List<Vector2>();

    //private TargetController currentTargetController;

    public void Initialize(GameplayRefs gameplayRefs,PlayerController playerController,MainOrbitController orbitController,List<MyTargetController> targetIDs){
        this.targetIDs = targetIDs;
        this.playerController = playerController;
        this.orbitController = orbitController;
        this.gameplayRefs = gameplayRefs;
    }

    public void LevelTransitionOnStart(bool isFirstTime = false){
        
        StopLevelTransitionOnStart();
        StopLevelTransitionOnEnd();
        levelTransitionOnStartSeq = DOTween.Sequence();

        CheckOrbitScale();
        SetOrbitIndividualScales();
        //SoundController.Instance.SetPitch(1);

        //Create tweens 

        //Tween targetPositionTween = TargetPosition(targetController.Position);
        //Player tweens
        playerController.gameObject.SetActive(true);
        Tween playerScaleTween;

        Tween playerPositionTween;

        if (isFirstTime)
        {
            playerController.transform.localPosition = Vector3.zero; 
            playerPositionTween = PlayerPosition(playerController.Position, Constants.playertransitionTime);
            playerScaleTween = PlayerScale();
            levelTransitionOnStartSeq.Append(playerScaleTween);
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
        //playerOrbit.localScale = Vector3.one*0.5f;
        gameplayRefs.playerOrbit.sizeDelta = Constants.playerOrbitInitialScale;
        //Score
        Tween scoreScale = InitialScoreScale(Constants.scoreInitialScale);
        ScoreScale(Constants.scoreInitialScaleWH);

        //InitialScoreScale(Constants.scoreInitialScale);
        Tween scorePosition = ScorePosition(Constants.scoreInitialPosition);
        //.Join(targetPositionTween)
        levelTransitionOnStartSeq.Join(playerPositionTween)
        //.Join(orbitsScaleTween)
        .Join(scoreScale)
        .Join(scorePosition);
        //Target tweens
        //for (int i = 0; i < targetsController.Count; i++)
        //{
        //    targetsController[i].gameObject.SetActive(true);
        //    targetsController[i].transform.localPosition = targetsController[i].Position;
        //    levelTransitionOnStartSeq.Join(TargetScale(i));
        //    levelTransitionOnStartSeq.Join(TargetFadeIn(i));
        //}
        levelTransitionOnStartSeq.SetEase(Ease.Linear);
        levelTransitionOnStartSeq.OnComplete(StartTransitionComplete)
        .Play();

    }



    private void StopLevelTransitionOnStart()
    {
        levelTransitionOnStartSeq.Kill();
    }

    //private Tween TargetScale(int index){
    //    targetsController[index].transform.localScale = Vector3.zero; 
    //    return targetsController[index].transform.DOScale(Vector3.one, Constants.transitionTime);
    //}

    //private Tween TargetPosition(Vector3 pos,int index)
    //{
    //    //target.transform.localPosition = Vector3.zero; 
    //    return  targetsController[index].transform.DOLocalMove(pos, Constants.transitionTime);
    //}

    //private Tween TargetFadeIn(int index)
    //{
    //    Image targetImg = targetsController[index].GetComponent<Image>();
    //    return targetImg.DOFade(1, Constants.transitionTime);
    //}

   

    private Tween PlayerScale()
    {
        playerController.transform.localScale = Vector3.zero; 
        return playerController.transform.DOScale(Vector3.one, Constants.playertransitionTime);
    }

    private Tween PlayerPosition(Vector3 pos,float transitionTime)
    {
        return playerController.transform.DOLocalMove(pos, transitionTime);
    }

    private Tween OrbitsScale(Vector3 scale)
    {
        return orbitController.transform.DOScale(scale, Constants.playertransitionTime);
    }

    private void ResetOrbitScale(){
        orbitController.transform.localScale = Vector3.zero;
    }

    //private void ScoreBeat(){
    //    scoreBeat.DoBeat(Constants.scoreInitialScale,Constants.scoreBeatScale,Constants.scoreBeatTime, 1);    //plays the infinite beating
    //}

    private Tween InitialScoreScale(Vector3 value){
        return gameplayRefs.scoreText.transform.DOScale(value, Constants.playertransitionTime);
    }

    private void ScoreScale(Vector2 value){
        gameplayRefs.scoreText.rectTransform.sizeDelta = value;
        //return scoreText.transform.DOScale(value, Constants.transitionTime);
    }

    private Tween ScorePosition(Vector3 value){
        return gameplayRefs.scoreText.transform.DOLocalMove(value, Constants.playertransitionTime);
    }

    private Tween ScoreAlpha()
    {
        return gameplayRefs.scoreText.DOFade(1f, Constants.transitionTime);
    }

    private void StartTransitionComplete(){
        GameplayContoller.Instance.IsAllowedToShot = true;
        GameplayContoller.Instance.playerController.SetCollisions(true);
        print("Allowed to Shot");
        //SetOrbitIndividualScales(orbitController);
        //ScoreBeat();
        TimerMovement();
    }

    private void SetOrbitIndividualScales(){
        //Orbits individual scales
        //List<Transform> orbitsTransform = orbitController.GetOrbits();
        //for (int i = 0; i < orbitsTransform.Count; i++)
        //{
        //    initialScales.Add(orbitController.GetCurrentScale(i));
        //    //print(i + " : " + initialScales[i]);
        //    //orbitsTransform[i].localScale = initialScales[i];
        //    orbitsTransform[i].DOScale(initialScales[i], .1f);

        //}

        List<RectTransform> orbitsTransform = orbitController.GetOrbitsRT();
        for (int i = 0; i < orbitsTransform.Count; i++)
        {
            initialScales.Add(orbitController.GetCurrentHW(i));
            //print(i + " : " + initialScales[i]);
            //orbitsTransform[i].localScale = initialScales[i];
            orbitsTransform[i].DOSizeDelta(initialScales[i], .1f);

        }
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
        //for (int i = 0; i < targetsController.Count; i++)
        //{
        //    timerMovement.Join(TargetMovement(i));
        //}

        //Orbits Scale
        //List<Transform> orbitScales = orbitController.GetOrbits();//list of orbits in the game
        //for (int i = 0; i < orbitScales.Count;i++){
        //    Vector3 scale = orbitController.GetCurrentScale(i);
        //    Vector3 value = scale - Vector3.one * 0.5f;
        //    timerMovement.Join(orbitScales[i].DOScale(value, Constants.orbitsScaleSpeed));
        //}
        List<RectTransform> orbitScales = orbitController.GetOrbitsRT();//list of orbits in the game
        for (int i = 0; i < orbitScales.Count; i++)
        {
            Vector2 scale = orbitController.GetCurrentHW(i);
            Vector2 value = scale - Constants.orbitReduceScale;
            timerMovement.Join(orbitScales[i].DOSizeDelta(value, Constants.orbitsScaleSpeed));
        }
        timerMovement.Play();
    }

    public void StopTimerMovement(){
        timerMovement.Kill();
    }


    private Tween PlayerOrbitScale()
    {
        //playerOrbit.localScale = Vector3.one*0.5f;
        //return playerOrbit.DOScale(Vector3.zero, Constants.playerOrbitScaleSpeed);
        //gameplayRefs.playerOrbit.sizeDelta = Constants.playerOrbitInitialScale;
        Vector2 scale = gameplayRefs.playerOrbit.sizeDelta - Constants.orbitReduceScale;//reduce player orbit by 100 units
        return gameplayRefs.playerOrbit.DOSizeDelta(scale, Constants.orbitsScaleSpeed);
    }

    private Tween PlayerMovement(){
        return playerController.transform.DOLocalMove(Vector3.zero, Constants.playerMoveSpeed);
    }

    //private Tween TargetMovement(int index)
    //{
    //    float speed = Constants.targetMoveSpeed + (Constants.targetMoveSpeed * targetsController[index].GetOrbit());
    //    return targetsController[index].transform.DOLocalMove(Vector3.zero, speed);
    //}

    //public void LevelTransitionOnTargetHit(Vector3 targetScreenPos){

    public void LevelTransitionOnTargetHit(Vector3 playerShotPos, int orbitIndex)
    {
        //int targetOrbitPos = targetsController[targetIndex].GetOrbit();
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
        //levelTransitionOnTargetHitSeq.Append(playerMoveToCenterTween);

        //.Join(playerScaleToZeroTween)
        //.Join(orbitMoveToTarget)
        //.Join(orbitScale)
        //.SetEase(Ease.Linear)
        //.OnComplete(TargetHitTransitionComplete)
        //.Play();
        Tween playerMoveToValueTween = PlayerMoveToValue(playerShotPos);

        List<RectTransform> orbitsTransform = orbitController.GetOrbitsRT();
        //Tween playerScaleToZeroTween = PlayerScaleToZero();
        //levelTransitionOnTargetHitSeq.Append(playerOrbit.DOScale(0f, Constants.transitionTime));
        levelTransitionOnTargetHitSeq.Append(gameplayRefs.playerOrbit.DOSizeDelta(Vector2.zero, Constants.transitionTime));
        levelTransitionOnTargetHitSeq.Join(playerMoveToValueTween);
        //playerController.gameObject.SetActive(false);
        //for (int i = 0; i < targetsController.Count;i++){
        //    levelTransitionOnTargetHitSeq.Join(TargetScaleToValue(0.5f,i));
        //    levelTransitionOnTargetHitSeq.Join(TargetMoveToPosition(playerShotPos,i));
        //}

        //levelTransitionOnTargetHitSeq.Join(playerScaleToZeroTween);
        //Setting pitch to zero that will result in stopping beats
        //SoundController.Instance.SetPitch(0);
        //scale down till the target orbit pos
        Vector2 scaleValue = Constants.orbitsDistance * orbitIndex; //if pos is 3 then 1.5 of scale will be reduced of each orbit
        //orbitController.StopBeats();
        for (int loopCount = 0; loopCount < orbitIndex; loopCount++)
        {
            for (int i = 0; i < orbitsTransform.Count; i++)
            {
                //Vector3 scale = orbitsTransform[i].transform.localScale - scaleValue;
                Vector3 scale = initialScales[i] - scaleValue;
                //if (scale.y <= 0f)
                    //orbitsTransform[i].GetComponent<Image>().DOFade(0f, 0f);
                Tween scaleTween = orbitController.ScaleDownHW(i, scale);
                levelTransitionOnTargetHitSeq.Join(scaleTween);
                //orbitController.ScaleDownHW(i,)
            }

            levelTransitionOnTargetHitSeq.SetEase(Ease.Linear);
            levelTransitionOnTargetHitSeq.OnComplete(() => TargetHitTransitionComplete(orbitIndex));
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

    //private Tween TargetMoveToPosition(Vector3 pos,int index)
    //{
    //    return targetsController[index].transform.DOLocalMove(pos, Constants.transitionTime);
    //}

    //private Tween TargetFadeOut(int index)
    //{
    //    Image targetImg = targetsController[index].GetComponent<Image>();
    //    return targetImg.DOFade(0, Constants.transitionTime);
    //}

    //private Tween TargetScaleToValue(float value,int index)
    //{
    //    return targetsController[index].transform.DOScale(Vector3.one*value, Constants.transitionTime);
    //}

    private Tween PlayerMoveToValue(Vector3 value)
    {
        return playerController.transform.DOLocalMove(value, Constants.transitionTime);
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

    private void TargetHitTransitionComplete(int orbitIndex){
        SortOrbits(orbitIndex);
        SortOrbitsInHierarchy();
        SortTargetIDs(orbitIndex);
        CheckOrbitScale();
        GameplayContoller.Instance.ChangeGameState(GameState.Start);
    }

    private void SortOrbits(int orbitIndex){
        List<RectTransform> orbitTransforms = orbitController.GetOrbitsRT();
        for (int i = 0; i < orbitIndex;i++){
            int j;
            RectTransform key = orbitTransforms[0];
            for (j = 0; j < orbitTransforms.Count-1; j++)
            {
                //shift left
                orbitTransforms[j] = orbitTransforms[j + 1];
            }
            orbitTransforms[j] = key;
        }
        orbitController.SetOrbits(orbitTransforms);
    }

    private void SortOrbitsInHierarchy(){
        orbitController.SortInHierarchy();//sorting orbits in heirarchy
        gameplayRefs.playerOrbit.SetAsLastSibling();//set player orbit as last in heirarchy
        gameplayRefs.timerOrbitImg.transform.SetAsLastSibling();//set timer as last in heirarchy
    }

    private void SortTargetIDs(int orbitIndex)
    {
        for (int i = 0; i < orbitIndex; i++)
        {
            int j;
            MyTargetController key = targetIDs[0];
            for (j = 0; j < targetIDs.Count - 1; j++)
            {
                //shift left
                targetIDs[j] = targetIDs[j + 1];
            }
            targetIDs[j] = key;
        }
        GameplayContoller.Instance.SetTargetIDs(targetIDs);
    }


    private void CheckOrbitScale(){

        List<RectTransform> orbitsTransform = orbitController.GetOrbitsRT();
        for (int i = 0; i < orbitsTransform.Count;i++){

            if (orbitsTransform[i].sizeDelta.x <= Constants.orbitResetScale.x)
            {
                //so this is the one to be set to initial scale
                //orbitsTransform[i].GetComponent<Image>().DOFade(0f, Constants.transitionTime);
                //orbitsTransform[i].gameObject.SetActive(false);
                orbitsTransform[i].sizeDelta = orbitController.GetCurrentHW(i) + Constants.intialOrbitScale;
                orbitsTransform[i].gameObject.SetActive(true);
                //orbitsTransform[i].GetComponent<Image>().DOFade(1f, 5f);
                //orbitsTransform[i].transform.DOScale(Constants.intialOrbitScale.x, Constants.transitionTime);

            }
        }
    }

    public void LevelTransitionOnEnd(){
        StopLevelTransitionOnEnd();
        levelTransitionOnEndSeq = DOTween.Sequence();
        playerController.gameObject.SetActive(false);
        //for (int i = 0; i < targetsController.Count;i++)
            //targetsController[i].gameObject.SetActive(false);
        //scoreBeat.StopBeat();
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
