using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverEventHandler : MonoBehaviour {

	public void ReviveClicked()
	{
        ThirdPartyController.Instance.admobController.ShowRewardBasedVideo ();
        //GameStateController.Instance.ReviveGame();
        //GameplayContoller.Instance.ReviveGame();
	}

	public void RestartClicked()
	{
		GameStateController.Instance.StartGame ();
	}

	public void HomeClicked()
	{
		GameStateController.Instance.ShowMainMenu ();
	}
}
