using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEventHandler : MonoBehaviour {

	public void Play()
	{
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		GameStateController.Instance.StartGame ();
	}

	public void ShowSettings()
	{
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		EventManager.DoFireOpenViewEvent (Views.Settings);
	}

	public void BuyNoAds()
	{
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		ThirdPartyController.Instance.iAppController.PurchaseItem (ThirdPartyConstants.NonConsumableIApps [0]);
	}

	public void CharacterClicked(int index)
	{
		EventManager.DoFireCharacterClickedEvent (index);
	}
}
