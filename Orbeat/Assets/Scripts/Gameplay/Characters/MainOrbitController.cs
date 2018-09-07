using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainOrbitController : CharacterBehaviour
{
    //[Range(0,99)]
    //public List<int> targetProbabilty;
    //public List<Transform> orbits;
    //public List<Transform> orbits;
    public List<MyScaler> orbitScalers;

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
                //SetColliders();
                //TargetProbability();
                //DoScale();
                //StartBeats();
                break;
            case GameState.End:
                StopRotation();
                //StopScale();
                break;
            case GameState.TargetHit:
                //StopRotation();
                //StopBeats();
                //StopScale();
                break;
        }
    }

    protected override void Rotate()
    {
        for (int i = 0; i < rotationComponent.Count; i++)
        {
            rotationComponent[i].StopRotate();
            AssignRandomDirection();
            if (i < orbitScalers.Count)//this will assign speeds to orbits
                AssignIndividualRotateSpeed(i);
            else//this will assign speeds to rest of the rotation components
                AssignRotateSpeed();
            rotationComponent[i].DoRotate(rotation, direction, rotateSpeed);
        }
    }

    protected override void SetPosition()
    {
        position = AssignPosition();
    }

    private void AssignIndividualRotateSpeed(int index){
        switch(index){
            case 0:
                rotateSpeed = Random.Range(Constants.minOrbitSpeed1, Constants.maxOrbitSpeed1);
                break;
            case 1:
                rotateSpeed = Random.Range(Constants.minOrbitSpeed2, Constants.maxOrbitSpeed2);
                break;
            case 2:
                rotateSpeed = Random.Range(Constants.minOrbitSpeed3, Constants.maxOrbitSpeed3);
                break;
            case 3:
                rotateSpeed = Random.Range(Constants.minOrbitSpeed4, Constants.maxOrbitSpeed4);
                break;
            case 4:
                rotateSpeed = Random.Range(Constants.minOrbitSpeed5, Constants.maxOrbitSpeed5);
                break;
            case 5:
                rotateSpeed = Random.Range(Constants.minOrbitSpeed6, Constants.maxOrbitSpeed6);
                break;
            case 6:
                rotateSpeed = Random.Range(Constants.minOrbitSpeed7, Constants.maxOrbitSpeed7);
                break;
        }
    }

    private Vector3 AssignPosition(){
        return Vector3.zero;
    }

    //private void SetColliders(){
    //    for (int i = 0; i < orbitScalers.Count;i++){
    //        orbitScalers[i].SetCollider();
    //    }
    //}

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

    public List<MyScaler> GetOrbitScalers()
    {
        return orbitScalers;
    }

    //public List<RectTransform> GetOrbits()
    //{
    //    return orbits;
    //}

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

    public Tween Scale(int orbitIndex,Vector3 outerValue){
        return orbitScalers[orbitIndex].DoScale(outerValue, Constants.transitionTime);
        //return orbits[orbitIndex].DOScale(value, Constants.transitionTime);
    }

    //public Tween ScaleDownHW(int orbitIndex,Vector2 value){
    //    return orbits[orbitIndex].DOSizeDelta(value,Constants.transitionTime);
    //}

    public Vector3 GetCurrentOuterScale(int orbitIndex){
        return orbitScalers[orbitIndex].outer.localScale;
    }

    //public Vector2 GetCurrentHW(int orbitIndex)
    //{
    //    return orbits[orbitIndex].sizeDelta;
    //}

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

    public void SetOrbits(List<MyScaler> orbitScalers){
        this.orbitScalers = orbitScalers;
    }

    //public void SetOrbits(List<RectTransform> orbits)
    //{
    //    this.orbits = orbits;
    //}


    public void SortInHierarchy(){
        int j = orbitScalers.Count - 1;
        for (int i = 0; i < orbitScalers.Count;i++){
            orbitScalers[i].transform.SetSiblingIndex(j);
            j--;
        }
        SortRotationComponents();
    }

    public Transform GetOrbit(int i){
        return orbitScalers[i].transform;
    }

    private void SortRotationComponents()
    {
        for (int i = 0; i < Constants.targetID; i++)
        {
            int j;
            Rotate key = rotationComponent[0];
            for (j = 0; j < orbitScalers.Count - 1; j++)
            {
                //shift left
                rotationComponent[j] = rotationComponent[j + 1];
            }
            rotationComponent[j] = key;
        }
    }

    //public RectTransform GetOrbitRT(int i)
    //{
    //    return orbits[i];
    //}

    //public void TargetProbability(){
    //    for (int i = 0; i < orbits.Count; i++)
    //    {
    //        int ran = Random.Range(0, 100); 
    //        orbits[i].Find("Target").gameObject.SetActive(false);
    //        if (ran < targetProbabilty[i]){ //if generated ran number is less than the orbit's target probabilty, then spawn it
    //            orbits[i].Find("Target").gameObject.SetActive(true);
    //        }
    //    }
    //}
}