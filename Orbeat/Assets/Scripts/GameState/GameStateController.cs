using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : Singleton<GameStateController> {

	private ViewController viewController;

	public void Initialize(ViewRefs refs){

		viewController = new ViewController (refs);

		LoadPlayerState ();

		LeaderBoardController.Instance.Initialize ();
		AnimationHandler.Initialize ();
		ThirdPartyController.Instance.Initialize ();

		LoadGameData ();

		ShowMainMenu ();

		if (PlayerData.IsFirstSession) {
			viewController.OpenView (Views.CharacterSelection);
		}
    }

	public void ShowMainMenu()
	{
		Time.timeScale = 1;
		viewController.CloseAllViews ();
		viewController.OpenView (Views.MainMenu);
	}

	private void LoadPlayerState()
	{
		PlayerData.LoadState ();
	}

	private void LoadGameData()
	{
		MetaLoader.LoadData ();
	}
}
