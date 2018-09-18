using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsEventHandler : MonoBehaviour {

	public void ChangeNotificationsState()
	{
		EventManager.DoFireChangeNotificaitonsEvent ();
	}

	public void ChangeMusicState()
	{
		EventManager.DoFireChangeMusicEvent ();
	}

	public void ChangeSoundState()
	{
		EventManager.DoFireChangeSoundEvent ();
	}

	public void RateUs()
	{
		CloseSettings ();
		EventManager.DoFireOpenViewEvent (Views.RateUs);
	}

	public void RestorePurchases()
	{
		ThirdPartyController.Instance.iAppController.RestorePurchases ();
	}

	public void CloseSettings()
	{
		EventManager.DoFireCloseViewEvent ();
	}
}
