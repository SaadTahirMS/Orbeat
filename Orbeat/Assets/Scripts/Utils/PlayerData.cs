using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData {

	#region Properties And Variables

	private static bool isFirstSession;
	private static bool isNoAdsPurchased;
	private static bool isNotificationsOn;
	private static bool isScoreChanged = true;
	private static int highScore;
	private static int currentScore;

	private static string playerName;
	private static int playerIconId;

	private static CharacterModel playerModel;

	public static bool IsFirstSession
	{
		get
		{ 
			return isFirstSession;
		}
		set
		{
			isFirstSession = value;
			SaveState ();
		}
	}

	public static bool IsNoAdsPurchased
	{
		get
		{ 
			return isNoAdsPurchased;
		}
	}

	public static bool IsNotificationsOn
	{
		get
		{ 
			return isNotificationsOn;
		}

		set
		{
			isNotificationsOn = value;
			SaveState ();
		}
	}

	public static CharacterModel PlayerModel
	{
		get
		{ 
			return playerModel;
		}

		set
		{
			playerModel = value;
		}
	}

	public static int HighScore
	{
		get
		{
			return highScore;
		}
		set
		{
			highScore = value;
			SaveState ();
		}
	}

	public static int CurrentScore
	{
		get
		{
			return currentScore;
		}
		set
		{
			currentScore = value;
		}
	}

	public static bool IsScoreChanged
	{
		get
		{
			return isScoreChanged;
		}
		set
		{
			isScoreChanged = value;
		}
	}

	public static string PlayerName
	{
		get
		{
			return playerName;
		}
		set
		{
			playerName = value;
			playerModel.name = playerName;
			SaveState ();
		}
	}

	public static int PlayerIconId
	{
		get
		{
			return playerIconId;
		}
		set
		{
			playerIconId = value;
			playerModel.playerSprite = UIIconsData.Instance.playerIcons [playerIconId];
			SaveState ();
		}
	}

	#endregion Properties And Variables

	#region Player Data Update Methods

	public static void NoAdsPurchased()
	{
		isNoAdsPurchased = true;

		SaveState ();
	}

	public static bool IsHighScoreChanged()
	{
		if (currentScore > highScore) {
			HighScore = currentScore;
			playerModel.score = highScore;
			isScoreChanged = true;
			return true;
		}
		return false;
	}

	#endregion

	#region Load/Save State

	public static void LoadState()
	{
		playerName = DatabaseManager.GetString (Constants.playerName, "Enter Your Name...");
		playerIconId = DatabaseManager.GetInt (Constants.playerIconId, 0);
		highScore = DatabaseManager.GetInt (Constants.highScore);
		isNoAdsPurchased = DatabaseManager.GetBool (Constants.noAdsPurchased);
		isNotificationsOn = DatabaseManager.GetBool (Constants.isNotificationsOn, true);
		isFirstSession = DatabaseManager.GetBool (Constants.isFirstSession, true);
	}

	public static void SaveState()
	{
		DatabaseManager.SetString (Constants.playerName, playerName);
		DatabaseManager.SetInt (Constants.playerIconId, playerIconId);
		DatabaseManager.SetInt (Constants.highScore, highScore);
		DatabaseManager.SetBool (Constants.noAdsPurchased, isNoAdsPurchased);
		DatabaseManager.SetBool (Constants.isNotificationsOn, isNotificationsOn);
		DatabaseManager.SetBool (Constants.isFirstSession, isFirstSession);
	}

	#endregion Load/Save State

	#region Cloud Load/Save

	public static Dictionary<string,string> GetCloudState()
	{
		Dictionary<string, string> state = new Dictionary<string, string>();
		return state;
	}

	#endregion Cloud Load/Save
}
