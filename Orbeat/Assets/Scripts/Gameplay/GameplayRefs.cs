using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayRefs : MonoBehaviour {

    public List<OrbitController> intialOrbitList;
    public PlayerController playerController;
    public List<HurdleController> hurdleControllers;//area that can collide with the player
    public MainOrbitController mainOrbitController;//inner and hurdle together
    public ColorController colorController;
    public Loudness loudness;
    public GameObject triangleParticles;
    public GameObject hexagonParticles;
    public InputController inputController;
    public Camera cam;

    //Coloring objects
    public List<Image> innerOrbitsImg;
    public Image playerObjImg;
    public Image playerOrbitImg;
    public Image playerOrbitGlowImg;
    public List<Image> hurdleOrbitsImg;
    public Text scoreText;
    public List<ParticleSystem> particles;

    [Header("Game Settings")]
    //public string inputMethod;
    //[Range(0f,0.95f)]
    //public float minHurdleFillAmount;
    //[Range(0f, 0.95f)]
    //public float maxHurdleFillAmount;
    //[Range(5f, 50f)]
    //public float hurdlesDistance;
    //[Range(0.1f,0.9f)]
    //public float hurdleFillAmount;
    //[Range(0.1f, 0.5f)]
    //public float scaleSpeed;
    //[Range(0f,5f)]
    //public float cameraOffset;
    ////public bool canRotateOrbits;
    //[Range(1f, 50f)]
    //public float minRotateSpeed;
    //[Range(1f, 50f)]
    //public float maxRotateSpeed;
    //[Range(0f, 180f)]
    //public float rotationOffset;
    //[Range(1f,15f)]
    //public float playerRotationSpeed;
    //[Range(10f,50f)]
    //public float playerScrollRotationSpeed;
    public bool playerCollision;//true for yes and false for no

    [Header("Progression Curves")]
    public float minHurdleDistance = 5f;
    public float maxHurdleDistance = 50f;
    public AnimationCurve hurdleDistanceCurve;
    public float minHurdleFillAmount = 0.1f;
    public float maxHurdleFillAmount = 0.9f;
    public AnimationCurve hurdleFillAmountCurve;
    public float minScaleSpeed = 0.1f;
    public float maxScaleSpeed = 0.5f;
    public AnimationCurve scaleSpeedCurve;
    public float minRotationOffset = 0f;
    public float maxRotationOffset = 180f;
    public AnimationCurve rotationOffsetCurve;
    public float minOrbitRotateSpeed = 0f;
    public float maxOrbitRotateSpeed = 100f;
    public AnimationCurve orbitRotationCurve;
}
