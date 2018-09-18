using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi.SavedGame;
using System.Text;

public class PlayServicesController {

	#region Variables

	private string SAVE_VALUE = "";

	private bool isSaving;

	private IAchievement[] achievement;

	public IAchievement[] Achievements
	{
		get { return achievement; }
		private set { achievement = value; }
	}

	#endregion Variables

	#region Initialization

	public PlayServicesController()
	{
		InitializeGameServices ();
	}

	private void InitializeGameServices()
	{
		#if UNITY_ANDROID
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			.RequestEmail()
			.AddOauthScope("profile")
			.EnableSavedGames()
			.Build();

		PlayGamesPlatform.InitializeInstance(config);
		if (Debug.isDebugBuild)
		{
			PlayGamesPlatform.DebugLogEnabled = true;
		}
		PlayGamesPlatform.Activate();
		#elif UNITY_IPHONE
		Social.localUser.Authenticate (AuthenticationCallBack);
		#endif
		ThirdPartyEventManager.DoFireLogDataEvent ("InitializeGameServices");
	}

	#endregion Initialization

	private void AuthenticationCallBack(bool success)
	{
		Debug.Log ("Is User Logged In: " + success);
	}

	#region Play Services Methods

	public bool IsUserSignedIn()
	{
		return Social.localUser.authenticated;
	}

	public void SignInUser()
	{
		Social.localUser.Authenticate ((bool success) => {
			if (success) {
				ThirdPartyEventManager.DoFireSignInSuccessfullEvent ();
				Social.LoadAchievements (LoadAchievementsCallBack);
			}
		});
	}

	public void SignOutUser()
	{
		#if UNITY_ANDROID
		PlayGamesPlatform.Instance.SignOut();
		#elif  UNITY_IPHONE
		// No Signout from Game Center
		#endif
	}

	#endregion Play Services Methods

	#region Achievements

	/**
	 * Use It To Unlock An Achievement
	 */

	public void UnlockAchievement(string id)
	{
		Social.ReportProgress(id, 100, success => { });
	}

	/**
	 * Use It For Incremental Achievements
	 */

	public void IncrementAchievement(string id, int stepsToIncrement)
	{
		PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
	}

	/**
	 * To Show Achievements With Default UI
	 */

	public void ShowAchievementsUI()
	{
		Social.ShowAchievementsUI();
	}

	private void LoadAchievementsCallBack(IAchievement[] achievements)
	{
		Achievements = achievements;
	}

	#endregion Achievements

	#region Leaderboards

	public void AddScoreToLeaderboard(string leaderboardId, long score)
	{
		Social.ReportScore(score, leaderboardId, success => { });
	}

	/**
	 * To Show LeaderBoard With Default UI
	 */

	public void ShowLeaderboardsUI()
	{
		Social.ShowLeaderboardUI();
	}

	/**
	 * To Load Scores Of All Players
	 * Register LoadLeaderBoardScore Event To Get A CallBack
	*/

	public void LoadLeaderBoard (string leaderboardId)
	{
		Social.LoadScores (leaderboardId, LoadLeaderBoardCallBack);
	}
		
	private void LoadLeaderBoardCallBack(IScore[] scores)
	{
		ThirdPartyEventManager.DoFireLoadLeaderBoardScoreEvent (scores);
	}

	#endregion Leaderboards

	#region Cloud Save

	public void SaveDataOnCloud(string userState, string SAVE_NAME)
	{
		if (!IsUserSignedIn ())
			return;
		
		SAVE_VALUE = userState;
		isSaving = true;
		((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution (SAVE_NAME,
			DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
	}

	private void SaveGame(ISavedGameMetadata game)
	{
		byte[] dataToSave = Encoding.ASCII.GetBytes (SAVE_VALUE);
		SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
		((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave,
			OnSavedGameDataWritten);
	}

	private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
	{

	}

	#endregion Cloud Save

	#region Cloud Load

	/**
	 * To Load User State From Cloud
	 * Register OnLoadCloudData Event To Get A CallBack
	*/

	public void LoadCloudData(string SAVE_NAME)
	{
		if (!IsUserSignedIn ())
			return;
		
		isSaving = false;
		((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution (SAVE_NAME,
			DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
	}

	private void LoadGame(ISavedGameMetadata game)
	{
		((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
	}

	private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData)
	{
		if (status == SavedGameRequestStatus.Success)
		{
			string userState;

			if (savedData.Length == 0)
				userState = string.Empty;
			else
				userState = Encoding.ASCII.GetString(savedData);

			LoadGameData (userState);
		}
	}

	private void LoadGameData(string userState)
	{
		ThirdPartyEventManager.DoFireLoadCloudSavedDataEvent (userState);
	}

	#endregion Cloud Load

	#region Cloud Save/Load Common Methods

	private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
	{
		if (status == SavedGameRequestStatus.Success)
		{
			if (!isSaving)
				LoadGame(game);
			else
				SaveGame(game);
		}
	}

	private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData, ISavedGameMetadata unmerged, byte[] unmergedData)
	{
		if (originalData == null)
			resolver.ChooseMetadata(unmerged);
		else if (unmergedData == null)
			resolver.ChooseMetadata(original);
		else
		{
			string originalStr = Encoding.ASCII.GetString(originalData);
			string unmergedStr = Encoding.ASCII.GetString(unmergedData);

			int originalNum = int.Parse(originalStr);
			int unmergedNum = int.Parse(unmergedStr);

			if (originalNum > unmergedNum)
			{
				resolver.ChooseMetadata(original);
				return;
			}
			else if (unmergedNum > originalNum)
			{
				resolver.ChooseMetadata(unmerged);
				return;
			}
			resolver.ChooseMetadata(original);
		}
	}
		
	#endregion Cloud Save/Load Common Methods
}
