using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrbitController : MonoBehaviour
{
    public List<Scale> orbits;
    private Sequence scale;
    private Vector3 position;
    public Vector3 Position
    {
        get
        {
            return position;
        }
    }

    private void Start()
    {
        StartScale();
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    public void ChangeState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                StartScale();
                break;
            case GameState.End:
                StopScale();
                break;
            case GameState.TargetHit:
                HitScale();
                break;
        }
    }

    private void StartScale(){
        for (int i = 0; i < orbits.Count;i++){
            orbits[i].DoScale(Vector3.one);
        }
    }

    private void StopScale()
    {
        for (int i = 0; i < orbits.Count; i++)
        {
            orbits[i].StopScale();
        }
    }

    private void HitScale() //scaling on target hit
    {
        //for (int i = 0; i < orbits.Count; i++)
        //{

        //}
    }




}