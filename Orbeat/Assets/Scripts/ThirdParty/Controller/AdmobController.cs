using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdmobController
{
	#region Variables

	private string appId;
	private string bannerAdUnitId;
	private string rewardedAdUnitId;
	private string interstitialAdUnitId;

	private BannerView bannerView;
	private InterstitialAd interstitial;
	private RewardBasedVideoAd rewardBasedVideo;

	private bool isBannerLoaded = false;

	#endregion Variables

	#region Initialization

	public AdmobController()
	{
		Initialize ();
	}

	public AdmobController(bool loadBanner,bool loadInterstitial,bool loadRewardedVideo)
	{
		Initialize ();
		LoadAds (loadBanner, loadInterstitial, loadRewardedVideo);
	}

	public void Initialize()
	{
		InitializeKeys ();
		InitializeMobileAds ();
		InitializeAdTypes ();
		RegisterEvents ();
	}

	private void InitializeKeys()
	{
		appId = ThirdPartyConstants.kAdMobAppId;
		bannerAdUnitId = ThirdPartyConstants.kAdMobBannerId;
		interstitialAdUnitId = ThirdPartyConstants.kAdMobInterstitalId;
		rewardedAdUnitId = ThirdPartyConstants.kAdMobRewardedVideoId;
	}

	private void InitializeMobileAds()
	{
		MobileAds.SetiOSAppPauseOnBackground(true);
		MobileAds.Initialize(appId);
	}

	private void InitializeAdTypes()
	{
		rewardBasedVideo = RewardBasedVideoAd.Instance;
		bannerView = new BannerView(bannerAdUnitId, AdSize.SmartBanner, AdPosition.Bottom);
		interstitial = new InterstitialAd (interstitialAdUnitId);
	}
		
	#endregion Initialization

	#region Events Register

	private void RegisterEvents()
	{
		RegisterRewardedVideoEvents ();
		RegisterBannerEvents ();
		RegisterInterstitialEvents ();
	}

	private void RegisterRewardedVideoEvents()
	{
		rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
		rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
		rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
		rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
		rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
		rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
		rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
	}

	private void RegisterBannerEvents()
	{
		bannerView.OnAdLoaded += HandleAdLoaded;
		bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
		bannerView.OnAdOpening += HandleAdOpened;
		bannerView.OnAdClosed += HandleAdClosed;
		bannerView.OnAdLeavingApplication += HandleAdLeftApplication;
	}

	private void RegisterInterstitialEvents()
	{
		interstitial.OnAdLoaded += HandleInterstitialLoaded;
		interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.OnAdOpening += HandleInterstitialOpened;
		interstitial.OnAdClosed += HandleInterstitialClosed;
		interstitial.OnAdLeavingApplication += HandleInterstitialLeftApplication;
	}

	#endregion Events Register

	#region Load Ad

	private void LoadAds(bool loadBanner,bool loadInterstitial,bool loadRewardedVideo)
	{
		if (loadInterstitial)
			LoadInterstitial ();
		
		if (loadRewardedVideo)
			LoadRewardBasedVideo ();
	}

	private AdRequest CreateAdRequest()
	{
		GoogleMobileAds.Api.Mediation.Vungle.VungleRewardedVideoMediationExtras extras = new GoogleMobileAds.Api.Mediation.Vungle.VungleRewardedVideoMediationExtras();
		#if UNITY_ANDROID
		extras.SetAllPlacements(new string[] { "SHEEPREWARDED-0597640"});
		#elif UNITY_IPHONE
		extras.SetAllPlacements(new string[] { "IOS_PLACEMENT_1", "IOS_PLACEMENT_2" });
		#endif

		return new AdRequest.Builder().AddMediationExtras(extras).Build();
	}

	public void LoadBanner()
	{
		bannerView.LoadAd(CreateAdRequest());
	}
		
	public void LoadInterstitial()
	{
		interstitial.LoadAd ( CreateAdRequest ());
	}

	public void LoadRewardBasedVideo()
	{
		rewardBasedVideo.LoadAd (CreateAdRequest (), rewardedAdUnitId);
	}

	#endregion Load Ad

	#region Show Ad

	public void HideBanner()
	{
		if (isBannerLoaded)
		{
			bannerView.Hide();
		}
	}

	public void ShowBanner()
	{
		Debug.Log (" ---- Ad Mob Mediation ---- isBannerLoaded : " + isBannerLoaded);
		if (isBannerLoaded)
		{
			bannerView.Show();
		}
		else
		{
			LoadBanner ();
		}
	}

	public void ShowInterstitial()
	{
		if (IsInterstitialAdLoaded())
		{
			interstitial.Show();
		}
		else
		{
			LoadInterstitial ();
		}
	}

	/**
	 * Call this function to Show Video Ad
	 * Register OnRewardBaseVideoClosed Events To Get A CallBack
	 */

	public void ShowRewardBasedVideo()
	{
#if UNITY_EDITOR
        ThirdPartyEventManager.OnRewardBaseVideoClosed();
#endif
        if (IsRewardedVideoLoaded())
		{
			rewardBasedVideo.Show();
		}
		else
		{
			ThirdPartyEventManager.DoFireRewardBaseVideoNotLoadedEvent ();
			LoadRewardBasedVideo ();
		}
	}

	#endregion Show Ad

	#region Ad Load Confirmation

	public bool IsBannerAdLoaded()
	{
		return isBannerLoaded;
	}

	public bool IsInterstitialAdLoaded()
	{
		return interstitial.IsLoaded ();
	}

	public bool IsRewardedVideoLoaded()
	{
		return rewardBasedVideo.IsLoaded();
	}

	#endregion Ad Load Confirmation

	#region Banner callback handlers

	private void HandleAdLoaded(object sender, EventArgs args)
	{
		Debug.Log (" ---- Ad Mob Mediation ---- Banner Loaded");
		isBannerLoaded = true;
	}

	private void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		Debug.Log (" ---- Ad Mob Mediation ---- Banner Failed To Load");
		isBannerLoaded = false;
		LoadBanner ();
	}

	private void HandleAdOpened(object sender, EventArgs args)
	{
		Debug.Log (" ---- Ad Mob Mediation ---- Banner Opened");
	}

	private void HandleAdClosed(object sender, EventArgs args)
	{
	}

	private void HandleAdLeftApplication(object sender, EventArgs args)
	{
	}

	#endregion
		
	#region Interstitial callback handlers

	private void HandleInterstitialLoaded(object sender, EventArgs args)
	{
		Debug.Log (" ---- Ad Mob Mediation ---- Interstial Loaded");
	}

	private void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		Debug.Log (" ---- Ad Mob Mediation ---- Interstial Failed To Load: " + args.Message);
		LoadInterstitial ();
	}

	private void HandleInterstitialOpened(object sender, EventArgs args)
	{
	}

	private void HandleInterstitialClosed(object sender, EventArgs args)
	{
		LoadInterstitial ();
	}

	private void HandleInterstitialLeftApplication(object sender, EventArgs args)
	{
	}

	#endregion
		
	#region RewardBasedVideo callback handlers

	private void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
	{
		Debug.Log (" ---- Ad Mob Mediation ---- Rewarded Video Loaded");
	}

	private void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		Debug.Log (" ---- Ad Mob Mediation ---- Rewarded Video Failed To Load" + args.Message);
		LoadRewardBasedVideo ();
	}

	private void HandleRewardBasedVideoOpened(object sender, EventArgs args)
	{
	}

	private void HandleRewardBasedVideoStarted(object sender, EventArgs args)
	{
	}

	private void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{
		ThirdPartyEventManager.DoFireRewardBaseVideoClosedEvent ();
		LoadRewardBasedVideo ();
	}

	private void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
	}

	private void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
	{
	}

	#endregion
}