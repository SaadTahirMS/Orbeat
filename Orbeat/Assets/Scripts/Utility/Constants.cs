using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants {

    public static float referenceWidth = 740f; 

    public static Vector3 playerInitialPosition = new Vector3(130f, 0f, 0f);
    public static Vector3 targetInitialPosition1 = new Vector3(265f, 0f, 0f);
    public static Vector3 targetInitialPosition2 = new Vector3(355f, 0f, 0f);
    public static Vector3 targetInitialPosition3 = new Vector3(445f, 0f, 0f);

    public static Vector3 playerOrbitInitialScale = new Vector3(1f, 1f, 1f);

    //Low Rotation Speed means fast speed
    public static float playerMinRotationSpeed = 1f;
    public static float playerMaxRotationSpeed = 4f;

    //Increase to decrease time
    public static float playerMoveSpeed = 20f; 
    public static float playerOrbitScaleSpeed = playerMoveSpeed*2; 
    public static float targetMinRotationSpeed = 5f;
    public static float targetMaxRotationSpeed = 10f;

    //Increase to increase shot speed
    public static float playerShotSpeed = 10f;

    public static float playerCenterCollisionDistance = 85f;
    public static float playerTargetCollisionDistance = 50f;

    public static float perfectHitThreshold = 2f;
}
