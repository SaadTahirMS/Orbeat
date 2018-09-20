using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUsEventHandler : MonoBehaviour {

	public void RateUsLater()
	{
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		EventManager.DoFireCloseViewEvent ();
	}

	public void RateUsNow()
	{
		PlayerData.IsRateUsClicked = true;
		RateUsLater ();
		ThirdPartyController.Instance.supportController.RateApplication ();
	}
}
