using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverEventHandler : MonoBehaviour {

	public void ReviveClicked()
	{
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		ThirdPartyController.Instance.admobController.ShowRewardBasedVideo ();
	}

	public void RestartClicked()
	{
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		GameStateController.Instance.StartGame ();
	}

	public void HomeClicked()
	{
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		GameStateController.Instance.ShowMainMenu ();
	}
}
