using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameplayTransitionController : MonoBehaviour
{

    private GameplayRefs gameplayRefs;
    private PlayerController playerController;
    private List<HurdleController> hurdleControllers;
    private MainOrbitController mainOrbitController;
    private List<OrbitController> orbitControllers;
    //Sequences
    private Sequence startSequence;
    private Sequence endSequence;

    public GameplayTransitionController(GameplayRefs gpRefs)
    {
        gameplayRefs = gpRefs;
        Open();
    }

    public void Open()
    {
        playerController = gameplayRefs.playerController;
        hurdleControllers = gameplayRefs.hurdleControllers;
        mainOrbitController = gameplayRefs.mainOrbitController;
    }

    public void ChangeState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                StartTransition();
                break;
            case GameState.Quit:
                EndTransition();
                break;
        }
    }

    #region StartTransition
    private void StartTransition()
    {
        Time.timeScale = 1;
        startSequence = DOTween.Sequence();
        Tween playerPosition = PlayerPosition(playerController.Position, Constants.transitionTime);
        Tween playerScale = PlayerScale(Vector3.one, Constants.transitionTime);

        startSequence.Append(playerPosition);
        startSequence.Join(playerScale);
        startSequence.Play();
    }
    #endregion

    #region EndTransition
    private void EndTransition()
    {
        //Time.timeScale = 0.1f;
        endSequence = DOTween.Sequence();
        Tween playerScale = PlayerScale(Vector3.zero, Constants.transitionTime);
        endSequence.Append(playerScale).OnComplete(()=> { Time.timeScale = 1f; });
        endSequence.OnComplete(EndTransitionComplete);
        endSequence.Play();


    }
    #endregion


    private Tween PlayerPosition(Vector3 endValue, float duration)
    {
        return playerController.transform.DOLocalMove(endValue, duration).SetEase(Ease.Linear);
    }

    //Scales parent of playerObj
    private Tween PlayerParentScale(Vector3 endValue, float duration)
    {
        return playerController.player.DOScale(endValue, duration).SetEase(Ease.Linear);
    }

    //Scale of playerObj
    private Tween PlayerScale(Vector3 endValue, float duration)
    {
        return playerController.transform.DOScale(endValue, duration).SetEase(Ease.Linear);
    }

    private void EndTransitionComplete(){
        GameplayContoller.Instance.ResetOrbitList();
        OpenMenu();
    }

    private void OpenMenu(){
		EventManager.DoFireOpenViewEvent (Views.GameOver);
    }

    //private void ResetOrbitList()
    //{
    //    orbitControllers = mainOrbitController.GetOrbits();
    //    for (int i = 0; i < orbitControllers.Count; i++)
    //    {
    //        orbitControllers[i] = gameplayRefs.intialOrbitList[i];
    //        orbitControllers[i].transform.SetAsFirstSibling();
    //    }
    //}

    //List<MyTargetController> targetIDs;
    //PlayerController playerController;
    //MainOrbitController orbitController;
    //GameplayRefs gameplayRefs;

    //MyPlayerOrbitScaler playerOrbit;

    //private Sequence levelTransitionOnStartSeq; //called when game starts at the beginning or after the level transition
    //private Sequence levelTransitionOnTargetHitSeq;   //called on target hit
    //private Sequence timerMovement;
    //private Sequence levelTransitionOnEndSeq;

    //List<Vector3> initialScales = new List<Vector3>();
    ////List<Vector2> initialScales = new List<Vector2>();

    ////private TargetController currentTargetController;

    //public void Initialize(GameplayRefs gameplayRefs,PlayerController playerController,MainOrbitController orbitController,List<MyTargetController> targetIDs){
    //    this.targetIDs = targetIDs;
    //    this.playerController = playerController;
    //    this.orbitController = orbitController;
    //    this.gameplayRefs = gameplayRefs;
    //    this.playerOrbit = this.gameplayRefs.playerOrbit;
    //}

    //public void LevelTransitionOnStart(bool isFirstTime = false){

    //    StopLevelTransitionOnStart();
    //    //StopLevelTransitionOnEnd();
    //    levelTransitionOnStartSeq = DOTween.Sequence();

    //    //CheckOrbitScale();
    //    //SetOrbitIndividualScales();
    //    //SoundController.Instance.SetPitch(1);

    //    //Create tweens 

    //    //Tween targetPositionTween = TargetPosition(targetController.Position);
    //    //Player tweens
    //    playerController.gameObject.SetActive(true);
    //    //Tween playerScaleTween;

    //    //Tween playerPositionTween;

    //    //if (isFirstTime)
    //    //{
    //    //    playerController.transform.localPosition = Vector3.zero; 
    //    //    playerPositionTween = PlayerPosition(playerController.Position, Constants.transitionTime);
    //    //    playerScaleTween = PlayerScale();
    //    //    levelTransitionOnStartSeq.Append(playerScaleTween);
    //    //}
    //    //else
    //    //{
    //    //    playerPositionTween = PlayerPosition(playerController.Position, 0f);
    //    //}
    //    //levelTransitionOnStartSeq.Join(playerPositionTween);

    //    //Orbits tweens
    //    //orbits.localPosition = Vector3.zero;
    //    //ResetOrbitScale();
    //    //Tween orbitsScaleTween = OrbitsScale(Vector3.one);
    //    //PlayerOrbit 
    //    //playerOrbit.outer.transform.localScale = Constants.playerOrbitInitialScale;
    //    playerOrbit.DoScale(Constants.playerOrbitInitialScale, 0f);
    //    //Score
    //    Tween scoreScale = InitialScoreScale(Constants.scoreInitialScale);
    //    //ScoreScale(Constants.scoreInitialScaleWH);

    //    //InitialScoreScale(Constants.scoreInitialScale);
    //    Tween scorePosition = ScorePosition(Constants.scoreInitialPosition);
    //    //.Join(targetPositionTween)
    //    //.Join(orbitsScaleTween)
    //    levelTransitionOnStartSeq.Join(scoreScale)
    //    .Join(scorePosition);
    //    //Target tweens
    //    //for (int i = 0; i < targetsController.Count; i++)
    //    //{
    //    //    targetsController[i].gameObject.SetActive(true);
    //    //    targetsController[i].transform.localPosition = targetsController[i].Position;
    //    //    levelTransitionOnStartSeq.Join(TargetScale(i));
    //    //    levelTransitionOnStartSeq.Join(TargetFadeIn(i));
    //    //}
    //    levelTransitionOnStartSeq.SetEase(Ease.Linear);
    //    levelTransitionOnStartSeq.OnComplete(StartTransitionComplete)
    //    .Play();

    //}



    //private void StopLevelTransitionOnStart()
    //{
    //    levelTransitionOnStartSeq.Kill();
    //}

    ////private Tween TargetScale(int index){
    ////    targetsController[index].transform.localScale = Vector3.zero; 
    ////    return targetsController[index].transform.DOScale(Vector3.one, Constants.transitionTime);
    ////}

    ////private Tween TargetPosition(Vector3 pos,int index)
    ////{
    ////    //target.transform.localPosition = Vector3.zero; 
    ////    return  targetsController[index].transform.DOLocalMove(pos, Constants.transitionTime);
    ////}

    ////private Tween TargetFadeIn(int index)
    ////{
    ////    Image targetImg = targetsController[index].GetComponent<Image>();
    ////    return targetImg.DOFade(1, Constants.transitionTime);
    ////}



    //private Tween PlayerScale()
    //{
    //    playerController.transform.localScale = Vector3.zero; 
    //    return playerController.transform.DOScale(Vector3.one, Constants.playertransitionTime);
    //}

    //private Tween PlayerPosition(Vector3 pos,float transitionTime)
    //{
    //    return playerController.transform.DOLocalMove(pos, transitionTime);
    //}

    //private Tween OrbitsScale(Vector3 scale)
    //{
    //    return orbitController.transform.DOScale(scale, Constants.playertransitionTime);
    //}

    //private void ResetOrbitScale(){
    //    orbitController.transform.localScale = Vector3.zero;
    //}

    ////private void ScoreBeat(){
    ////    scoreBeat.DoBeat(Constants.scoreInitialScale,Constants.scoreBeatScale,Constants.scoreBeatTime, 1);    //plays the infinite beating
    ////}

    //private Tween InitialScoreScale(Vector3 value){
    //    return gameplayRefs.scoreText.transform.DOScale(value, Constants.playertransitionTime);
    //}

    //private void ScoreScale(Vector2 value){
    //    gameplayRefs.scoreText.rectTransform.sizeDelta = value;
    //    //return scoreText.transform.DOScale(value, Constants.transitionTime);
    //}

    //private Tween ScorePosition(Vector3 value){
    //    return gameplayRefs.scoreText.transform.DOLocalMove(value, Constants.playertransitionTime);
    //}

    //private Tween ScoreAlpha()
    //{
    //    return gameplayRefs.scoreText.DOFade(1f, Constants.transitionTime);
    //}

    //private void StartTransitionComplete(){
    //    GameplayContoller.Instance.IsAllowedToShot = true;
    //    GameplayContoller.Instance.playerController.SetCollisions(true);
    //    print("Allowed to Shot");
    //    //SetOrbitIndividualScales(orbitController);
    //    //ScoreBeat();
    //    TimerMovement();
    //}

    //private void SetOrbitIndividualScales(){
    //    //Orbits individual scales
    //    List<MyScaler> orbitScalers = orbitController.GetOrbitScalers();
    //    for (int i = 0; i < orbitScalers.Count; i++)
    //    {
    //        initialScales.Add(orbitController.GetCurrentOuterScale(i));
    //        //print(i + " : " + initialScales[i]);
    //        //orbitsTransform[i].localScale = initialScales[i];
    //        //orbitController.Scale(i, initialScales[i]);
    //        orbitScalers[i].DoScale(initialScales[i], 0.1f);
    //    }


    //    //List<RectTransform> orbitsTransform = orbitController.GetOrbits();
    //    //for (int i = 0; i < orbitsTransform.Count; i++)
    //    //{
    //    //    //targetIDs[i].gameObject.SetActive(true);
    //    //    initialScales.Add(orbitController.GetCurrentHW(i));
    //    //    //print(i + " : " + initialScales[i]);
    //    //    //orbitsTransform[i].localScale = initialScales[i];
    //    //    orbitsTransform[i].DOSizeDelta(initialScales[i], .1f);

    //    //}
    //}

    //private void TimerMovement(){
    //    StopTimerMovement();
    //    timerMovement = DOTween.Sequence();
    //    //Player Orbit tweens
    //    //Tween playerOrbitScale = PlayerOrbitScale();
    //    //Player Movement tweens
    //    //Tween playerMovement = PlayerMovement();
    //    //timerMovement.Append(playerOrbitScale);
    //    //timerMovement.Join(playerMovement);
    //    //for (int i = 0; i < targetsController.Count; i++)
    //    //{
    //    //    timerMovement.Join(TargetMovement(i));
    //    //}



    //    //Orbits Scale
    //    //List<MyScaler> orbitScales = orbitController.GetOrbitScalers();//list of outer orbits in the game
    //    //for (int i = 0; i < orbitScales.Count;i++){
    //    //Vector3 scale = orbitController.GetCurrentOuterScale(i);
    //    //Vector3 value = scale - Constants.orbitReduceScale;
    //    //timerMovement.Join(orbitScales[i].DOScale(value, Constants.orbitsScaleSpeed));

    //    //timerMovement.Join(orbitScales[i].DoScale(Vector3.zero, Constants.orbitsScaleSpeed));

    //    //Vector3 currentPosition = targetIDs[i].edgeObj1.localPosition;
    //    //value = currentPosition - Constants.edgeMovementReduceValue; //50f reduction of edges because player dies after that so no need for more

    //    //targetIDs[i].SetEdgeState(true);
    //    //timerMovement.Join(targetIDs[i].EdgeMovement(value,Constants.orbitsScaleSpeed));
    //    //}
    //    //List<RectTransform> orbitScales = orbitController.GetOrbits();//list of orbits in the game
    //    //for (int i = 0; i < orbitScales.Count; i++)
    //    //{
    //    //    Vector2 scale = orbitController.GetCurrentHW(i);
    //    //    Vector2 value = scale - Constants.orbitReduceScale;
    //    //    timerMovement.Join(orbitScales[i].DOSizeDelta(value, Constants.orbitsScaleSpeed));
    //    //}

    //    List<MyScaler> orbitScales = orbitController.GetOrbitScalers();//list of outer orbits in the game
    //    for (int i = 0; i < orbitScales.Count; i++) {
    //        timerMovement.Join(orbitScales[i].DoScale(Vector3.zero,  Constants.orbitsScaleSpeed+i*2f+1));
    //    }
    //    timerMovement.Play();
    //}

    //public void StopTimerMovement(){
    //    timerMovement.Kill();
    //}


    //private Tween PlayerOrbitScale()
    //{
    //    //playerOrbit.localScale = Vector3.one*0.5f;
    //    return playerOrbit.DoScale(Vector3.zero, Constants.playerOrbitScaleSpeed);
    //    //gameplayRefs.playerOrbit.sizeDelta = Constants.playerOrbitInitialScale;
    //    //Vector2 scale = gameplayRefs.playerOrbit.sizeDelta - Constants.orbitReduceScale;//reduce player orbit by 100 units
    //    //return gameplayRefs.playerOrbit.DOSizeDelta(scale, Constants.orbitsScaleSpeed);
    //}

    //private Tween PlayerMovement(){
    //    return playerController.transform.DOLocalMove(Vector3.zero, Constants.playerMoveSpeed);
    //}

    ////private Tween TargetMovement(int index)
    ////{
    ////    float speed = Constants.targetMoveSpeed + (Constants.targetMoveSpeed * targetsController[index].GetOrbit());
    ////    return targetsController[index].transform.DOLocalMove(Vector3.zero, speed);
    ////}

    ////public void LevelTransitionOnTargetHit(Vector3 targetScreenPos){

    //public void LevelTransitionOnTargetHit(Vector3 playerShotPos, int targetIndex)
    //{
    //    //int targetOrbitPos = targetsController[targetIndex].GetOrbit();
    //    StopLevelTransitionOnTargetHit();

    //    levelTransitionOnTargetHitSeq = DOTween.Sequence();

    //    ////Create tweens
    //    ////Target tweens
    //    //Tween targetMoveToCenterTween = TargetMoveToCenter();
    //    ////Tween targetFadeOutTween = TargetFadeOut();
    //    ////Player tweens
    //    //Tween playerMoveToCenterTween = PlayerMoveToCenter();
    //    //Tween playerScaleToZeroTween = PlayerScaleToZero();
    //    ////Orbit tweens
    //    //Vector3 direction = GetDirection(targetScreenPos);
    //    //Tween orbitMoveToTarget = OrbitMoveToTarget(direction.x,direction.y);
    //    //Tween orbitScale = OrbitsScale(Vector3.one * 2);
    //    ////Add tweens
    //    //levelTransitionOnTargetHitSeq.Append(targetMoveToCenterTween)
    //    ////.Join(targetFadeOutTween)
    //    //levelTransitionOnTargetHitSeq.Append(playerMoveToCenterTween);

    //    //.Join(playerScaleToZeroTween)
    //    //.Join(orbitMoveToTarget)
    //    //.Join(orbitScale)
    //    //.SetEase(Ease.Linear)
    //    //.OnComplete(TargetHitTransitionComplete)
    //    //.Play();

    //    Tween playerMoveToValueTween = PlayerMoveToValue(playerShotPos);

    //    List<MyScaler> orbitScalers = orbitController.GetOrbitScalers();
    //    //Tween playerScaleToZeroTween = PlayerScaleToZero();
    //    levelTransitionOnTargetHitSeq.Append(playerOrbit.DoScale(Vector3.zero, Constants.transitionTime));
    //    //levelTransitionOnTargetHitSeq.Append(gameplayRefs.playerOrbit.DOSizeDelta(Constants.orbitReduceScale, Constants.transitionTime));
    //    //levelTransitionOnTargetHitSeq.Append(gameplayRefs.playerOrbit.DOSizeDelta(Vector2.zero, Constants.transitionTime));
    //    levelTransitionOnTargetHitSeq.Join(playerMoveToValueTween);
    //    //playerController.gameObject.SetActive(false);
    //    //for (int i = 0; i < targetsController.Count;i++){
    //    //    levelTransitionOnTargetHitSeq.Join(TargetScaleToValue(0.5f,i));
    //    //    levelTransitionOnTargetHitSeq.Join(TargetMoveToPosition(playerShotPos,i));
    //    //}

    //    //levelTransitionOnTargetHitSeq.Join(playerScaleToZeroTween);
    //    //Setting pitch to zero that will result in stopping beats
    //    //SoundController.Instance.SetPitch(0);
    //    //scale down till the target orbit pos
    //    Vector3 scaleValue = Constants.orbitsDistance * targetIndex; 
    //    for (int loopCount = 0; loopCount < targetIndex; loopCount++)
    //    {
    //        for (int i = 0; i < orbitScalers.Count; i++)
    //        {
    //            targetIDs[i].SetEdgeState(false);

    //            Vector3 scale = orbitScalers[i].outer.transform.localScale - scaleValue;
    //            //Vector3 scale = initialScales[i] - scaleValue;
    //            //if (scale.y <= 0f)
    //            //orbitsTransform[i].GetComponent<Image>().DOFade(0f, 0f);
    //            //Tween scaleTween = orbitController.ScaleDownHW(i, scale);
    //            Tween scaleTween = orbitScalers[i].DoScale(scale, Constants.transitionTime);
    //            levelTransitionOnTargetHitSeq.Join(scaleTween);
    //            //orbitController.ScaleDownHW(i,)
    //        }


    //        levelTransitionOnTargetHitSeq.SetEase(Ease.Linear);
    //        levelTransitionOnTargetHitSeq.OnComplete(() => TargetHitTransitionComplete(targetIndex));
    //        levelTransitionOnTargetHitSeq.Play();
    //    }
    //    //RecursiveTransition(targetOrbitPos, orbitController);
    //}

    ////private void RecursiveTransition(int targetOrbitPos, OrbitController orbitController)
    ////{
    ////    if (targetOrbitPos <= 0)
    ////    {
    ////        GameplayContoller.Instance.ChangeGameState(GameState.Start);
    ////        return;
    ////    }
    ////    List<Transform> orbitsTransform = orbitController.GetOrbits();

    ////    for (int i = 0; i < orbitsTransform.Count; i++)
    ////    {
    ////        Vector3 scale = orbitsTransform[i].transform.localScale - Constants.orbitsDistance;
    ////        Tween scaleTween = orbitController.ScaleDown(i, scale);
    ////        levelTransitionOnTargetHitSeq.Join(scaleTween);
    ////    }

    ////    levelTransitionOnTargetHitSeq.SetEase(Ease.Linear);
    ////    levelTransitionOnTargetHitSeq.OnComplete(delegate {

    ////        TargetHitTransitionComplete(orbitController, orbitsTransform);
    ////        Dummy(targetOrbitPos, orbitController);
    ////    });
    ////    levelTransitionOnTargetHitSeq.Play();

    ////}

    ////private void Dummy(int targetOrbitPos, OrbitController orbitController)
    ////{
    ////    levelTransitionOnTargetHitSeq.Kill();
    ////    RecursiveTransition(--targetOrbitPos, orbitController);
    ////}

    //private void StopLevelTransitionOnTargetHit(){
    //    levelTransitionOnTargetHitSeq.Kill();
    //}

    ////private Tween TargetMoveToPosition(Vector3 pos,int index)
    ////{
    ////    return targetsController[index].transform.DOLocalMove(pos, Constants.transitionTime);
    ////}

    ////private Tween TargetFadeOut(int index)
    ////{
    ////    Image targetImg = targetsController[index].GetComponent<Image>();
    ////    return targetImg.DOFade(0, Constants.transitionTime);
    ////}

    ////private Tween TargetScaleToValue(float value,int index)
    ////{
    ////    return targetsController[index].transform.DOScale(Vector3.one*value, Constants.transitionTime);
    ////}

    //private Tween PlayerMoveToValue(Vector3 value)
    //{
    //    return playerController.transform.DOLocalMove(value, Constants.transitionTime);
    //}

    //private Tween PlayerMoveToCenter()
    //{
    //    return playerController.transform.DOLocalMove(Vector3.zero, Constants.transitionTime);
    //}

    //private Tween PlayerScaleToZero()
    //{
    //    return playerController.transform.DOScale(Vector3.zero, Constants.transitionTime);
    //}

    //private Vector3 GetDirection(Vector3 pos)
    //{
    //    return new Vector3(Screen.width / 2 - pos.x, Screen.height / 2 - pos.y, 0f);
    //}

    //private Tween OrbitMoveToTarget(float x, float y)
    //{
    //    return orbitController.transform.DOLocalMove(new Vector3(x * 10, y * 10, 0f), Constants.transitionTime);
    //}

    //private void TargetHitTransitionComplete(int orbitIndex){
    //    SortOrbits(orbitIndex);
    //    SortOrbitsInHierarchy();
    //    SortTargetIDs(orbitIndex);
    //    //CheckOrbitScale();
    //    GameplayContoller.Instance.ChangeGameState(GameState.Start);
    //}

    //private void SortOrbits(int orbitIndex){
    //    List<MyScaler> orbitTransforms = orbitController.GetOrbitScalers();
    //    for (int i = 0; i < orbitIndex;i++){
    //        int j;
    //        MyScaler key = orbitTransforms[0];
    //        for (j = 0; j < orbitTransforms.Count-1; j++)
    //        {
    //            //shift left
    //            orbitTransforms[j] = orbitTransforms[j + 1];
    //        }
    //        orbitTransforms[j] = key;
    //    }
    //    orbitController.SetOrbits(orbitTransforms);
    //}

    //private void SortOrbitsInHierarchy(){
    //    orbitController.SortInHierarchy();//sorting orbits in heirarchy
    //    playerOrbit.transform.SetAsLastSibling();//set player orbit as last in heirarchy
    //    gameplayRefs.timerOrbitImg.transform.SetAsLastSibling();//set timer as last in heirarchy
    //}

    //private void SortTargetIDs(int orbitIndex)
    //{
    //    for (int i = 0; i < orbitIndex; i++)
    //    {
    //        int j;
    //        MyTargetController key = targetIDs[0];
    //        for (j = 0; j < targetIDs.Count - 1; j++)
    //        {
    //            //shift left
    //            targetIDs[j] = targetIDs[j + 1];
    //        }
    //        targetIDs[j] = key;
    //    }
    //    GameplayContoller.Instance.SetTargetIDs(targetIDs);
    //}


    ////private void CheckOrbitScale(){

    ////    List<MyScaler> orbitScalers = orbitController.GetOrbitScalers();
    ////    for (int i = 0; i < orbitScalers.Count;i++){

    ////        if (orbitScalers[i].outer.transform.localScale.x <= Constants.orbitResetScale.x)
    ////        {
    ////            //so this is the one to be set to initial scale
    ////            //orbitScalers[i].transform.localScale = orbitController.GetCurrentOuterScale(i) + Constants.intialOrbitScale;
    ////            orbitScalers[i].DoScale(orbitController.GetCurrentOuterScale(i) + Constants.intialOrbitScale, 0f);
    ////            //orbitScalers[i].gameObject.SetActive(true);
    ////            //orbitsTransform[i].GetComponent<Image>().DOFade(1f, 5f);
    ////            //orbitsTransform[i].transform.DOScale(Constants.intialOrbitScale.x, Constants.transitionTime);
    ////        }
    ////    }
    ////}


    //public void LevelTransitionOnEnd(){
    //    StopLevelTransitionOnEnd();
    //    levelTransitionOnEndSeq = DOTween.Sequence();
    //    playerController.gameObject.SetActive(false);
    //    //for (int i = 0; i < targetsController.Count;i++)
    //        //targetsController[i].gameObject.SetActive(false);
    //    //scoreBeat.StopBeat();
    //    Tween scorePosition = ScorePosition(Constants.scoreGameOverPos);
    //    //Tween scoreScale = ScoreScale(Constants.scoreGameOverScale);
    //    ScoreScale(Constants.scoreGameOverScale);
    //    Tween scoreAlpha = ScoreAlpha();
    //    levelTransitionOnEndSeq
    //    .Append(scorePosition)
    //    //.Join(scoreScale)
    //    .Join(scoreAlpha)
    //    .SetEase(Ease.Linear)
    //    .Play();
    //}

    //private void StopLevelTransitionOnEnd(){
    //    levelTransitionOnEndSeq.Kill();
    //}

}
