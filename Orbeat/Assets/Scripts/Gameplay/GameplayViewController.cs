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
            gameplayRefs.innerOrbitsImg[i].color = colorSet.bgColor; //new Color(42f/255, 47f/255, 52f/255, 255f/255); //colorSet.bgColor;
        }

        gameplayRefs.playerObjImg.color = colorSet.playerColor;
        gameplayRefs.playerOrbitImg.color = colorSet.playerOrbitColor;

        for (int i = 0; i < gameplayRefs.hurdleOrbitsImg.Count;i++){
            gameplayRefs.hurdleOrbitsImg[i].color = colorSet.hurdleColor;
        }

        gameplayRefs.glowImg.color = colorSet.glowColor;
        gameplayRefs.scoreText.color = colorSet.scoreColor;

        for (int i = 0; i < gameplayRefs.particles.Count; i++)
        {
            gameplayRefs.particles[i].startColor = colorSet.explosionColor;
        }
    }

    public void LookAtTransform(Vector3 target,float offset){
       gameplayRefs.cam.transform.position = new Vector3(target.x * (cameraMovementFactor) * offset, 0f, -10);
    }
}
