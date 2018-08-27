using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrbitController : CharacterBehaviour
{
    //public Beat orbit1Beat;
    //public Beat orbit2Beat;
    //public Beat orbit3Beat;

    public List<Beat> orbitBeat;


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
                StartBeat();
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

    private void StartBeat(){
        //orbit1Beat.DoBeat(Vector3.one,Constants.beatScale,Constants.beatTime,1);
        //orbit2Beat.DoBeat(Vector3.one,Constants.beatScale,Constants.beatTime,1);
        //orbit3Beat.DoBeat(Vector3.one,Constants.beatScale,Constants.beatTime,1);
        for (int i = 0; i < orbitBeat.Count;i++){
            orbitBeat[i].DoBeat(Vector3.one, Constants.beatScale, Constants.beatTime, 1);
        }
    }


}