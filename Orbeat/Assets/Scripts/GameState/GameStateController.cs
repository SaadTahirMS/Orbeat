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

		EventManager.DoFireOpenViewEvent (Views.MainMenu);
		if (PlayerData.IsFirstSession) {
			EventManager.DoFireOpenViewEvent (Views.CharacterSelection);
		}
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
