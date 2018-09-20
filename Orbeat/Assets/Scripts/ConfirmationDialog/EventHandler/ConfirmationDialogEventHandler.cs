using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationDialogEventHandler : MonoBehaviour {

	public void NoPressed()
	{
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		EventManager.DoFireNoPressedEvent ();
	}

	public void YesPressed()
	{
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		EventManager.DoFireYesPressedEvent ();
	}
}
