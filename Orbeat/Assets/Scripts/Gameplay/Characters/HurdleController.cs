using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HurdleController : CharacterBehaviour {
    
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
        int pos = GameplayContoller.Instance.GetOrbitIndex();
        position = AssignPosition(pos);
        //StartCoroutine(RandomPosition());
        transform.DOLocalMove(position, Constants.transitionTime);
    }

    private Vector3 AssignPosition(int pos)
    {
        //int ran = Random.Range(1, Constants.orbitCount + 1);

        switch (pos)
        {
            case 1:
                return Constants.hurdleInitialPosition1;
            case 2:
                return Constants.hurdleInitialPosition2;
            case 3:
                return Constants.hurdleInitialPosition3;
            default:
                return Vector3.zero;
        }
    }

    public int GetOrbit()
    {
        if (position == Constants.hurdleInitialPosition1)
            return 1;
        else if (position == Constants.hurdleInitialPosition2)
            return 2;
        else if (position == Constants.hurdleInitialPosition3)
            return 3;
        else
            return 0;
    }


}
