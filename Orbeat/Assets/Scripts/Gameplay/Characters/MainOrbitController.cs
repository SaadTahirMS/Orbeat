using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainOrbitController : MonoBehaviour
{
    public List<MyScaler> orbitScalers;

    public void Initialize()
    {
        
    }

    public void ChangeState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                break;
            case GameState.End:
                break;
           
        }
    }

    //public void Rotate()
    //{
    //    for (int i = 0; i < rotationComponent.Count; i++)
    //    {
    //        rotationComponent[i].StopRotate();
    //        AssignRandomDirection();
    //        if (i < orbitScalers.Count)//this will assign speeds to orbits
    //            AssignIndividualRotateSpeed(i);
    //        else//this will assign speeds to rest of the rotation components
    //            AssignRotateSpeed();
    //        rotationComponent[i].DoRotate(rotation, direction, rotateSpeed);
    //    }
    //}

    //private void AssignIndividualRotateSpeed(int index){
    //    switch(index){
    //        case 0:
    //            rotateSpeed = Random.Range(Constants.minOrbitSpeed1, Constants.maxOrbitSpeed1);
    //            break;
    //        case 1:
    //            rotateSpeed = Random.Range(Constants.minOrbitSpeed2, Constants.maxOrbitSpeed2);
    //            break;
    //        case 2:
    //            rotateSpeed = Random.Range(Constants.minOrbitSpeed3, Constants.maxOrbitSpeed3);
    //            break;
    //        case 3:
    //            rotateSpeed = Random.Range(Constants.minOrbitSpeed4, Constants.maxOrbitSpeed4);
    //            break;
    //        case 4:
    //            rotateSpeed = Random.Range(Constants.minOrbitSpeed5, Constants.maxOrbitSpeed5);
    //            break;
    //        case 5:
    //            rotateSpeed = Random.Range(Constants.minOrbitSpeed6, Constants.maxOrbitSpeed6);
    //            break;
    //        case 6:
    //            rotateSpeed = Random.Range(Constants.minOrbitSpeed7, Constants.maxOrbitSpeed7);
    //            break;
    //    }
    //}

    //public Tween Scale(int orbitIndex,Vector3 outerValue){
    //    return orbitScalers[orbitIndex].DoScale(outerValue, Constants.transitionTime);
    //}

  
}