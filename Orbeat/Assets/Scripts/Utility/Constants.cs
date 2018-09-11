using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants {

    //Player
    public static Vector3 playerInitialPosition = new Vector3(0f, 175f, 0f);
    public static float playerRotationSpeed = 10f;

    //Sound
    public static float pitchTime = .75f;

    //Start Transition
    public static float transitionTime = 0.25f;
    public static float playerTransitionTime = 0.15f;

    //Hurdle
    public static float hurdleIncreaseAmount = 0.1f;
    public static float minHurdleFillAmount = 0.45f;
    public static float maxHurdleFillAmount = 0.95f;
    public static Vector3 hurdleWidth = new Vector3(1f, 1f, 1f); //1 is max and 0.1 is min
    public static Vector3 hurdlesDistance = new Vector3(10f, 10f, 10f);

    //Orbits
    public static float minRotateSpeed = 1f; //Low Rotation Speed means fast speed
    public static float maxRotateSpeed = 10f;
    public static Vector3 minOrbitScale = new Vector3(14f, 14f, 14f);
    public static Vector3 maxOrbitScale = new Vector3(44f, 44f, 44f);
    public static float scaleSpeed = 0.5f;//0.001 - 1

    //Initials
    //public static float referenceWidth = 740f;
    ////public static Vector2 orbitsDistance = new Vector2(250f,250f);//distance between orbits
    ////public static Vector2 orbitResetScale = new Vector2(100f,100f);//when to reset the orbits
    ////public static Vector2 intialOrbitScale = new Vector2(2150f,2150f);//intial value of the orbits
    ////public static List<int> availablePositions = new List<int>() { 1, 2, 3 };//array of positions
    ////public static Vector2 orbitReduceScale = new Vector2(100f, 100f);//how much scale to reduce for each orbit
    //public static Vector3 orbitsDistance = new Vector3(2.5f, 2.5f,2.5f);//distance between orbits
    //public static Vector3 orbitResetScale = new Vector3(1f, 1f,1f);//when to reset the orbits
    //public static Vector3 intialOrbitScale = new Vector3(21.5f, 21.5f,21.5f);//intial value of the orbits
    //public static Vector3 orbitReduceScale = new Vector3(1f, 1f,1f);//how much scale to reduce for each orbit
    //public static Vector3 innerOuterDistance = new Vector3(1f, 1f, 1f);//distance b/w inner and outer
    //public static float maxTargetFillAmount = 0.25f; // 1 is fully filled and 0 empty
    //public static float minTargetFillAmount = 0.025f; // 1 is fully filled and 0 empty
    //public static float targetReduceAmount = 0.005f;
    //public static float boundaryOffset = 0.1f;
    //public static int totalTargets = 7;

    ////Perfect hit text
    //public static string[] perfectHitArray = new string[]{
    //    "PERFECT HIT",
    //    "BULL'S EYE",
    //    "SMASHING",
    //    "AWESOME",
    //    "DEAD CENTRE",
    //    "INSANE"
    //};


    ////Positions
    //public static Vector3 playerInitialPosition = new Vector3(175f, 0f, 0f);
    //public static Vector3 targetInitialPosition1 = new Vector3(260f, 0f, 0f);
    //public static Vector3 targetInitialPosition2 = new Vector3(395f, 0f, 0f);
    //public static Vector3 targetInitialPosition3 = new Vector3(530f, 0f, 0f);
    //public static Vector3 scoreInitialPosition = new Vector3(0f, 0f, 0f);
    //public static Vector3 scoreGameOverPos = new Vector3(0f, 325f, 0f);
    //public static int orbitCount = 3;
    //public static float edgeInitialPos = 300f;
    //public static float edgeDistance = 125f;

    ////Rotations
    //public static float playerMinRotationSpeed = 2f;  //Low Rotation Speed means fast speed
    //public static float playerMaxRotationSpeed = 5f;
    //public static float minOrbitSpeed1 = 1f;  
    //public static float maxOrbitSpeed1 = 2f;
    //public static float minOrbitSpeed2 = 2f;
    //public static float maxOrbitSpeed2 = 3f;
    //public static float minOrbitSpeed3 = 4f;
    //public static float maxOrbitSpeed3 = 5f;
    //public static float minOrbitSpeed4 = 5f;
    //public static float maxOrbitSpeed4 = 6f;
    //public static float minOrbitSpeed5 = 6f;
    //public static float maxOrbitSpeed5 = 7f;
    //public static float minOrbitSpeed6 = 8f;
    //public static float maxOrbitSpeed6 = 9f;
    //public static float minOrbitSpeed7 = 9f;
    //public static float maxOrbitSpeed7 = 10f;

    ////Movement
    //public static float playerMoveSpeed = 50f;    //Increase to decrease time
    //public static float playerOrbitScaleSpeed = 50f;
    //public static float cameraPosOffset = 240;
    //public static float cameraPosTime = 5f;
    //public static float cameraOffset = 2f;//0.30f;
    //public static float orbitsScaleSpeed = 1f;
    //public static float targetMoveSpeed = 11f;    //Increase to decrease time
    //public static Vector3 edgeMovementReduceValue = new Vector3(50f, 0f, 0f);

    ////Scale
    //public static Vector3 playerOrbitInitialScale = new Vector3(4f, 4f);
    //public static Vector3 scoreGameOverScale = new Vector3(2f, 2f);
    //public static Vector3 scoreInitialScale = new Vector3(1f, 1f, 1f);
    ////public static Vector3 scoreInitialScaleWH = new Vector3(90f, 90f, 90f);
    //public static float orbitScaleThreshold = 0.25f; //when the orbits will active false on target hit

    ////Shot
    //public static float playerShotSpeed = 8f;    //Increase to increase shot speed
    //public static float perfectHitThreshold = 3f; //range of angle for perfect hit
    //public static int targetHitCount = 5; //level up after how many target hits
    //public static int targetID = 1; //target ID of which target u hit

    ////Color
    //public static Color timerInitialColor = new Color(255f, 0f, 0f, 0.4f);//red color with 0.4 alpha 7117AFFF
    //public static Color timerWarningColor = new Color(255f, 0f, 0f, 0.8f);//red color with 0.4 alpha 7117AFFF
    //public static float orbitAlpha = 1f;//0.5f;
    //public static float perfectTextAlpha = 0f;
    //public static float scoreAlpha = 0.5f;

    ////Tweening
    //public static float transitionTime = .35f;
    //public static float playertransitionTime = .15f;
    //public static float beatTime = 0.195f;
    //public static float beatScale = 0.04f;
    //public static float scoreBeatTime = .19f;
    //public static float scoreBeatScale = 0.25f;
    //public static float pitchTime = .75f;
    //public static float flashTime = .1f;
    //public static float shakeTime = .5f;
    //public static float shakeStrength = 1f;
    //public static int shakeRandomness = 90;
    //public static float warningSpeed = .5f;
    //public static float perfectHitFadeTime = .2f;
    //public static float lookingOffset = 1f;
    //public static float orbitFadeTime = 0.5f;
    //public static long vibrateTime = 100;
    //public static float targetSizeTime = 1f;
}
