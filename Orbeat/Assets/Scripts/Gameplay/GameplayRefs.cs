using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayRefs : MonoBehaviour {

    public PlayerController playerController;
    public List<HurdleController> hurdleControllers;//area that can collide with the player
    public MainOrbitController mainOrbitController;//inner and hurdle together
    public Loudness loudness;
    public GameObject triangleParticles;
    public GameObject hexagonParticles;
    public InputController inputController;
    public Camera cam;

    [Header("Game Settings")]
    [Range(0f,0.95f)]
    public float minHurdleFillAmount;
    [Range(0f, 0.95f)]
    public float maxHurdleFillAmount;
    [Range(0f,5f)]
    public float cameraOffset;
    [Range(1f,50f)]
    public float hurdlesDistance;
    [Range(0f,1f)]
    public float scaleSpeed;
    [Range(1f,15f)]
    public float playerRotationSpeed;
    [Range(10f,50f)]
    public float playerScrollRotationSpeed;
    public bool playerCollision = true;//true for yes and false for no
    [Range(1f,10f)]
    public float minRotateSpeed;
    [Range(1f, 10f)]
    public float maxRotateSpeed;
    [Range(0f,180f)]
    public float rotationOffset;
}
