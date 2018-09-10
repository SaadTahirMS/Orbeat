using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayRefs : MonoBehaviour {

    public PlayerController playerController;
    //public MainOrbitController orbitController;//inner and hurdle together
    //public List<HurdleController> hurdleControllers;//area that can collide with the player
    public Loudness loudness;
    public GameObject triangleParticles;
    public GameObject hexagonParticles;
    //[Range(0.025f, 0.25f)]
    public float targetFillAmount;
}
