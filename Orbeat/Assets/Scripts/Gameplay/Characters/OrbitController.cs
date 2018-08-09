using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrbitController : CharacterBehaviour
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
                break;
            case GameState.End:
                StopRotation();          
                break;
            case GameState.TargetHit:
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



}