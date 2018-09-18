using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationDialogController : BaseController {

	#region Variables

	private ConfirmationDialogViewController confirmationDialogViewController;
	private ConfirmationDialogModel confirmationDialogModel;

	#endregion Variables

	#region Life Cycle Methods

	public ConfirmationDialogController()
	{
		confirmationDialogViewController = new ConfirmationDialogViewController ();
	}

	public void Open(GameObject obj, object viewModel = null)
	{
		confirmationDialogModel = (ConfirmationDialogModel)viewModel;
		InitializeEvents ();
		confirmationDialogViewController.Open (obj, confirmationDialogModel);
	}

	public void Close()
	{
		UnInitializeEvents ();
		confirmationDialogViewController.Close ();
	}

	private void InitializeEvents()
	{
		EventManager.OnNoPressed += Button1Pressed;
		EventManager.OnYesPressed += Button2Pressed;
	}

	private void UnInitializeEvents()
	{
		EventManager.OnNoPressed -= Button1Pressed;
		EventManager.OnYesPressed -= Button2Pressed;
	}

	#endregion Life Cycle Methods

	#region Event Call Back

	private void Button1Pressed()
	{
		confirmationDialogModel.noCallback ();
	}

	private void Button2Pressed()
	{
		confirmationDialogModel.yesCallback ();
	}

	#endregion Event Call Back
}
