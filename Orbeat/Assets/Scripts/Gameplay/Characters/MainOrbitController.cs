using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainOrbitController : MonoBehaviour
{
    public List<OrbitController> orbitControllers;
    private int rotateDirection;
    private float rotateSpeed;
    GameplayRefs gprefs;

    public void Initialize(GameplayRefs gameplayRefs)
    {
        gprefs = gameplayRefs;
        CanRotate(gameplayRefs.canRotateOrbits);
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
                SetInitialScale();
                Rotate();
                Scale();
                break;
            case GameState.Restart:
                break;   
            case GameState.Quit:
                StopScale();
                StopRotate();
                break;
        }
    }

    private void SetOrbitState(bool state)
    {
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            orbitControllers[i].gameObject.SetActive(state);
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

    private void ResetScales()
    {
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            orbitControllers[i].SetScale(Vector3.one);
        }
    }

    public List<OrbitController> GetOrbits(){
        return orbitControllers;
    }

    private Vector3 CalculateScale(int a)
    {
        return Constants.playerOrbitScale + (a * Constants.hurdlesDistance);
    }

    private void SetInitialScale()
    {
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            orbitControllers[i].SetScale(CalculateInitialScale(i));
        }
    }

    private Vector3 CalculateInitialScale(int a)
    {
        return Constants.hurdlesInitialDistance + (a * Constants.hurdlesDistance); 
    }

    //Scaling depends on the hurdleScale
    private void Scale()
    {
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            ScaleIndividual(Vector3.zero,i);
        }
    }

    private void ScaleIndividual(Vector3 endValue,int index)
    {
        orbitControllers[index].DoScale(endValue, orbitControllers[index].GetHurdleScale().x / 2 * Constants.scaleSpeed);
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
        orbitControllers[0].SetScale(CalculateScale(orbitControllers.Count));
        ScaleIndividual(Vector3.zero,0);//scale down to this value
    }

    private void StopScale()
    {
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            orbitControllers[i].StopScale();
        }
    }

    private void StopRotate()
    {
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            orbitControllers[i].StopRotate();
        }
    }

    //Scale of the first hurdle in the list
    public void SetNewScale(){
        //get the current scale of last index hurdle
        Vector3 lastHurdleScale = orbitControllers[orbitControllers.Count - 1].GetHurdleScale();
        //add the hurdle distance to this scale
        Vector3 newScale = lastHurdleScale + Constants.hurdlesDistance;
        //this is the new scale of this orbit
        orbitControllers[0].SetScale(newScale);
        ScaleIndividual(Vector3.zero, 0);//scale down to this value

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

    //rotation offset on the last orbit depending on the previous orbit rotation
    public void RotationOffset()
    {
        int i = orbitControllers.Count - 1; //last orbit
        float previousRotation = orbitControllers[i-1].transform.localRotation.eulerAngles.z; //2nd last orbit
        float randomOffset = Random.Range(-Constants.rotationOffset, Constants.rotationOffset);
        orbitControllers[i].RotationOffset(previousRotation + randomOffset);
    }

    public void CanRotate(bool flag){
        for (int i = 0; i < orbitControllers.Count;i++){
            orbitControllers[i].rotate.canRotate = flag;
        }
    }
     
}