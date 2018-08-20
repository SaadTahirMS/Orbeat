using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TargetController : CharacterBehaviour
{
    private Vector3 position;
    private int orbitNo;

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
        orbitNo = AssignOrbit();
        position = AssignPosition(orbitNo);
    }

    private Vector3 AssignPosition(int orbitNumber){
        //int randomPos = Random.Range(1, 3);
        switch(orbitNumber){
            case 1:
                //switch(randomPos){
                //    case 1:
                //        return new Vector3(Constants.targetInitialPosition1, 0f, 0f);
                //    case 2:
                //        return new Vector3(-Constants.targetInitialPosition1, 0f, 0f);
                //    case 3:
                //        return new Vector3(0f, -Constants.targetInitialPosition1, 0f);
                //    case 4:
                //        return new Vector3(0f,Constants.targetInitialPosition1, 0f);
                //    default:
                //        return Vector3.zero;
                //}
                return new Vector3(Constants.targetInitialPosition1, 0f, 0f);
            case 2:
                //switch (randomPos)
                //{
                //    case 1:
                //        return new Vector3(Constants.targetInitialPosition2, 0f, 0f);
                //    case 2:
                //        return new Vector3(-Constants.targetInitialPosition2, 0f, 0f);
                //    case 3:
                //        return new Vector3(0f, -Constants.targetInitialPosition2, 0f);
                //    case 4:
                //        return new Vector3(0f, Constants.targetInitialPosition2, 0f);
                //    default:
                //        return Vector3.zero;
                //}
                return new Vector3(Constants.targetInitialPosition2, 0f, 0f);
            case 3:
                //switch (randomPos)
                //{
                //    case 1:
                //        return new Vector3(Constants.targetInitialPosition3, 0f, 0f);
                //    case 2:
                //        return new Vector3(-Constants.targetInitialPosition3, 0f, 0f);
                //    case 3:
                //        return new Vector3(0f, -Constants.targetInitialPosition3, 0f);
                //    case 4:
                //        return new Vector3(0f, Constants.targetInitialPosition3, 0f);
                //    default:
                //        return Vector3.zero;
                //}
                return new Vector3(Constants.targetInitialPosition3, 0f, 0f);
            default:
                return Vector3.zero;
        }
    }

    private int AssignOrbit(){
        return Random.Range(1, Constants.orbitCount + 1);
    }

    public int GetOrbit(){
        return orbitNo;
    }

    public Vector3 GetScreenPosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }


}
