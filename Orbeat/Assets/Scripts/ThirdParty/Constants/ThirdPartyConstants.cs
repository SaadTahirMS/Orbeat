using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPartyConstants {

	#region Support

	public static string SupportMailToURL = "mailto:shahrukh.rafique%40mindstormstudios.com?subject=_AppName_%20Support&body=%0D%0A%0D%0A%0D%0A%0D%0A**********%0D%0A_Version_%20%7C%20_OS_%20%7C%20_Platform_%20%7C%20_DID_";

	#if UNITY_ANDROID
	public static string AppRatingURL = "https://play.google.com/store/apps/details?id=com.live4fun.wordtris.google";
	#elif UNITY_IOS
	public static string AppRatingURL = "https://play.google.com/store/apps/details?id=com.live4fun.wordtris.google";
	#endif

	#endregion Support

	#region Local Notifications

	public static string SmallIconName = "ic_stat_sheep";
	public static string LargeIconName = "ic_launcher";

	#endregion Local Notifications

	#region InApps Purchasing

	public static string KIAppPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAnTtdm2e194Mg5KujUnobqDqD18dcVEgf91p90cHDQU9xL5RNdPzFYIZ6H2lbfcopjDsQRV0WRgsBUs6HhWbXPrQbkfGIGAuLwq4ttwRvPsGAH0q9UnGjpqlU1bIWzl/A3pnohZxSPOnv6bakXyVHxAFjUZ7emPxXzEFJz2ClEDBzDpXZa79jAlt/W0uQt5E/RFK+p/yyF3yYxi92/YCqJ1TiivmfDGJB/IROzZjbgSK5ok0NLtIkuAHGbvAYdknlk4dEo6fycxxp3YDpTc61v1bbmzPGjlu5SOpM/u/1qsQHdklG6BcJ4DLuSKH1vGyyZMA2DqXbsQ6rFH8MNgLJxwIDAQAB/GyneyjwB1S7RFHOYHTYToYzz7KjLs+qgfFx+w8ZYJpAO98XEOBaARhVYxMSRsU7+mE6V/7AUQGEul6KEIHY3Y10ftY9TI17+hKDNVR25BuNKpQhC4PmzjmYXIyJxa7M6dZ7/qssdWYdRnkPR3HdgX0xP0URT3FtcH//VzTtLgR7wxMma9rl5pnotsFt9mlFcNYPEq8xGVdspjJv4Taoz4hZT1xHdTgJO4mTqIxDuMvvdHkTTk45J7HD4J+hkkYLN7VTtnDida3Zt1oI2dFwHDeperJnhodywIDAQAB";

	public static string[] ConsumableIApps = new string[] { };
	public static string[] NonConsumableIApps = new string[]{ "com.mindstormstudios.gominer.removeads" };

	#endregion InApps Purchasing

	#region Google Play Services

	public static string achievementId = "CgkIxpPRx5oEEAIQAQ";
	public static string leaderBoardId = "CgkIxpPRx5oEEAIQAg";
	public static string gameId = "144568502726";

	#endregion Google Play Services


	#region AdMob

	#if UNITY_ANDROID
	// Chartboost testing kAdMobAppId : ca-app-pub-3848928197586604~8828570575
	// Shahrukh testing kAdMobAppId : ca-app-pub-5568002574023697~6761479932
	public static string kAdMobAppId = "ca-app-pub-3848928197586604~8828570575";



	public static string kAdMobBannerId = "ca-app-pub-5568002574023697/8046641449";
	public static string kAdMobInterstitalId = "ca-app-pub-5568002574023697/9964543566";

	// Vungle testing kAdMobRewardedVideoId : ca-app-pub-3848928197586604/3137260197
	// AdColony testing kAdMobRewardedVideoId : ca-app-pub-3848928197586604/5118703503
	// Unity testing kAdMobRewardedVideoId : ca-app-pub-3848928197586604/2925399401
	// Chartboost testing kAdMobRewardedVideoId : ca-app-pub-3848928197586604/2812104360
	// Shahrukh testing kAdMobRewardedVideoId : ca-app-pub-5568002574023697/2441276765

	public static string kAdMobRewardedVideoId = "ca-app-pub-3848928197586604/3137260197";

	#elif UNITY_IOS
	public static string kAdMobAppId = "";

	public static string kAdMobBannerId = "";
	public static string kAdMobInterstitalId = "";
	public static string kAdMobRewardedVideoId = "";
	#endif

	#endregion AdMob


}
