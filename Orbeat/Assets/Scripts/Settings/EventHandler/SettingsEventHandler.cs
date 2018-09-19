using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsEventHandler : MonoBehaviour {

	public void ChangeNotificationsState()
	{
		EventManager.DoFireChangeNotificaitonsEvent ();
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
	}

	public void ChangeMusicState()
	{
		EventManager.DoFireChangeMusicEvent ();
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
	}

	public void ChangeSoundState()
	{
		EventManager.DoFireChangeSoundEvent ();
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
	}

	public void RateUs()
	{
		CloseSettings ();
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		EventManager.DoFireOpenViewEvent (Views.RateUs);
	}

	public void RestorePurchases()
	{
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		ThirdPartyController.Instance.iAppController.RestorePurchases ();
	}

	public void CloseSettings()
	{
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		EventManager.DoFireCloseViewEvent ();
	}
}
