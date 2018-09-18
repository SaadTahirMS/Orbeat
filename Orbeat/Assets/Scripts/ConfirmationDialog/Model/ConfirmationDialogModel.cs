using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConfirmationDialogModel {

	public string headerText;
	public string descriptionText;

	public string noText;
	public string yesText;

	public Action noCallback;
	public Action yesCallback;
}
