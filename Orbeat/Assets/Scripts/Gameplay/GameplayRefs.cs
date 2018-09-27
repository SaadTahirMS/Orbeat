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
    public GameObject tutorialBtns;
    public Image flashImg;

    //Coloring objects
    public List<Image> innerOrbitsImg;
    public Image playerObjImg;
    public Image playerOrbitImg;
    public Image playerOrbitGlowImg;
    public List<Image> hurdleOrbitsImg;
    public Text scoreText;
    public List<ParticleSystem> particles;

    public bool playerCollision;//true for yes and false for no

    [Header("Progression Curves")]
    //public float minHurdleDistance = 5f;
    //public float maxHurdleDistance = 50f;
    public AnimationCurve hurdleDistanceCurve;
    //public float minHurdleFillAmount = 0.1f;
    //public float maxHurdleFillAmount = 0.9f;
    public AnimationCurve hurdleFillAmountCurve;
    //public float minScaleSpeed = 0.1f;
    //public float maxScaleSpeed = 0.5f;
    public AnimationCurve scaleSpeedCurve;
    //public float minRotationOffset = 0f;
    //public float maxRotationOffset = 180f;
    //public AnimationCurve rotationOffsetCurve;
    //public float minOrbitRotateSpeed = 0f;
    //public float maxOrbitRotateSpeed = 100f;
    public AnimationCurve orbitRotationCurve;
}
