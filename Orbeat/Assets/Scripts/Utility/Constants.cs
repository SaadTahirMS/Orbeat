using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants {

    public static float referenceWidth = 740f; 

    //Positions
    public static Vector3 playerInitialPosition = new Vector3(155f, 0f, 0f);
    public static Vector3 targetInitialPosition1 = new Vector3(270f, 0f, 0f);
    public static Vector3 targetInitialPosition2 = new Vector3(405f, 0f, 0f);
    public static Vector3 targetInitialPosition3 = new Vector3(540f, 0f, 0f);
    public static Vector3 scoreGameOverPos = new Vector3(0f, 200f, 0f);
    public static int orbitCount = 3;

    //Rotations
    public static float playerMinRotationSpeed = 1f;  //Low Rotation Speed means fast speed
    public static float playerMaxRotationSpeed = 5f;
    public static float targetMinRotationSpeed = 15f;
    public static float targetMaxRotationSpeed = 20f;

    //Movement
    public static float playerMoveSpeed = 50f;    //Increase to decrease time
    public static float playerOrbitScaleSpeed = 50f;
    public static float cameraPosOffset = 240;
    public static float cameraPosTime = 5f;
    //Scale
    public static Vector3 playerOrbitInitialScale = new Vector3(1f, 1f, 1f);
    public static Vector3 scoreGameOverScale = new Vector3(2f, 2f, 2f);
    public static Vector3 scoreInitialScale = new Vector3(1.25f, 1.25f, 1.25f);

    //Shot
    public static float playerShotSpeed = 15f;    //Increase to increase shot speed
    public static float perfectHitThreshold = 3f; //range of angle for perfect hit
    public static int targetHitCount = 5; //level up after how many target hits

    //Color
    public static Color timerInitialColor = new Color(255f, 0f, 0f, 0.4f);//red color with 0.4 alpha

    //Tweening
    public static float transitionTime = .35f; //0.25f
    public static float beatTime = 0.19f;
    public static float beatScale = 0.05f;
    public static float scoreBeatTime = .19f;
    public static float scoreBeatScale = .25f;
    public static float pitchTime = 1f;
    public static float flashTime = .2f;
    public static float shakeTime = .5f;
    public static float shakeStrength = 150f;
    public static int shakeRandomness = 90;
    public static float warningSpeed = .5f;

    public static float lookingOffset = 1f;

}
