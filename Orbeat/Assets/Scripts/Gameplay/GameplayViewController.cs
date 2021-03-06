﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameplayViewController : IController
{

    private GameplayRefs gameplayRefs;
    private Sequence timerWarningSequence;
    float cameraMovementFactor;

    public GameplayViewController(GameplayRefs gpRefs)
    {
        gameplayRefs = gpRefs;
        Open();
    }

    public void Open()
    {
        SetScoreContainer(true);
        SetScore("0");
        cameraMovementFactor = GetAspectRatio();
    }

    public void SetScore(string score)
    {
        gameplayRefs.scoreText.text = score;
    }

    public void SetScoreContainer(bool value)
    {
        gameplayRefs.scoreContainer.SetActive(value);
    }

    private float GetAspectRatio()
    {
        return 1 - (float)Screen.width / Screen.height;
    }

    public void SetPerfectHit(string text)
    {
        gameplayRefs.perfectHitText.text = text;
        gameplayRefs.perfectHitText.DOFade(1f, Constants.scoreBeatTime)
                    .SetLoops(6, LoopType.Yoyo);
    }

    public void SetTargetOrbitAlpha(int pos)
    {
        ResetOrbitsAlpha();
        switch (pos)
        {
            case 1:
                gameplayRefs.orbit1Img.DOFade(1f, Constants.orbitFadeTime);
                break;
            case 2:
                gameplayRefs.orbit2Img.DOFade(1f, Constants.orbitFadeTime);
                break;
            case 3:
                gameplayRefs.orbit3Img.DOFade(1f, Constants.orbitFadeTime);
                break;
        }
    }

    private void ResetOrbitsAlpha()
    {
        gameplayRefs.orbit1Img.DOFade(Constants.orbitAlpha, 0f);
        gameplayRefs.orbit2Img.DOFade(Constants.orbitAlpha, 0f);
        gameplayRefs.orbit3Img.DOFade(Constants.orbitAlpha, 0f);

    }

    public void ChangeColorSet(ColorSet colorSet)
    {
        gameplayRefs.orbit1Img.color = colorSet.orbit1Color;
        gameplayRefs.orbit2Img.color = colorSet.orbit2Color;
        gameplayRefs.orbit3Img.color = colorSet.orbit3Color;
        //gameplayRefs.innerOrbitImg.color = colorSet.innerOrbitColor;
        gameplayRefs.cam.backgroundColor = colorSet.backgroundColor;
        gameplayRefs.playerOrbitImg.color = colorSet.playerOrbitColor;
        gameplayRefs.targetImg.color = colorSet.targetColor;
        gameplayRefs.targetOrbitImg.color = colorSet.targetOrbitColor;
        gameplayRefs.playerImg.color = colorSet.playerColor;
        gameplayRefs.scoreText.color = colorSet.scoreColor;
        //gameplayRefs.shareBtnImg.color = colorSet.shareBtnColor;
        gameplayRefs.playBtnImg.color = colorSet.playBtnColor;
        gameplayRefs.perfectHitText.color = colorSet.perfectTextColor;
        //gameplayRefs.highscoreText.color = colorSet.highscoreColor;
    }

    public void ChangeArrowColor()
    {
        gameplayRefs.playerArrowImg.color = gameplayRefs.playerImg.color;
    }

    public void Flash(Color color, float duration)
    {
        gameplayRefs.flashImg.DOColor(color, duration).SetLoops(2, LoopType.Yoyo);
    }

    public void Shake(float duration)
    {
        //gameplayRefs.transform.DOShakePosition(duration, Constants.shakeStrength, Constants.shakeRandomness);
        gameplayRefs.cam.DOShakePosition(duration, Constants.shakeStrength, Constants.shakeRandomness);
    }


    public void LookAtTarget(Vector3 target,float offset,int targetPos){
        //float angle = target.transform.rotation.eulerAngles.z;
        //if((angle > 0 && angle < 90) || (angle > 270 && angle < 360)){
        //    //Debug.Log("X is +ve");
        //    gameplayRefs.transform.DOLocalMoveX(-Constants.cameraPosOffset,Constants.cameraPosTime).SetEase(Ease.Linear);
        //}
        //else
        //{
        //    //Debug.Log("X is -ve");
        //    gameplayRefs.transform.DOLocalMoveX(Constants.cameraPosOffset, Constants.cameraPosTime).SetEase(Ease.Linear);
        //}

        gameplayRefs.cam.transform.position = new Vector3(target.x * (cameraMovementFactor) * offset * targetPos, 0, -10);
    }

    public void TimerWarningSequence(Color color,float speed){
        timerWarningSequence = DOTween.Sequence();
        timerWarningSequence.Append(gameplayRefs.timerOrbitImg.DOColor(color, speed))
        .SetLoops(-1, LoopType.Yoyo);
        timerWarningSequence.Play();
    }

    public void StopTimerWarningSequence(){
        gameplayRefs.timerOrbitImg.color = Constants.timerInitialColor;
        timerWarningSequence.Kill();
    }

    public void SetArrowAlpha(float alpha)
    {
        gameplayRefs.playerArrowImg.DOFade(alpha, 0f);
    }

    public void ScoreColor()
    {
        gameplayRefs.scoreText.DOFade(1f, Constants.scoreBeatTime).SetLoops(6, LoopType.Yoyo);
    }

    public void SetCenterOrbits(bool value){
        gameplayRefs.innerOrbitImg.gameObject.SetActive(value);
        gameplayRefs.timerOrbitImg.gameObject.SetActive(value);
    }
}
