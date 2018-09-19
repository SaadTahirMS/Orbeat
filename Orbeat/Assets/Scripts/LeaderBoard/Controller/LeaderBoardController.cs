using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardController : Singleton<LeaderBoardController> {

	#region Variables And Properties

	private Dictionary<int,CharacterModel> opponentsDict;
	private List<CharacterModel> charactersToShowList;

	private List<CharacterModel> gameOverCharactersList;

	private int maxListSize = 5;
	private int opponentsToShowBelow = 2;

	private int playerLeaderBoardPosition;
	private int playerIndex;

	public int TotalOpponents
	{
		get
		{
			return opponentsDict.Count;
		}
	}

	public int PlayerIndex
	{
		get
		{
			return playerIndex;
		}
	}

	public int PlayerLeaderBoardPosition
	{
		get
		{
			return playerLeaderBoardPosition;
		}
	}

	#endregion Variables And Properties

	#region Initialization

	public void Initialize()
	{
		opponentsDict = new Dictionary<int, CharacterModel> ();
		charactersToShowList = new List<CharacterModel> ();
		gameOverCharactersList = new List<CharacterModel> ();

		IntializePlayerModel ();
	}

	private void IntializePlayerModel()
	{
		CharacterModel playerModel = new CharacterModel ();
		playerModel.barColor = UIIconsData.Instance.playerBarColor;
		playerModel.fontColor = UIIconsData.Instance.playerFontColor;
		playerModel.score = PlayerData.HighScore;
		playerModel.name = PlayerData.PlayerName;

		if (PlayerData.PlayerIconId == -1)
			playerModel.playerSprite = UIIconsData.Instance.playerDefaultIcon;
		else
			playerModel.playerSprite = UIIconsData.Instance.playerIcons [PlayerData.PlayerIconId];
		
		PlayerData.PlayerModel = playerModel;
	}

	#endregion Initialization

	#region Load Data From Meta

	public void AddLeaderBoardCharacter(CharacterModel model)
	{
		model.fontColor = UIIconsData.Instance.opponentFontColor;
		model.barColor = UIIconsData.Instance.opponentBarColor;
		model.playerSprite = UIIconsData.Instance.opponentIcons [model.id - 1];
		opponentsDict.Add (model.id, model);
	}

	#endregion Load Data From Meta

	#region LeaderBoard Position 

	public CharacterModel GetCharacterModel(int id)
	{
		return opponentsDict [id];
	}

	public List<CharacterModel> GetPlayersToShowList(int listSize, bool showTopPlayer = true, int opponentsBelow = 2)
	{
		opponentsToShowBelow = opponentsBelow;
		maxListSize = listSize;
		charactersToShowList.Clear ();
		playerLeaderBoardPosition = CalulatePlayerPosition ();
		int startIndex = GetPlayersRange ();
		if (startIndex == 0)
			startIndex = 1;
		int offset = 0;
		for (int i = 0; i < maxListSize; i++) {
			if (startIndex == playerLeaderBoardPosition) {
				charactersToShowList.Add (PlayerData.PlayerModel);
				playerIndex = maxListSize - i - 1;
				offset++;
			} else if (i == maxListSize - 1 && startIndex - offset != opponentsDict.Count && showTopPlayer) {
				charactersToShowList.Add (opponentsDict [opponentsDict.Count]);
			}else {
				charactersToShowList.Add (opponentsDict [startIndex - offset]);
			}
			startIndex++;
		}

		return charactersToShowList;
	}

	public int CalulatePlayerPosition()
	{
		int playerScore = PlayerData.HighScore;

		for (int i = 1; i <= opponentsDict.Count; i++) {
			if (playerScore <= opponentsDict [i].score)
				return i;
		}

		return opponentsDict.Count + 1;
	}

	public int CalulatePlayerPosition(bool currentPosition = false)
	{
		int playerScore = currentPosition ? PlayerData.CurrentScore : PlayerData.HighScore;

		for (int i = 1; i <= opponentsDict.Count; i++) {
			if (playerScore < opponentsDict [i].score)
				return i;
		}

		return opponentsDict.Count + 1;
	}

	private int GetPlayersRange()
	{
		if (playerLeaderBoardPosition > opponentsDict.Count - opponentsToShowBelow)
			return opponentsToShowBelow > 1 ? opponentsDict.Count - opponentsToShowBelow - 1 : opponentsDict.Count - opponentsToShowBelow;
		else if(playerLeaderBoardPosition == opponentsToShowBelow)
			return playerLeaderBoardPosition - 1;
		else if(playerLeaderBoardPosition > opponentsToShowBelow && playerLeaderBoardPosition <= opponentsDict.Count - opponentsToShowBelow)
			return playerLeaderBoardPosition - opponentsToShowBelow;
		else
			return playerLeaderBoardPosition;
	}

	public bool IsPlayerPositionChanged()
	{
		if (playerLeaderBoardPosition > opponentsDict.Count)
			return false;
		return PlayerData.CurrentScore > opponentsDict [playerLeaderBoardPosition].score;
	}

	#endregion  LeaderBoard Position 

	#region Game Over Characters List

	public List<CharacterModel> GetGameOverCharactersList()
	{
		playerLeaderBoardPosition = CalulatePlayerPosition ();
		gameOverCharactersList.Clear ();
		if (playerLeaderBoardPosition == opponentsDict.Count + 1) {
			gameOverCharactersList.Add (opponentsDict [playerLeaderBoardPosition - 1]);
			gameOverCharactersList.Add (PlayerData.PlayerModel);
		} else {
			gameOverCharactersList.Add (PlayerData.PlayerModel);
			gameOverCharactersList.Add (opponentsDict [playerLeaderBoardPosition]);
		}

		return gameOverCharactersList;
	}

	#endregion Game Over Characters List

	#region Dummy Data

	private void LoadDummyData()
	{
		for (int i = 1; i < 9; i++) {
			CharacterModel ch1 = new CharacterModel ();
			ch1.id = i;
			ch1.name = "shahrukh" + i;
			ch1.score = (10 * i);
			AddLeaderBoardCharacter (ch1);
		}
	}

	#endregion Dummy Data
}
