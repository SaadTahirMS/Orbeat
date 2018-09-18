using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationDialogViewController {

	#region Variables

	private ConfirmationDialogRefs refs;
	private ConfirmationDialogModel confirmationDialogModel;

	#endregion Variables

	#region Life Cycle Methods

	public void Open(GameObject obj, object viewModel = null)
	{
		if (refs == null) {
			refs = obj.GetComponent<ConfirmationDialogRefs> ();
		}

		confirmationDialogModel = (ConfirmationDialogModel)viewModel;

		SetTexts ();

		AnimationHandler.PopIn (refs.dialogContainer);
		SetState (true);
	}

	public void Close()
	{
		AnimationHandler.PopOut (refs.dialogContainer, TweenComplete);
	}

	#endregion #region Life Cycle Methods

	#region Texts Initialization

	private void SetTexts()
	{
		refs.headerText.text = confirmationDialogModel.headerText;
		refs.descText.text = confirmationDialogModel.descriptionText;
		refs.button1Text.text = confirmationDialogModel.noText;
		refs.button2Text.text = confirmationDialogModel.yesText;
	}

	#endregion Texts Initialization

	#region State Handling

	private void SetState(bool state)
	{
		refs.gameObject.SetActive (state);
	}

	#endregion State Handling

	#region Tween Call Back

	private void TweenComplete()
	{
		SetState (false);
	}

	#endregion Tween Call Back
}
