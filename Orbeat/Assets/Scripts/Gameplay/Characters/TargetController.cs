using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TargetController : CharacterBehaviour
{
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
        //int ran = Random.Range(1,4);
        int ran = 1;
        switch(ran){
            case 1:
                //transform.localPosition = Constants.targetInitialPosition1;
                //transform.DOLocalMove(Constants.targetInitialPosition1, 1f);
                return Constants.targetInitialPosition1;
            case 2:
                //transform.localPosition = Constants.targetInitialPosition2;
                //transform.DOLocalMove(Constants.targetInitialPosition2, 1f);
                return Constants.targetInitialPosition2;
            case 3:
                //transform.localPosition = Constants.targetInitialPosition3;
                //transform.DOLocalMove(Constants.targetInitialPosition3, 1f);
                return Constants.targetInitialPosition3;
            default:
                return Vector3.zero;
        }
    }

    public Vector3 GetScreenPosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }



}
