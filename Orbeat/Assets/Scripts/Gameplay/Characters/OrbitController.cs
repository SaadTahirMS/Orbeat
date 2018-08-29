using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrbitController : CharacterBehaviour
{
    //public Beat orbit1Beat;
    //public Beat orbit2Beat;
    //public Beat orbit3Beat;

    public List<MyBeat> orbitBeat;
    public List<Transform> orbits;

    private Vector3 position;
    public Vector3 Position
    {
        get
        {
            return position;
        }
    }

    public override float MinRotateSpeed
    {
        get
        {
            return Constants.playerMinRotationSpeed;
        }
    }

    public override float MaxRotateSpeed
    {
        get
        {
            return Constants.playerMaxRotationSpeed;
        }
    }

    public override void Initialize()
    {
        gameObject.SetActive(true);
        base.Initialize();
    }

    public void ChangeState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                SetPosition();
                Rotate();
                //DoScale();
                //StartBeats();
                break;
            case GameState.End:
                StopRotation();
                //StopScale();
                break;
            case GameState.TargetHit:
                //StopBeats();
                //StopScale();
                break;
        }
    }

    protected override void SetPosition()
    {
        position = Vector3.zero;
    }


    //private void StartBeat(){
    //    //orbit1Beat.DoBeat(Vector3.one,Constants.beatScale,Constants.beatTime,1);
    //    //orbit2Beat.DoBeat(Vector3.one,Constants.beatScale,Constants.beatTime,1);
    //    //orbit3Beat.DoBeat(Vector3.one,Constants.beatScale,Constants.beatTime,1);
    //    for (int i = 0; i < orbitBeat.Count;i++){
    //        orbitBeat[i].DoBeat(Vector3.one, Constants.beatScale, Constants.beatTime, 1);
    //    }
    //}

    public List<Transform> GetOrbits(){
        return orbits;
    }

    public void StopBeats(){
        for (int i = 0; i < orbitBeat.Count; i++)
        {
            orbitBeat[i].canBeat = false;
            orbitBeat[i].StopBeat();
        }
    }

    public void StartBeats(){
        for (int i = 0; i < orbitBeat.Count; i++)
        {
            orbitBeat[i].canBeat = true;
        }
    }

    public Tween ScaleDown(int orbitIndex,Vector3 value){
        return orbits[orbitIndex].DOScale(value, Constants.transitionTime);
    }

    public Vector3 GetCurrentScale(int orbitIndex){
        return orbits[orbitIndex].localScale;
    }

    //Sequence scaleSequence;
    //public void DoScale(){
    //    scaleSequence = DOTween.Sequence();
    //    for (int i = 0; i < orbits.Count;i++){
    //        scaleSequence.Join(orbits[i].DOScale(Vector3.zero, Constants.playerOrbitScaleSpeed));
    //    }
    //    scaleSequence.Play();
    //}

    //public void StopScale(){
    //    scaleSequence.Kill();
    //}

    public void SetOrbits(List<Transform> orbits){
        this.orbits = orbits;
    }

}