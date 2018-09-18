using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameplayPattern {

    public int direction; // 1 and -1
    public bool pingpong;
    public float initialRotation;
    public float rotationSpeed;
    public float initialRotationSpeed;
    public float rotationOffset;
    public Vector3 hurdleDistance;
    public Vector3 hurdleInitialDistance;
    public float hurdleFillAmount;
    public float scaleSpeed;
}
