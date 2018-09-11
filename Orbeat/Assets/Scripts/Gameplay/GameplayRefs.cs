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
    public float minHurdleFillAmount;
    public float maxHurdleFillAmount;
    public float cameraOffset;
    public float hurdlesDistance;
    public float scaleSpeed;
    public float playerRotationSpeed;
    public float playerScrollRotationSpeed;
    public float minRotateSpeed;
    public float maxRotateSpeed;
    //public float rotationOffset;
}
