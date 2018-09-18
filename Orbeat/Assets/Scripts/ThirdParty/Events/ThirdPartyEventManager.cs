using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.SocialPlatforms;

public class ThirdPartyEventManager {

	#region In-App Purchases Delegates
	
	public delegate void PurchaseSuccessfull(string purchasedProductID);
	public static PurchaseSuccessfull OnPurchaseSuccessfull;

	public delegate void PurchaseFail(string productID, PurchaseFailureReason failureReason);
	public static PurchaseFail OnPurchaseFail;

	public static void DoFirePurchaseSuccessFullEvent(string purchasedProductID)
	{
		if (OnPurchaseSuccessfull != null)
			OnPurchaseSuccessfull (purchasedProductID);	
	}

	public static void DoFirePurchaseFailEvent(string productID, PurchaseFailureReason failureReason)
	{
		if (OnPurchaseFail != null)
			OnPurchaseFail (productID, failureReason);
	}

	#endregion In-App Purchases Delegates

	#region Play Services Delegates

	public delegate void LoadLeaderBoardScore(IScore[] score);
	public static LoadLeaderBoardScore OnLoadLeaderBoardScore;

	public delegate void LoadCloudSavedData(string userState);
	public static LoadCloudSavedData OnLoadCloudData;

	public delegate void SignInComplete();
	public static SignInComplete OnSignInComplete;

	public static void DoFireLoadLeaderBoardScoreEvent(IScore[] score)
	{
		if (OnLoadLeaderBoardScore != null)
			OnLoadLeaderBoardScore (score);
	}

	public static void DoFireLoadCloudSavedDataEvent(string userState)
	{
		if (OnLoadCloudData != null)
			OnLoadCloudData (userState);
	}

	public static void DoFireSignInSuccessfullEvent()
	{
		if (OnSignInComplete != null)
			OnSignInComplete ();
	}

	#endregion Play Services Delegates

	#region Ads Delegates

	public delegate void RewardBaseVideoClosed();
	public static RewardBaseVideoClosed OnRewardBaseVideoClosed;

	public delegate void RewardBaseVideoNotLoaded();
	public static RewardBaseVideoNotLoaded OnRewardBaseVideoNotLoaded;

	public static void DoFireRewardBaseVideoClosedEvent()
	{
		if (OnRewardBaseVideoClosed != null)
			OnRewardBaseVideoClosed ();
	}

	public static void DoFireRewardBaseVideoNotLoadedEvent()
	{
		if (OnRewardBaseVideoNotLoaded != null)
			OnRewardBaseVideoNotLoaded ();
	}

	#endregion Ads Delegates

	#region Logs

	public delegate void LogData(string userState);
	public static LogData OnLogData;

	public static void DoFireLogDataEvent(string data)
	{
		if (OnLogData != null)
			OnLogData (data);
	}

	#endregion Logs

	#region UnAssignEvents

	public static void UnAssignAllEvents()
	{
		OnPurchaseSuccessfull = null;
		OnPurchaseFail = null;
		OnLoadLeaderBoardScore = null;
		OnLoadCloudData = null;
		OnSignInComplete = null;
		OnRewardBaseVideoClosed = null;
		OnRewardBaseVideoNotLoaded = null;
		OnLogData = null;
	}

	#endregion UnAssignEvents
}
