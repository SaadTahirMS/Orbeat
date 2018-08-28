using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants {

    //Initials
    public static float referenceWidth = 740f;
    public static Vector3 orbitsDistance = new Vector3(0.5f,0.5f,0.5f);//distance between orbits
    public static Vector3 orbitResetScale = new Vector3(0.5f, 0.5f, 0.5f);//when to reset the orbits
    public static Vector3 intialOrbitScale = new Vector3(3.5f, 3.5f, 3.5f);//intial value of the orbits


    //Perfect hit text
    public static string[] perfectHitArray = new string[]{
        "PERFECT HIT",
        "BULL'S EYE",
        "SMASHING",
        "AWESOME",
        "DEAD CENTRE",
        "INSANE"
    };


    //Positions
    public static Vector3 playerInitialPosition = new Vector3(135f, 0f, 0f);
    public static Vector3 targetInitialPosition1 = new Vector3(260f, 0f, 0f);
    public static Vector3 targetInitialPosition2 = new Vector3(395f, 0f, 0f);
    public static Vector3 targetInitialPosition3 = new Vector3(530f, 0f, 0f);
    public static Vector3 scoreInitialPosition = new Vector3(0f, -14f, 0f);
    public static Vector3 scoreGameOverPos = new Vector3(0f, 325f, 0f);
    public static int orbitCount = 3;

    //Rotations
    public static float playerMinRotationSpeed = 2f;  //Low Rotation Speed means fast speed
    public static float playerMaxRotationSpeed = 5f;
    public static float targetMinRotationSpeed = 15f;
    public static float targetMaxRotationSpeed = 20f;

    //Movement
    public static float playerMoveSpeed = 70f;    //Increase to decrease time
    public static float playerOrbitScaleSpeed = 70f;
    public static float cameraPosOffset = 240;
    public static float cameraPosTime = 5f;
    public static float cameraOffset = 0.30f;
    public static float orbitsScaleSpeed = 35f;
    public static float targetMoveSpeed = 40f;    //Increase to decrease time


    //Scale
    public static Vector3 playerOrbitInitialScale = new Vector3(1f, 1f, 1f);
    public static Vector3 scoreGameOverScale = new Vector3(1.25f, 1.25f, 1.25f);
    public static Vector3 scoreInitialScale = new Vector3(1f, 1f, 1f);

    //Shot
    public static float playerShotSpeed = 25f;    //Increase to increase shot speed
    public static float perfectHitThreshold = 3f; //range of angle for perfect hit
    public static int targetHitCount = 5; //level up after how many target hits

    //Color
    public static Color timerInitialColor = new Color(255f, 0f, 0f, 0.4f);//red color with 0.4 alpha
    public static float orbitAlpha = 1f;//0.5f;
    public static float perfectTextAlpha = 0f;
    public static float scoreAlpha = 0.5f;

    //Tweening
    public static float transitionTime = .45f; 
    public static float beatTime = 0.195f;
    public static float beatScale = 0.04f;
    public static float scoreBeatTime = .19f;
    public static float scoreBeatScale = 0.25f;
    public static float pitchTime = 1f;
    public static float flashTime = .1f;
    public static float shakeTime = .5f;
    public static float shakeStrength = 1f;
    public static int shakeRandomness = 90;
    public static float warningSpeed = .5f;
    public static float perfectHitFadeTime = .2f;
    public static float lookingOffset = 1f;
    public static float orbitFadeTime = 0.5f;
    public static long vibrateTime = 100;
}
