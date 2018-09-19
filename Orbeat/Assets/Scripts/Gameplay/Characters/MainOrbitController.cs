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
        //CanRotate(gameplayRefs.canRotateOrbits);
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            orbitControllers[i].Initialize(gameplayRefs.hurdleControllers[i],i);
        }
    }

    public void ChangeState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                SetInitialScale();
                CanRotate(true);
                Rotate();
                Scale();
                break;
            case GameState.Restart:
                break;   
            case GameState.Quit:
                StopScale();
                CanRotate(false);
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

    public void ChangeDirection(){
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            orbitControllers[i].ChangeDirection();
        }
    }

    //public void SetNewRotation(float initialRotation,int direction,float speed){
    //    orbitControllers[0].transform.localRotation = Quaternion.Euler(0f, 0f, initialRotation);
    //    orbitControllers[0].DoRotate(direction, speed);
    //}

    public void SetNewRotations(float initialRotation,int direction,float speed){
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            //orbitControllers[i].transform.localRotation = Quaternion.Euler(0f, 0f, initialRotation);
            orbitControllers[i].transform.DOLocalRotate(new Vector3(0f, 0f, initialRotation),1f);
            orbitControllers[i].DoRotate(direction, speed);
        }
    }


    public void SetRotateSpeed(int direction, float speed)
    {
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            orbitControllers[i].DoRotate(direction, speed);
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

    //assigning new rotation to the hurdle that hit the orbit
    public void AssignNewRotation(){
        float newRotateSpeed = AssignRotateSpeed();
        int newDirection = AssignDirection();
        orbitControllers[0].DoRotate(newDirection, newRotateSpeed);
    }

    //assigning new rotation to the hurdle that hit the orbit
    public void AssignNewRotations()
    {
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            float newRotateSpeed = AssignRotateSpeed();
            int newDirection = AssignDirection();
            orbitControllers[i].DoRotate(newDirection, newRotateSpeed);
        }

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

    public void SetInitialScale()
    {
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            orbitControllers[i].SetScale(CalculateInitialScale(i));
        }
    }

    public Vector3 CalculateInitialScale(int a)
    {
        return Constants.hurdlesInitialDistance + (a * Constants.hurdlesDistance); 
    }

    //Scaling depends on the hurdleScale
    public void Scale()
    {
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            ScaleIndividual(Vector3.zero,i);
        }
    }

    //Scaling depends on the hurdleScale
    public void ScaleTo(Vector3 endValue)
    {
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            ScaleIndividualWithTime(endValue + i * Constants.hurdlesDistance, i, 2f);
        }
    }

    ////Scaling depends on the hurdleScale
    //public void ScaleFrom(Vector3 endValue)
    //{
    //    for (int i = 0; i < orbitControllers.Count; i++)
    //    {
    //        ScaleIndividual(endValue + i * Constants.hurdlesDistance, i, 1f);
    //    }
    //}


    private void ScaleIndividual(Vector3 endValue,int index)
    {
        orbitControllers[index].DoScale(endValue, orbitControllers[index].GetHurdleScale().x / 2 * Constants.scaleSpeed);
    }

    private void ScaleIndividualWithTime(Vector3 endValue, int index,float duration)
    {
        orbitControllers[index].DoScale(endValue, duration);
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

    //public void ResetHurdleOrbitScale()
    //{
    //    orbitControllers[0].SetScale(CalculateScale(orbitControllers.Count));
    //    ScaleIndividual(Vector3.zero,0);//scale down to this value
    //}

    public void StopScale()
    {
        for (int i = 0; i < orbitControllers.Count; i++)
        {
            orbitControllers[i].StopScale();
        }
    }

    //private void StopRotate()
    //{
    //    for (int i = 0; i < orbitControllers.Count; i++)
    //    {
    //        orbitControllers[i].StopRotate();
    //    }
    //}

    //Scale of the hurdle after hit
    public void SetNewScale(){
        //get the current scale of last index hurdle
        Vector3 lastHurdleScale = orbitControllers[orbitControllers.Count - 1].GetHurdleScale();
        //add the hurdle distance to this scale
        Vector3 newScale = lastHurdleScale + Constants.hurdlesDistance;
        //this is the new scale of this orbit
        orbitControllers[0].SetScale(newScale);
        ScaleIndividual(Vector3.zero, 0);//scale down to this value

    }


    public void SetNewInitialScale()
    {
        orbitControllers[0].SetScale(CalculateInitialScale(0));
        ScaleIndividual(Vector3.zero, 0);
    }

    //rotation offset on the last orbit depending on the previous orbit rotation
    public void RotationOffset()
    {
        int i = orbitControllers.Count - 1; //last orbit
        float previousRotation = orbitControllers[i-1].transform.localRotation.eulerAngles.z; //2nd last orbit
        float randomOffset = Random.Range(-Constants.rotationOffset, Constants.rotationOffset);
        orbitControllers[i].RotationOffset(previousRotation + randomOffset);
    }

    //public void SetNewRotationOffset()
    //{
    //    int i = orbitControllers.Count - 1;//last orbit
    //    float previousRotation = orbitControllers[i].transform.localRotation.eulerAngles.z;
    //    orbitControllers[0].RotationOffset(previousRotation + Constants.rotationOffset);//add to this orbit
    //}

    public void SetNewRotationOffset()
    {
        for (int i = 1; i < orbitControllers.Count; i++){
            float previousRotation = orbitControllers[i-1].transform.localRotation.eulerAngles.z;
            orbitControllers[i].RotationOffset(previousRotation + Constants.rotationOffset);
        }
        //int i = orbitControllers.Count - 1;//last orbit
        //float previousRotation = orbitControllers[i].transform.localRotation.eulerAngles.z;
        //orbitControllers[0].RotationOffset(previousRotation + Constants.rotationOffset);//add to this orbit
    }

    public void CanRotate(bool flag){
        for (int i = 0; i < orbitControllers.Count;i++){
            orbitControllers[i].rotate.canRotate = flag;
            //orbitControllers[i].rotate.speed = 0.1f;
        }
    }


     
}