using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants {

	#region Store

	public const int sackOfCoins = 1000;
	public const int sackOfCandies = 1000;

	public static string[] consumableItemsPrices = new string[]{ "$100", "$100" };
	public static string[] nonConsumableItemsPrices = new string[]{ "100" };

	#endregion Store

	#region Local Notifications

	public const int localNotificationTimeInHours = 12;

	#endregion Local Notifications

	#region Ads

	public const int videoAdReward = 100;

	#endregion Ads

	#region Analytics

	public const string ButtonPress = "ButtonPress";

	#endregion Analytics

	#region Google Play Services User State Key

	public const string kUserStateSaveKey = "USERSTATE";

	#endregion Google Play Services User State Key

	#region PlayerPrefs Ids

	public const int kEncyptionStringKey = 910867680;

	public const string isNotificationsOn 		= "isNotificationsOn";

	public const string noAdsPurchased 		= "noAdsPurchased";

	public const string playerCash 					= "playerCash";
	public const string playerGemCount 				= "playerGemCount";
	public const string playerOfflineEarning 		= "playerOfflineEarning";
	public const string playerDistance 		= "playerDistance";
	public const string currentDepthUpgradeIndex 	= "currentDepthUpgradeIndex";
	public const string currentGemCollectionUpgradeIndex 		= "currentGemCountIndex";
	public const string currentOfflineEarningUpgradeIndex = "currentOfflineEarningUpgradeIndex";

	public const string UserState = "UserState";
	public const string HighScore = "HighScore";

	public const string MusicState = "MusicState";
	public const string SoundState = "SoundState";
    public const string ApplicationPauseTime = "ApplicationPauseTime";
	public const string DiscoveredGem = "DiscoveredGem";

	#endregion PlayerPrefs Ids

	#region Path Management

	public const int canvasWidth = 640;

	public const int propsPoolCount = 50;

	public const int HurdleOccuranceProbability = 10;
	public const int CoinOccuranceProbability = 100;

	public const int minYPropsDistance = 80;
	public const int maxYPropsDistance = 550;

	public const int minXPropsPosition = -210;
	public const int maxXPropsPosition = 210;

	public const float minPropMovementSpeed = 1.0f;
	public const float maxPropMovementSpeed = 3.0f;

	#endregion Path Management

	#region Player Movement

	public const float playerMovementFactor = 160;
	public const float playerXBound = 220;

	public const float playerYPosition 		= 150;
	public const float playerLeftXPosition 	= 235;
	public const float playerRightXPosition = 100;

	#endregion Player Movement

	#region Game Play

	public const double initialPlayerCash = 10000;

	public const int laneTransitionOffset = 60;

	public const float scoreMultiplier = 0.2f;

	public const float gameSpeed = 1900;
	public const float gameSpeedAspectRatio = 0.466f; // iphoneX aspect ration

	public const float upSpeedfactor = 15;
	public const float downSpeedfactor = 20;
	public const float distanceConversionFactor = 0.0714f;

	public static float bottomStayCRTime = 0.5f;
	public static float goalReachedCRTime = 0.35f;

	#endregion Game Play

	#region Mode Selection

	public static bool isPathInverse = true;

	#endregion

	#region Character Selection

	public static int SelectedCharacter = 0;

	#endregion
}
