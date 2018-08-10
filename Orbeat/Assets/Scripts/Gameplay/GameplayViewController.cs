using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameplayViewController : IController{
    
    private GameplayRefs gameplayRefs;

    public GameplayViewController(GameplayRefs gpRefs)
    {
        gameplayRefs = gpRefs;
        Open();   
    }

    public void Open()
    {
        SetScoreContainer(true);
        SetScore("0");
    }

    public void SetScore(string score){
        gameplayRefs.scoreText.text = score;
    }

    public void SetScoreContainer(bool value){
        gameplayRefs.scoreContainer.SetActive(value);
    }

    public void ChangeColorSet(ColorSet colorSet){
        gameplayRefs.orbit1Img.color = colorSet.orbit1Color;
        gameplayRefs.orbit2Img.color = colorSet.orbit2Color;
        gameplayRefs.orbit3Img.color = colorSet.orbit3Color;
        gameplayRefs.innerOrbitImg.color = colorSet.innerOrbitColor;
        gameplayRefs.cam.backgroundColor = colorSet.backgroundColor;
        gameplayRefs.playerOrbitImg.color = colorSet.playerOrbitColor;
        gameplayRefs.targetImg.color = colorSet.targetColor;
        gameplayRefs.targetOrbitImg.color = colorSet.targetOrbitColor;
        gameplayRefs.playerImg.color = colorSet.playerColor;
        gameplayRefs.scoreText.color = colorSet.scoreColor;
        //gameplayRefs.playerArrowImg.color = colorSet.playerArrowColor;
        //gameplayRefs.shareBtnImg.color = colorSet.shareBtnColor;
        gameplayRefs.playBtnImg.color = colorSet.playBtnColor;
        //gameplayRefs.highscoreText.color = colorSet.highscoreColor;
    }

    public void CameraFlash(Color color,float duration){
        gameplayRefs.flashImg.DOColor(color, duration).SetLoops(2, LoopType.Yoyo);
    }

    public void CameraShake(float duration){
        //gameplayRefs.cam.DOShakePosition(duration);
        //gameplayRefs.cam.DOShakeRotation(duration);
    }
}
