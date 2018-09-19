using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseEventHandler : MonoBehaviour {

	public void ResumeGame()
	{
		GameplayContoller.Instance.Open ();
		EventManager.DoFireCloseAllViewsEvent ();
	}

	public void BackToHome()
	{
		GameStateController.Instance.ShowMainMenu ();
	}
}
