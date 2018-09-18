using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class TestingScript : MonoBehaviour {

	private int buttonPressCount = 0;

	public Text pressCountText; 

	public Text log; 

	void OnEnable()
	{
		ThirdPartyEventManager.OnLoadCloudData += OnLoadCloudData;
		ThirdPartyEventManager.OnLogData += OnLogData;
		ThirdPartyEventManager.OnPurchaseSuccessfull += OnPurchaseSuccessfull;
		ThirdPartyEventManager.OnPurchaseFail += OnPurchaseFail;
	}

	void OnDisable()
	{
		ThirdPartyEventManager.OnLoadCloudData -= OnLoadCloudData;
		ThirdPartyEventManager.OnLogData -= OnLogData;
		ThirdPartyEventManager.OnPurchaseFail -= OnPurchaseFail;
	}

	void Start()
	{
		ThirdPartyController.Instance.Initialize ();
		buttonPressCount=PlayerPrefs.GetInt(GameConstants.HighScore, 0);
		pressCountText.text = buttonPressCount.ToString ();
	}

	void OnLogData(string data)
	{
		log.text = data;
	}

	void OnLoadCloudData(string score)
	{
		buttonPressCount = int.Parse (score);
		pressCountText.text = score;
	}

	public void PressButton()
	{
		buttonPressCount++;
		ThirdPartyController.Instance.analyticsController.SendUnityAnalyticsToServer (GameConstants.ButtonPress);
		PlayerPrefs.SetInt (GameConstants.HighScore, buttonPressCount);
		pressCountText.text = buttonPressCount.ToString ();
		if (ThirdPartyController.Instance.playServicesController.IsUserSignedIn ()) {
			ThirdPartyController.Instance.playServicesController.AddScoreToLeaderboard (ThirdPartyConstants.leaderBoardId, buttonPressCount);
			if (buttonPressCount == 5)
				UnlockAchievment ();
		}
	}

	public void UnlockAchievment()
	{
		ThirdPartyController.Instance.playServicesController.UnlockAchievement (ThirdPartyConstants.achievementId);
	}

	public void SignInUser()
	{
		ThirdPartyController.Instance.playServicesController.SignInUser ();
	}

	public void ShowLeaderBoard()
	{
		ThirdPartyController.Instance.playServicesController.ShowLeaderboardsUI ();
	}

	public void ShowAchievements()
	{
		ThirdPartyController.Instance.playServicesController.ShowAchievementsUI ();
	}

	public void SaveToCloud()
	{
		ThirdPartyController.Instance.playServicesController.SaveDataOnCloud (buttonPressCount.ToString (), GameConstants.kUserStateSaveKey);
	}

	public void LoadData()
	{
		ThirdPartyController.Instance.playServicesController.LoadCloudData (GameConstants.kUserStateSaveKey);
	}

	public void RemoveAds()
	{
		ThirdPartyController.Instance.iAppController.PurchaseItem ("com.mindstormstudios.tinysheep.removeads");
	}

	public void UnlimitedRun()
	{
		ThirdPartyController.Instance.iAppController.PurchaseItem ("com.mindstormstudios.tinysheep.unlimitedrun");
	}

	void OnPurchaseFail(string id,PurchaseFailureReason failureReason)
	{
		OnLogData (id + " Fail " + failureReason);
	}

	void OnPurchaseSuccessfull(string id)
	{
		OnLogData (id + " Success");
	}

	public void ShowBanner()
	{
		ThirdPartyController.Instance.admobController.ShowBanner ();
	}

	public void ShowInterstitial()
	{
		ThirdPartyController.Instance.admobController.ShowInterstitial ();
	}

	public void ShowRewardedVideo()
	{
		ThirdPartyController.Instance.admobController.ShowRewardBasedVideo ();
	}

	public void ShowLocalNotification()
	{
		ThirdPartyController.Instance.localNotificationController.CreateLocalNotification (1, "Test", "Test", 2);
	}

	public void SendEmail()
	{
		ThirdPartyController.Instance.supportController.SendEmailToSupport ();
	}

	public void RateUs()
	{
		ThirdPartyController.Instance.supportController.RateApplication ();
	}

}
