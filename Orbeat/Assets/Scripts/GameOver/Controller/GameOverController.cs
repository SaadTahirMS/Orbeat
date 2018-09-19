using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : BaseController {

	#region Variables

	private GameOverViewController gameOverViewController;

	private List<CharacterModel> charactersList;

	private float fillAmount;
	private float decreaseFactor = 0.25f;

	private WaitForSeconds decreaseFillAmountWait;
	private WaitForSeconds showRestartButtonWait;
	private WaitForSeconds popUpCrWait;


	private float decreaseFillAmountCrDelay = 0.025f;
	private float showRestartButtonCrDelay = 2f;
	private float popUpCrDelay = 1;

	private bool isFirstSession;

	private int playerIndex;
	private int playerLeaderBoardPostion;

	#endregion Variables

	#region Life Cycle Methods

	public GameOverController()
	{
		isFirstSession = true;
		popUpCrWait = new WaitForSeconds (popUpCrDelay);
		decreaseFillAmountWait = new WaitForSeconds (decreaseFillAmountCrDelay);
		showRestartButtonWait = new WaitForSeconds (showRestartButtonCrDelay);
		gameOverViewController = new GameOverViewController ();
	}

	public void Open(GameObject obj, object viewModel = null)
	{
		InitializeEvents ();
		gameOverViewController.Open (obj);
		HandleReviveFiller ();
		ShowLeaderBoard ();
	}

	public void Close()
	{
		UnInitializeEvents ();
		gameOverViewController.Close ();
	}

	private void InitializeEvents()
	{
		ThirdPartyEventManager.OnRewardBaseVideoClosed += ReviveGame;
		ThirdPartyEventManager.OnRewardBaseVideoNotLoaded += OnRewardBaseVideoNotLoaded;
	}

	private void UnInitializeEvents()
	{
		ThirdPartyEventManager.OnRewardBaseVideoClosed -= ReviveGame;
		ThirdPartyEventManager.OnRewardBaseVideoNotLoaded -= OnRewardBaseVideoNotLoaded;
	}

	#endregion Life Cycle Methods

	#region Event Call Back

	private void ReviveGame()
	{
		GameStateController.Instance.StopCoroutine (DecreaseReviveFillerCR ());
		GameStateController.Instance.StopCoroutine (ShowRestartButtonCr ());
		EventManager.DoFireCloseViewEvent ();
        GameplayContoller.Instance.ReviveGame();
	}

	private void OnRewardBaseVideoNotLoaded()
	{
		gameOverViewController.ShowWarning (WarningType.NoVideoAvailable);
	}


	#endregion Event Call Back

	#region Filler Handling

	private void HandleReviveFiller()
	{
		GameStateController.Instance.StartCoroutine (DecreaseReviveFillerCR ());
		GameStateController.Instance.StartCoroutine (ShowRestartButtonCr ());
	}

	private IEnumerator DecreaseReviveFillerCR()
	{
		fillAmount = 1;
		while (fillAmount > 0) {
			fillAmount -= decreaseFactor * Time.deltaTime;
			gameOverViewController.SetReviveFillAmount (fillAmount);
			yield return decreaseFillAmountWait;
		}
		gameOverViewController.SetReviveButton (false);
	}

	private IEnumerator ShowRestartButtonCr()
	{
		yield return showRestartButtonWait;
		gameOverViewController.SetGameOverButtons (true);
	}

	#endregion Filler Handling

	#region Leader Board Handling

	private void ShowLeaderBoard()
	{
		if (PlayerData.IsHighScoreChanged () || isFirstSession) {
			isFirstSession = false;
			charactersList = LeaderBoardController.Instance.GetPlayersToShowList (3, false, 1);
			playerIndex = LeaderBoardController.Instance.PlayerIndex;
			playerLeaderBoardPostion = LeaderBoardController.Instance.PlayerLeaderBoardPosition;
			for (int i = 0; i < charactersList.Count; i++) {
				gameOverViewController.SetLeaderBoardStrip (i, charactersList [charactersList.Count - i - 1]);
			}

			if (PlayerData.IsHighScoreChanged ()) {
				PlayerData.UpdateHighScore ();
				gameOverViewController.SetHighScoreBanner (true);
			}

			GameStateController.Instance.StartCoroutine (PopPlayerStripe ());

			if (LeaderBoardController.Instance.IsPlayerPositionChanged ())
				GameStateController.Instance.StartCoroutine (UpdateLeaderBoardCR ());
			else
				gameOverViewController.SetLeaderBoardStrip (playerIndex, PlayerData.PlayerModel);
		}
	}

	private IEnumerator UpdateLeaderBoardCR()
	{
		isFirstSession = true;
		yield return showRestartButtonWait;
		int offset = playerIndex == 2 ? -1 : 1;
		int playerFactor = -1;
		int playerNewLBPosition = LeaderBoardController.Instance.CalulatePlayerPosition ();
		int newPlayerIndex = playerIndex - 1;

		if (playerNewLBPosition - playerLeaderBoardPostion >= 2 && playerLeaderBoardPostion == 1) {
			offset = 0;
			playerFactor -= 1;
			gameOverViewController.SetLeaderBoardStrip (playerIndex, charactersList [playerIndex + offset - 1]);
			gameOverViewController.SetLeaderBoardStrip (playerIndex -1, charactersList [playerIndex + offset]);
			newPlayerIndex = 0;
		}
		else
			gameOverViewController.SetLeaderBoardStrip (playerIndex, charactersList [playerIndex + offset]);

		gameOverViewController.SetLeaderBoardStrip (playerIndex + playerFactor, PlayerData.PlayerModel);
		GameStateController.Instance.StartCoroutine (PopPlayerStripe ());
		playerIndex = newPlayerIndex;
	}

	private IEnumerator PopPlayerStripe()
	{
		yield return popUpCrWait;
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		gameOverViewController.PlayPlayerPopAnim (playerIndex);
	}

	#endregion Leader Board Handling

}
