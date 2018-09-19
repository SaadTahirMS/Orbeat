using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : Singleton<GameStateController> {

	private ViewController viewController;
	private ProgressionBarController progressionBarController;

	public void Initialize(GameRefs refs){

		//PlayerPrefs.DeleteAll ();

		viewController = new ViewController (refs.viewRefs);
		progressionBarController = new ProgressionBarController (refs.progressionBarRefs);

		LoadPlayerState ();
		SoundController.Instance.Initialize ();
		LeaderBoardController.Instance.Initialize ();
		AnimationHandler.Initialize ();
		ThirdPartyController.Instance.Initialize ();
		LoadGameData ();

		ShowMainMenu ();
    }

	public void ShowMainMenu()
	{
		Time.timeScale = 1;
		viewController.CloseAllViews ();
		viewController.OpenView (Views.MainMenu);
	}

	public void StartGame()
	{
		GameplayContoller.Instance.Open ();
		progressionBarController.Open ();
		viewController.CloseAllViews ();
	}

	private void LoadPlayerState()
	{
		PlayerData.LoadState ();
//		PlayerData.HighScore = 250;
	}

	private void LoadGameData()
	{
		MetaLoader.LoadData ();
	}
}
