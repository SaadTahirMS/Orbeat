using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainOrbitController : MonoBehaviour
{
    public List<OrbitController> orbitControllers;
    private int rotateDirection;
    private float rotateSpeed;


    public void Initialize(GameplayRefs gameplayRefs)
    {
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            orbitControllers[i].Initialize(gameplayRefs.hurdleControllers[i]);
        }
    }

    public void ChangeState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                Rotate();
                Scale();
                break;
            case GameState.End:
                break;
           
        }
    }

    private void Rotate()
    {
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            rotateDirection = AssignDirection();
            rotateSpeed = AssignRotateSpeed();
            orbitControllers[i].DoRotate(rotateDirection, rotateSpeed);
        }
    }

    private int AssignDirection()
    {
        int direction = Random.Range(0, 2);
        direction = direction > 0 ? 1 : -1;
        return direction;
    }

    private float AssignRotateSpeed()
    {
        return Random.Range(Constants.minRotateSpeed, Constants.maxRotateSpeed);
    }

    //Scaling depends on the hurdleScale
    private void Scale()
    {
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            ScaleIndividual(i);
        }
    }

    private void ScaleIndividual(int index)
    {
        orbitControllers[index].DoScale(Vector3.zero, orbitControllers[index].GetHurdleScale() / 2 * Constants.scaleSpeed);
    }

    //Always the bottom hurdle in hierarchy will collide with the wall 
    public void SortHurdleOrbit()
    {
        orbitControllers[0].transform.SetAsFirstSibling();
    }

    public void SortOrbits()
    {
        int i = 0;
        OrbitController key = orbitControllers[i];
        for (; i < orbitControllers.Count - 1; i++)
        {
            orbitControllers[i] = orbitControllers[i + 1];
        }
        orbitControllers[i] = key;
    }

    public void ResetHurdleOrbitScale()
    {
        orbitControllers[0].SetScale(Constants.maxOrbitScale);
        ScaleIndividual(0);
    }

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



  
}