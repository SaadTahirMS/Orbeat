using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HurdleController : CharacterBehaviour {

    public int orbitPos;
    private Vector3 position;
    public int myDirection;

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
            return Constants.hurdleMinRotationSpeed;
        }
    }

    public override float MaxRotateSpeed
    {
        get
        {
            return Constants.hurdleMaxRotationSpeed;
        }
    }

    public override void Initialize()
    {
        base.Initialize();
    }


    public void ChangeState(GameState state)
    {

        switch (state)
        {
            case GameState.Start:
                SetPosition();
                Rotate();
                break;
            case GameState.End:
                StopRotation();
                break;
        }
    }

    protected override void SetPosition()
    {
        position = AssignPosition();
        transform.DOLocalMove(position, Constants.transitionTime);
    }

    private Vector3 AssignPosition()
    {
        //int ran = Random.Range(1, 3);
        //int ran = 1;
        int ran = orbitPos;
        switch (ran)
        {
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

    protected override void AssignRandomDirection()
    {
        direction = myDirection;
    }
}
