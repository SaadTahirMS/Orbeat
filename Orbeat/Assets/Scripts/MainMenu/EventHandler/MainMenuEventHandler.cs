using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEventHandler : MonoBehaviour {

	public void Play()
	{
		GameplayContoller.Instance.Open ();
		EventManager.DoFireCloseAllViewsEvent ();
	}

	public void ShowSettings()
	{
		EventManager.DoFireOpenViewEvent (Views.Settings);
	}

	public void BuyNoAds()
	{
		ThirdPartyController.Instance.iAppController.PurchaseItem (ThirdPartyConstants.NonConsumableIApps [0]);
	}

	public void CharacterClicked(int index)
	{
		EventManager.DoFireCharacterClickedEvent (index);
	}
}
