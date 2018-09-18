using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuitController {

	#region Variables

	private ConfirmationDialogModel confirmationDialogModel;

	#endregion Variables

	public GameQuitController()
	{
		SetGameQuitDialog ();
	}

	private void SetGameQuitDialog()
	{
		confirmationDialogModel = new ConfirmationDialogModel ();

		confirmationDialogModel.noCallback = NoClicked;
		confirmationDialogModel.yesCallback = YesClicked;

		confirmationDialogModel.noText = TextConstants.No;
		confirmationDialogModel.yesText = TextConstants.Yes;
		confirmationDialogModel.headerText = TextConstants.GameQuitHeading;
		confirmationDialogModel.descriptionText = TextConstants.GameQuitDesc;

	}

	#region Game Quit Handling

	public void ShowGameQuit()
	{
		if (confirmationDialogModel == null) {
			SetGameQuitDialog ();
		}

		EventManager.DoFireOpenViewEvent (Views.ConfirmationDialog, confirmationDialogModel);
	}

	private void YesClicked()
	{
		Application.Quit ();
	}

	public void NoClicked()
	{
		EventManager.DoFireCloseViewEvent ();
	}

	#endregion Game Quit Handling
}
