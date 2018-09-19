using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameplayViewController : IController
{

    private GameplayRefs gameplayRefs;
    float cameraMovementFactor;
    Sequence punchSequence,flashSequence;

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
        gameplayRefs.cam.DOColor(colorSet.bgColor, Constants.colorTransitionTime);
        //gameplayRefs.playerObjImg.color = colorSet.playerColor;
        gameplayRefs.playerObjImg.DOColor(colorSet.playerColor,Constants.colorTransitionTime);

        //gameplayRefs.playerOrbitImg.color = colorSet.playerOrbitColor;
        gameplayRefs.playerOrbitImg.DOColor(colorSet.playerOrbitColor,Constants.colorTransitionTime);
        gameplayRefs.playerOrbitGlowImg.color = colorSet.playerOrbitGlowColor;
        //gameplayRefs.flashImg.color = colorSet.flashColor;

        //gameplayRefs.playerOrbitGlowImg.DOColor(colorSet.playerOrbitGlowColor, Constants.colorTransitionTime);

        for (int i = 0; i < gameplayRefs.hurdleOrbitsImg.Count;i++){
            //gameplayRefs.hurdleOrbitsImg[i].color = colorSet.hurdleColor;
            gameplayRefs.hurdleOrbitsImg[i].DOColor(colorSet.hurdleColor,Constants.colorTransitionTime);
        }

        //gameplayRefs.glowImg.color = colorSet.glowColor;
        //gameplayRefs.glowImg.DOColor(colorSet.glowColor,Constants.colorTransitionTime);
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

    public void OrbitPunchFade(){
        //gameplayRefs.playerOrbitImg.transform.DOScale(1.1f,0.1f).SetLoops(2, LoopType.Yoyo);
        //gameplayRefs.playerOrbitGlowImg.transform.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo);
        punchSequence.Kill();
        punchSequence = DOTween.Sequence();
        punchSequence.Append(gameplayRefs.playerOrbitImg.transform.DOPunchScale(Vector3.one * 0.35f, 0.5f, 10));
        punchSequence.Join(gameplayRefs.playerOrbitGlowImg.transform.DOPunchScale(Vector3.one * 0.35f, 0.5f, 10));
        punchSequence.Join(gameplayRefs.playerOrbitGlowImg.DOFade(0.4f, 0.25f).SetLoops(2, LoopType.Yoyo));
        punchSequence.Play();
    }

    //public void Flash(){
    //    flashSequence.Kill();
    //    flashSequence = DOTween.Sequence();
    //    flashSequence.Append(gameplayRefs.flashImg.DOFade(1f,0.2f).SetLoops(2, LoopType.Yoyo));
    //    flashSequence.Play();

    //}

}
