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

    public void ChangeColorSet(ColorSet colorSet)
    {
        for (int i = 0; i < gameplayRefs.innerOrbitsImg.Count;i++){
            //gameplayRefs.innerOrbitsImg[i].color = colorSet.bgColor; //new Color(42f/255, 47f/255, 52f/255, 255f/255); //colorSet.bgColor;
            gameplayRefs.innerOrbitsImg[i].DOColor(colorSet.bgColor,Constants.colorTransitionTime);
        }

        //gameplayRefs.playerObjImg.color = colorSet.playerColor;
        gameplayRefs.playerObjImg.DOColor(colorSet.playerColor,Constants.colorTransitionTime);


        //gameplayRefs.playerOrbitImg.color = colorSet.playerOrbitColor;
        gameplayRefs.playerOrbitImg.DOColor(colorSet.playerOrbitColor,Constants.colorTransitionTime);

        for (int i = 0; i < gameplayRefs.hurdleOrbitsImg.Count;i++){
            //gameplayRefs.hurdleOrbitsImg[i].color = colorSet.hurdleColor;
            gameplayRefs.hurdleOrbitsImg[i].DOColor(colorSet.hurdleColor,Constants.colorTransitionTime);
        }

        //gameplayRefs.glowImg.color = colorSet.glowColor;
        gameplayRefs.glowImg.DOColor(colorSet.glowColor,Constants.colorTransitionTime);
        //gameplayRefs.scoreText.color = colorSet.scoreColor;
        gameplayRefs.scoreText.DOColor(colorSet.scoreColor,Constants.colorTransitionTime);

        for (int i = 0; i < gameplayRefs.particles.Count; i++)
        {
            gameplayRefs.particles[i].startColor = colorSet.explosionColor;
        }
    }

    public void LookAtTransform(Vector3 target,float offset){
       gameplayRefs.cam.transform.position = new Vector3(target.x * (cameraMovementFactor) * offset, 0f, -10);
    }
}
