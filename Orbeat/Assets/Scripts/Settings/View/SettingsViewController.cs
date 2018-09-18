using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SettingsViewController : BaseController {

	#region Variables

	private SettingsRefs refs;

	private float onXValue = 49;
	private float offXValue = -49;

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
		refs.soundText.text = TextConstants.Sound.ToUpper ();
		refs.musicText.text = TextConstants.Music.ToUpper ();
		refs.notificationsText.text = TextConstants.Notifications.ToUpper ();
		refs.rateUsText.text = TextConstants.RateUs.ToUpper ();
		refs.restoreText.text = TextConstants.Restore.ToUpper ();

		refs.musicOnText.text = TextConstants.On.ToUpper ();
		refs.notificationsOnText.text = TextConstants.On.ToUpper ();
		refs.soundOnText.text = TextConstants.On.ToUpper ();

		refs.musicOffText.text = TextConstants.Off.ToUpper ();
		refs.notificationsOffText.text = TextConstants.Off.ToUpper ();
		refs.soundOffText.text = TextConstants.Off.ToUpper ();
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
		float xValue = soundState ? onXValue : offXValue;
		AnimationHandler.PlayXToggleAnimation (refs.soundToggleObj, xValue);
	}

	public void UpdateMusicView(bool musicState)
	{
		float xValue = musicState ? onXValue : offXValue;
		AnimationHandler.PlayXToggleAnimation (refs.musicToggleObj, xValue);
	}

	public void UpdateNotificationsView(bool notification)
	{
		float xValue = notification ? onXValue : offXValue;
		AnimationHandler.PlayXToggleAnimation (refs.notificationsToggleObj, xValue);
	}

	#endregion View Handling

	#region Tween Call Back

	private void SlideComplete()
	{
		SetState (false);
	}

	#endregion Tween Call Back
}
