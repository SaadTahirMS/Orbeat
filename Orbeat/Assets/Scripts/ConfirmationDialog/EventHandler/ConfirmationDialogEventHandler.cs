using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationDialogEventHandler : MonoBehaviour {

	public void NoPressed()
	{
		EventManager.DoFireNoPressedEvent ();
	}

	public void YesPressed()
	{
		EventManager.DoFireYesPressedEvent ();
	}
}
