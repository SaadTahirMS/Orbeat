using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUsViewController : BaseController {

	#region Variables

	private RateUsRefs refs;

	private WaitForSeconds noButtonDelay;

	#endregion Variables

	#region Life Cycle Methods

	public void Open(GameObject obj, object viewModel = null)
	{
		if (refs == null) {
			refs = obj.GetComponent<RateUsRefs> ();
			noButtonDelay = new WaitForSeconds (5f);
			SetTexts ();
		}
		AnimationHandler.SlideDown (refs.dialogContainer);
		SetState (true);
	}

	public void Close()
	{
		AnimationHandler.SlideBackUp (refs.dialogContainer, SlideComplete);
	}

	#endregion #region Life Cycle Methods

	#region Texts Initialization

	private void SetTexts()
	{
		refs.laterText.text = TextConstants.NoThanks;
		refs.rateUsText.text = TextConstants.RateUs;
		refs.rateUsDesc.text = TextConstants.RateUsDesc;
		refs.rateUsHeading.text = TextConstants.RateUsHeading;
	}

	#endregion Texts Initialization

	#region State Handling

	private void SetState(bool state)
	{
		refs.gameObject.SetActive (state);
	}

	#endregion State Handling

	#region Tween Call Back

	private void SlideComplete()
	{
		SetState (false);
	}

	#endregion Tween Call Back
}
