using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUsEventHandler : MonoBehaviour {

	public void RateUsLater()
	{
		EventManager.DoFireCloseViewEvent ();
	}

	public void RateUsNow()
	{
		RateUsLater ();
		ThirdPartyController.Instance.supportController.RateApplication ();
	}
}
