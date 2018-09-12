using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameplayViewController : IController
{

    private GameplayRefs gameplayRefs;
    float cameraMovementFactor;

    public GameplayViewController(GameplayRefs gpRefs)
    {
        gameplayRefs = gpRefs;
        Open();
    }

    public void Open()
    {
        cameraMovementFactor = GetAspectRatio();
    }

    public void SetScore(int score)
    {
        gameplayRefs.scoreText.text = score.ToString();
    }

    private float GetAspectRatio()
    {
        return 1 - (float)Screen.width / Screen.height;
    }

    //public void ChangeColorSet(ColorSet colorSet)
    //{
    //    //gameplayRefs.orbit1Img.color = colorSet.orbit1Color;
    //    //gameplayRefs.orbit2Img.color = colorSet.orbit2Color;
    //    //gameplayRefs.orbit3Img.color = colorSet.orbit3Color;
    //    for (int i = 0; i < gameplayRefs.orbitImg.Count; i++)
    //    {
    //        gameplayRefs.orbitImg[i].color = colorSet.orbitColor;
    //    }
    //    //gameplayRefs.innerOrbitImg.color = colorSet.innerOrbitColor;
    //    gameplayRefs.cam.backgroundColor = colorSet.backgroundColor;
    //    gameplayRefs.playerOrbitImg.color = colorSet.playerOrbitColor;
    //    gameplayRefs.playerImg.color = colorSet.playerColor;
    //    gameplayRefs.scoreText.color = colorSet.scoreColor;
    //    //gameplayRefs.shareBtnImg.color = colorSet.shareBtnColor;
    //    gameplayRefs.playBtnImg.color = colorSet.playBtnColor;
    //    gameplayRefs.perfectHitText.color = colorSet.perfectTextColor;
    //    //gameplayRefs.highscoreText.color = colorSet.highscoreColor;
    //    //for (int i = 0; i < gameplayRefs.targetImg.Count;i++){
    //    //    gameplayRefs.targetImg[i].color = colorSet.targetColor;
    //    //    gameplayRefs.targetOrbitImg[i].color = colorSet.targetOrbitColor;
    //    //}
    //}

    public void LookAtTransform(Vector3 target,float offset){
       gameplayRefs.cam.transform.position = new Vector3(target.x * (cameraMovementFactor) * offset, 0f, -10);
    }
}
