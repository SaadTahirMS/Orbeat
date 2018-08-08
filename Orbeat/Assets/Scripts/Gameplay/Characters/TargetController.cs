using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TargetController : CharacterBehaviour
{
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
                Position();
                Rotate();
                break;
            case GameState.End:
                StopRotation();
                Reset();
                break;
        }
    }

    private void Position(){
        AssignPosition();
    }

    private void AssignPosition(){
        int ran = Random.Range(1,4);
        print("Target pos: "+ran);
        switch(ran){
            case 1:
                //transform.localPosition = Constants.targetInitialPosition1;
                transform.DOLocalMove(Constants.targetInitialPosition1, 1f);
                break;
            case 2:
                //transform.localPosition = Constants.targetInitialPosition2;
                transform.DOLocalMove(Constants.targetInitialPosition2, 1f);
                break;
            case 3:
                //transform.localPosition = Constants.targetInitialPosition3;
                transform.DOLocalMove(Constants.targetInitialPosition3, 1f);
                break;
        }
    }

    private void Reset()
    {
        ResetPosition();
    }

    private void ResetPosition()
    {
        //transform.localPosition = Vector3.zero;
        transform.DOLocalMove(Vector3.zero, 1f);
    }

}
