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
        gameplayRefs.playerObjImg.DOColor(colorSet.playerColor,Constants.colorTransitionTime);

        gameplayRefs.playerOrbitImg.DOColor(colorSet.playerOrbitColor,Constants.colorTransitionTime);
        gameplayRefs.playerOrbitGlowImg.color = colorSet.playerOrbitGlowColor;

        for (int i = 0; i < gameplayRefs.hurdleOrbitsImg.Count;i++){
            gameplayRefs.hurdleOrbitsImg[i].DOColor(colorSet.hurdleColor,Constants.colorTransitionTime);
        }

        gameplayRefs.scoreText.DOColor(colorSet.scoreColor,Constants.colorTransitionTime);

        for (int i = 0; i < gameplayRefs.particles.Count; i++)
        {
            gameplayRefs.particles[i].startColor = colorSet.explosionColor;
        }

		EventManager.DoFireUpdateFillBarColorEvent (colorSet.barColor,colorSet.barfillColor);
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
        //punchSequence.Join(gameplayRefs.playerOrbitGlowImg.transform.DOPunchScale(Vector3.one * 0.35f, 0.5f, 10));
        //punchSequence.Join(gameplayRefs.playerOrbitGlowImg.DOFade(0.4f, 0.25f).SetLoops(2, LoopType.Yoyo));
        punchSequence.Play();
    }

    //public void Flash(){
    //    flashSequence.Kill();
    //    flashSequence = DOTween.Sequence();
    //    flashSequence.Append(gameplayRefs.flashImg.DOFade(1f,0.2f).SetLoops(2, LoopType.Yoyo));
    //    flashSequence.Play();

    //}

}
