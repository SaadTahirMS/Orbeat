using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GamePauseViewController : BaseController {

	#region Variables

	private GamePauseRefs refs;

	#endregion Variables

	#region Life Cycle Methods

	public void Open(GameObject obj, object viewModel = null)
	{
		if (refs == null) {
			refs = obj.GetComponent<GamePauseRefs> ();
			SetTexts ();
		}
//		AnimationHandler.SlideInFromRight (refs.transform);
		SetState (true);
	}

	public void Close()
	{
//		AnimationHandler.SlideOutToRight (refs.transform, SlideComplete);
		SetState (false);
	}

	#endregion #region Life Cycle Methods

	#region State Handling

	private void SetState(bool state)
	{
		refs.gameObject.SetActive (state);
	}

	#endregion State Handling

	#region Texts Initialization

	private void SetTexts()
	{
		refs.header.text = TextConstants.GamePauseHeading;
		refs.resumeText.text = TextConstants.Resume;
		refs.homeText.text = TextConstants.Home;
	}

	#endregion Texts Initialization

	#region Tween Call Back

	private void SlideComplete()
	{
		SetState (false);
	}

	#endregion Tween Call Back
}
