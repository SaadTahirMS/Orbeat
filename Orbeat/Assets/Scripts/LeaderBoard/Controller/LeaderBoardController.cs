using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardController : Singleton<LeaderBoardController> {

	#region Variables And Properties

	private Dictionary<int,CharacterModel> opponentsDict;
	private List<CharacterModel> charactersToShowList;

	private int maxListSize = 5;
	private int opponentsToShowBelow = 2;

	private int playerCurrentPosition;

	private int playerIndex;

	public int PlayerIndex
	{
		get
		{
			return playerIndex;
		}
	}

	#endregion Variables And Properties

	#region Initialization

	public void Initialize()
	{
		opponentsDict = new Dictionary<int, CharacterModel> ();
		charactersToShowList = new List<CharacterModel> ();

		IntializePlayerModel ();
	}

	private void IntializePlayerModel()
	{
		CharacterModel playerModel = new CharacterModel ();
		playerModel.barColor = UIIconsData.Instance.playerBarColor;
		playerModel.fontColor = UIIconsData.Instance.playerFontColor;
		playerModel.playerSprite = UIIconsData.Instance.playerIcons [PlayerData.PlayerIconId];
		playerModel.score = PlayerData.HighScore;
		playerModel.name = PlayerData.PlayerName;

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

	public int GetPlayerCurrentPosition()
	{
		return playerCurrentPosition;
	}

	public List<CharacterModel> GetPlayersToShowList()
	{
		charactersToShowList.Clear ();
		playerCurrentPosition = CalulatePlayerCurrentPosition ();
		int startIndex = GetPlayersRange ();
		int offset = 0;
		for (int i = 0; i < maxListSize; i++) {
			if (startIndex == playerCurrentPosition) {
				charactersToShowList.Add (PlayerData.PlayerModel);
				playerIndex = maxListSize - i - 1;
				offset++;
			} else {
				charactersToShowList.Add (opponentsDict [startIndex - offset]);
			}
			startIndex++;
		}

		return charactersToShowList;
	}

	private int CalulatePlayerCurrentPosition()
	{
		int playerScore = PlayerData.HighScore;

		for (int i = 1; i <= opponentsDict.Count; i++) {
			if (playerScore <= opponentsDict [i].score)
				return i;
		}

		return opponentsDict.Count + 1;
	}

	private int GetPlayersRange()
	{
		if (playerCurrentPosition > opponentsDict.Count - opponentsToShowBelow)
			return opponentsDict.Count - opponentsToShowBelow - 1;
		else if(playerCurrentPosition == opponentsToShowBelow)
			return playerCurrentPosition - 1;
		else if(playerCurrentPosition > opponentsToShowBelow && playerCurrentPosition <= opponentsDict.Count - opponentsToShowBelow)
			return playerCurrentPosition - opponentsToShowBelow;
		else
			return playerCurrentPosition;
	}

	#endregion  LeaderBoard Position 

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
