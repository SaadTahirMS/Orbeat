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
    public HalfScreenTouchMovement gameControls;
    [Range(0.45f, 0.95f)]
    public float hurdleFillAmount;
}
