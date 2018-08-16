using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants {

    public static float referenceWidth = 740f;

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
    public static Vector3 playerInitialPosition = new Vector3(180f, 0f, 0f);
    public static Vector3 targetInitialPosition1 = new Vector3(275f, 0f, 0f);
    public static Vector3 targetInitialPosition2 = new Vector3(390f, 0f, 0f);
    public static Vector3 targetInitialPosition3 = new Vector3(500f, 0f, 0f);
    public static Vector3 hurdleInitialPosition1 = new Vector3(-275f, 0f, 0f);
    public static Vector3 hurdleInitialPosition2 = new Vector3(-390f, 0f, 0f);
    public static Vector3 hurdleInitialPosition3 = new Vector3(-500f, 0f, 0f);
    public static Vector3 scoreInitialPosition = new Vector3(0f, -14f, 0f);
    public static Vector3 scoreGameOverPos = new Vector3(0f, 325f, 0f);
    public static int orbitCount = 3;

    //Rotations
    public static float playerMinRotationSpeed = 2f;  //Low Rotation Speed means fast speed
    public static float playerMaxRotationSpeed = 5f;
    public static float targetMinRotationSpeed = 15f;
    public static float targetMaxRotationSpeed = 20f;
    public static float hurdleMinRotationSpeed = 5f;
    public static float hurdleMaxRotationSpeed = 13f;

    //Movement
    public static float playerMoveSpeed = 50f;    //Increase to decrease time
    public static float playerOrbitScaleSpeed = 50f;
    public static float cameraPosOffset = 240;
    public static float cameraPosTime = 5f;
    public static float cameraOffset = 0.25f;

    //Scale
    public static Vector3 playerOrbitInitialScale = new Vector3(1f, 1f, 1f);
    public static Vector3 scoreGameOverScale = new Vector3(1.25f, 1.25f, 1.25f);
    public static Vector3 scoreInitialScale = new Vector3(1f, 1f, 1f);

    //Shot
    public static float playerShotSpeed = 15f;    //Increase to increase shot speed
    public static float perfectHitThreshold = 3f; //range of angle for perfect hit
    public static int targetHitCount = 3; //level up after how many target hits

    //Color
    public static Color timerInitialColor = new Color(255f, 0f, 0f, 0.4f);//red color with 0.4 alpha
    public static float orbitAlpha = 0.5f;
    public static float perfectTextAlpha = 0f;
    public static float scoreAlpha = 0.5f;

    //Tweening
    public static float transitionTime = .4f; //0.25f
    public static float beatTime = 0.185f;
    public static float beatScale = 0.05f;
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
