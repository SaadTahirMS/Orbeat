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
	private float decreaseFillAmountCrDelay = 0.025f;
	private float showRestartButtonCrDelay = 2f;

	private bool isReviveClicked;

	private bool isFirstSession;

	private int playerPosition;

	#endregion Variables

	#region Life Cycle Methods

	public GameOverController()
	{
		isFirstSession = true;
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
		isReviveClicked = true;
		GameStateController.Instance.StopCoroutine (DecreaseReviveFillerCR ());
		GameStateController.Instance.StopCoroutine (ShowRestartButtonCr ());
		EventManager.DoFireCloseViewEvent ();
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
		isReviveClicked = false;
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
			playerPosition = LeaderBoardController.Instance.PlayerIndex;
			for (int i = 0; i < charactersList.Count; i++) {
				gameOverViewController.SetLeaderBoardStrip (i, charactersList [charactersList.Count - i - 1]);
			}

			if (PlayerData.IsHighScoreChanged ()) {
				PlayerData.UpdateHighScore ();
				gameOverViewController.SetHighScoreBanner (true);
			}

			if (LeaderBoardController.Instance.IsPlayerPositionChanged ())
				GameStateController.Instance.StartCoroutine (UpdateLeaderBoardCR ());
			else
				gameOverViewController.SetLeaderBoardStrip (playerPosition, PlayerData.PlayerModel);
		}
	}

	private IEnumerator UpdateLeaderBoardCR()
	{
		isFirstSession = true;
		yield return new WaitForSeconds (3f);
		int offset = playerPosition == 2 ? -1 : 1;
		gameOverViewController.SetLeaderBoardStrip (playerPosition, charactersList [playerPosition + offset]);
		gameOverViewController.SetLeaderBoardStrip (playerPosition - 1, PlayerData.PlayerModel);
	}

	#endregion Leader Board Handling

}
