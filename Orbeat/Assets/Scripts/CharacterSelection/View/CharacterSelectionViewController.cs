using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionViewController : BaseController {

	#region Variables

	private CharacterSelectionRefs refs;

	private float onXValue = 49;
	private float offXValue = -49;

	#endregion Variables

	#region Life Cycle Methods

	public void Open(GameObject obj, object viewModel = null)
	{
		if (refs == null) {
			refs = obj.GetComponent<CharacterSelectionRefs> ();
			SetTexts ();
		}
		AnimationHandler.SlideInFromLeft (refs.transform);
		SetState (true);
		SetPlayerNameText (PlayerData.PlayerName);
	}

	public void Close()
	{
		AnimationHandler.SlideOutToLeft (refs.transform, SlideComplete);
	}

	#endregion #region Life Cycle Methods

	#region Player Handling

	private void SetTexts()
	{
		refs.header.text = TextConstants.CharacterSelectionHeading;
	}

	public void SetChangeField(string text)
	{
		refs.changeTextField.text = text;
	}

	public void SetHighLighter(int currentSelectedCharacter, bool state)
	{
		refs.highLighters [currentSelectedCharacter].SetActive (state);
	}

	public void SetPlayerNameText(string name)
	{
		refs.placeHolderText.text = name;
	}

	public void NameChanged()
	{
		if (refs.iField.text == "")
			PlayerData.PlayerName = TextConstants.You;
		else if (refs.iField.text != "")
			PlayerData.PlayerName = refs.iField.text;
	}

	public void CharacterChanged(int characterNumber)
	{
		if (characterNumber != PlayerData.PlayerIconId)
			refs.changeTextField.text = TextConstants.Change;

		PlaySelectionAnim (characterNumber);
		SetHighLighter (PlayerData.PlayerIconId,false);
		SetHighLighter (characterNumber, true);
	}

	public int GetTotalIconsCount()
	{
		return refs.characters.Length;
	}

	public void PlaySelectionAnim(int characterNumber)
	{
		AnimationHandler.PlaySelectAnimation (refs.characters [characterNumber]);
	}

	public void RandomCharacterSelected(int characterNumber)
	{
		PlaySelectionAnim (characterNumber);
		SetHighLighter (characterNumber, true);
	}

	#endregion Player Handling

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
