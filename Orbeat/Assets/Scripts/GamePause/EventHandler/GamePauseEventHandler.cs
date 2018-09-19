using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseEventHandler : MonoBehaviour {

	public void ResumeGame()
	{
		EventManager.DoFireCloseViewEvent ();
	}

	public void BackToHome()
	{
		GameStateController.Instance.ShowMainMenu ();
	}
}
