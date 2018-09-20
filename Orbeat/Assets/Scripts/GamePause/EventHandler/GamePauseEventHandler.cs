using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseEventHandler : MonoBehaviour {

	public void ResumeGame()
	{
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		EventManager.DoFireCloseViewEvent ();
	}

	public void BackToHome()
	{
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		GameStateController.Instance.ShowMainMenu ();
	}
}
