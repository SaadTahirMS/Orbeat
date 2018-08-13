using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TargetController : CharacterBehaviour
{
    public Beat targetOrbit;

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
            return Constants.targetMinRotationSpeed;
        }
    }

    public override float MaxRotateSpeed
    {
        get
        {
            return Constants.targetMaxRotationSpeed;
        }
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public void ChangeState(GameState state){

        switch(state){
            case GameState.Start:
                SetPosition();
                StartBeat();
                Rotate();
                break;
            case GameState.End:
                StopRotation();
                break;
        }
    }

    protected override void SetPosition(){
        position = AssignPosition();
    }

    private Vector3 AssignPosition(){
        //int ran = Random.Range(1,Constants.orbitCount+1);
        int ran = 3;
        switch(ran){
            case 1:
                return Constants.targetInitialPosition1;
            case 2:
                return Constants.targetInitialPosition2;
            case 3:
                return Constants.targetInitialPosition3;
            default:
                return Vector3.zero;
        }
    }

    public Vector3 GetScreenPosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void StartBeat(){
        targetOrbit.DoBeat(Vector3.one,Constants.beatScale,Constants.beatTime, -1);
    }



}
