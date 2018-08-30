using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TargetController : CharacterBehaviour
{
    public Transform target;
    private int id;
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

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public void ChangeState(GameState state){
        
        switch(state){
            case GameState.Start:
                ResetParent();
                SetPosition();
                //StartBeat();
                Rotate();
                break;
            case GameState.TargetHit:
                transform.SetParent(target.parent);
                StopRotation();
                break;
            case GameState.End:
                StopRotation();
                break;
        }
    }

    protected override void SetPosition(){
        int pos = GameplayContoller.Instance.GetOrbitIndex();
        position = AssignPosition(pos);
    }

    private Vector3 AssignPosition(int pos){
        //int ran = Random.Range(1,Constants.orbitCount+1);
        //int ran = 3;
        switch(pos){
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

    //private void StartBeat(){
    //    targetOrbit.DoBeat(Vector3.one,Constants.beatScale,Constants.beatTime, -1);
    //}

    public int GetOrbit(){
        if (position == Constants.targetInitialPosition1)
            return 1;
        else if (position == Constants.targetInitialPosition2)
            return 2;
        else if (position == Constants.targetInitialPosition3)
            return 3;
        else
            return 0;
    }

    private void ResetParent()
    {
        transform.SetParent(target);
    }






}
