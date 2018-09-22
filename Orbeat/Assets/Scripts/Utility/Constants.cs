using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants {

	#region Player Prefs

	public const int kEncyptionStringKey = 910867680;

	public const string MusicState = "MusicState";
	public const string SoundState = "SoundState";

	public const string isFirstSession 		= "isFirstSession";
	public const string isRateUsClicked 		= "isRateUsClicked";

	public const string isNotificationsOn 		= "isNotificationsOn";

	public const string noAdsPurchased 		= "noAdsPurchased";

	public const string highScore 		= "highScore";
	public const string playerName 		= "playerName";
	public const string playerIconId 		= "playerIconId";

	#endregion Player Prefs

    public static float difficultyLevel = 50;

    //Player
    public static Vector3 playerInitialPosition = new Vector3(0f, 225f, 0f);
    public static float playerRotationSpeed = 7.5f;
    public static float playerTapRotateSpeed = 5f;

    public static float playerScrollRotationSpeed = 30f;
    public static Vector3 playerOrbitScale = new Vector3(4f, 4f, 4f);
    public static bool playerCollision = true;

    //Sound
    public static float pitchTime = .75f;

    //Tweens
    public static float transitionTime = 0.25f;
    public static float playerTransitionTime = 0.15f;
    public static float fillAmountTime = 1f;
    public static float colorTransitionTime = 1f;
    public static float minColorTimer = 0.2f;
    public static float maxColorTimer = 0.8f;

    //Hurdle
    public static float hurdleIncreaseAmount = 0.1f;
    //public static float minHurdleFillAmount = 0.45f;
    //public static float maxHurdleFillAmount = 0.95f;
    public static float hurdleFillAmount = 0.25f; //increases with time to reduce the gap
    public static Vector3 hurdleWidth = new Vector3(1f, 1f, 1f); //1 is max and 0.1 is min
    public static Vector3 hurdlesDistance = new Vector3(10f, 10f, 10f);
    public static Vector3 hurdlesInitialDistance = new Vector3(24f, 24f, 24f);
    public static Vector3 initialDistanceAfterSpecial = new Vector3(27f, 27f, 27f);
    public static float initialDistanceAfterSpecialFactor = 4.3f; // 0.6667/ min speed
    public static int hurdleFillChangeScore = 35;


    //Orbits
    public static float minRotateSpeed = 5f; //Low Rotation Speed means fast speed
    public static float maxRotateSpeed = 10f;
    public static float scaleSpeed = 0.25f;//0.001 - 1
    public static float rotationOffset = 45f; // plus minus previous value
    public static Vector3 edgeInitialPos = new Vector3(1175f,0f,0f);
    public static float edgeDistance = 50f;
    public static float minNormalModeTime = 12;
    public static float maxNormalModeTime = 22;

    //Camera 
    public static float cameraOffset = 1f;

}
