using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainOrbitController : CharacterBehaviour
{
    [Range(0,99)]
    public List<int> targetProbabilty;
    //public List<Transform> orbits;
    public List<RectTransform> orbits;

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
                //TargetProbability();
                //DoScale();
                //StartBeats();
                break;
            case GameState.End:
                StopRotation();
                //StopScale();
                break;
            case GameState.TargetHit:
                StopRotation();
                //StopBeats();
                //StopScale();
                break;
        }
    }

    protected override void SetPosition()
    {
        position = AssignPosition();
    }

    private Vector3 AssignPosition(){
        return Vector3.zero;
    }

    //private void StartBeat(){
    //    //orbit1Beat.DoBeat(Vector3.one,Constants.beatScale,Constants.beatTime,1);
    //    //orbit2Beat.DoBeat(Vector3.one,Constants.beatScale,Constants.beatTime,1);
    //    //orbit3Beat.DoBeat(Vector3.one,Constants.beatScale,Constants.beatTime,1);
    //    for (int i = 0; i < orbitBeat.Count;i++){
    //        orbitBeat[i].DoBeat(Vector3.one, Constants.beatScale, Constants.beatTime, 1);
    //    }
    //}

    //public List<Transform> GetOrbits(){
    //    return orbits;
    //}

    public List<RectTransform> GetOrbitsRT()
    {
        return orbits;
    }

    //public void StopBeats(){
    //    for (int i = 0; i < orbitBeat.Count; i++)
    //    {
    //        orbitBeat[i].canBeat = false;
    //        orbitBeat[i].StopBeat();
    //    }
    //}

    //public void StartBeats(){
    //    for (int i = 0; i < orbitBeat.Count; i++)
    //    {
    //        orbitBeat[i].canBeat = true;
    //    }
    //}

    //public Tween ScaleDown(int orbitIndex,Vector3 value){
    //    return orbits[orbitIndex].GetComponent<Scaler>().DoScale(value);
    //    //orbits[orbitIndex].GetComponent<Scaler>().DoHeightWidth
    //    //return orbits[orbitIndex].DOScale(value, Constants.transitionTime);
    //}

    public Tween ScaleDownHW(int orbitIndex,Vector2 value){
        return orbits[orbitIndex].DOSizeDelta(value,Constants.transitionTime);
    }

    public Vector3 GetCurrentScale(int orbitIndex){
        return orbits[orbitIndex].localScale;
    }

    public Vector2 GetCurrentHW(int orbitIndex)
    {
        return orbits[orbitIndex].sizeDelta;
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

    //public void SetOrbits(List<Transform> orbits){
    //    this.orbits = orbits;
    //}

    public void SetOrbits(List<RectTransform> orbits)
    {
        this.orbits = orbits;
    }

    public void SortInHierarchy(){
        int j = orbits.Count - 1;
        for (int i = 0; i < orbits.Count;i++){
            orbits[i].SetSiblingIndex(j);
            j--;
        }
    }

    public Transform GetOrbit(int i){
        return orbits[i];
    }

    public RectTransform GetOrbitRT(int i)
    {
        return orbits[i];
    }

    public void TargetProbability(){
        for (int i = 0; i < orbits.Count; i++)
        {
            int ran = Random.Range(0, 100); 
            orbits[i].Find("Target").gameObject.SetActive(false);
            if (ran < targetProbabilty[i]){ //if generated ran number is less than the orbit's target probabilty, then spawn it
                orbits[i].Find("Target").gameObject.SetActive(true);
            }
        }
    }
}