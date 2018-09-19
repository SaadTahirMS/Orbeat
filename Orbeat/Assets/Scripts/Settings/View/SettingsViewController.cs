using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SettingsViewController : BaseController {

	#region Variables

	private SettingsRefs refs;

	#endregion Variables

	#region Life Cycle Methods

	public void Open(GameObject obj, object viewModel = null)
	{
		if (refs == null) {
			refs = obj.GetComponent<SettingsRefs> ();
			SetTexts ();
		}
		AnimationHandler.SlideInFromLeft (refs.transform);
		SetState (true);
	}

	public void Close()
	{
		AnimationHandler.SlideOutToLeft (refs.transform, SlideComplete);
	}

	#endregion #region Life Cycle Methods

	#region Texts Initialization

	private void SetTexts()
	{
		refs.rateUsText.text = TextConstants.RateUs;
		refs.restoreText.text = TextConstants.Restore;
	}

	#endregion Texts Initialization

	#region State Handling

	private void SetState(bool state)
	{
		refs.gameObject.SetActive (state);
	}

	private void SetRestorePurchasesButton()
	{
		#if UNITY_ANDROID
		refs.restorePurchasesObj.SetActive (false);
		#elif Unity_IPHONE
		refs.restorePurchasesObj.SetActive (true);
		#endif
	}

	#endregion State Handling

	#region View Handling

	public void UpdateSoundView(bool soundState)
	{
		refs.soundToggleObj.SetActive (!soundState);
	}

	public void UpdateMusicView(bool musicState)
	{
		refs.musicToggleObj.SetActive (!musicState);
	}

	public void UpdateNotificationsView(bool notification)
	{
		refs.notificationsToggleObj.SetActive (!notification);
	}

	#endregion View Handling

	#region Tween Call Back

	private void SlideComplete()
	{
		SetState (false);
	}

	#endregion Tween Call Back
}
