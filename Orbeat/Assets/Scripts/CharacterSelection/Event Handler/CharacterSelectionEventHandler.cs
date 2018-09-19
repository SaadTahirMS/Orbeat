using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionEventHandler : MonoBehaviour {

	public void DoneClicked()
	{
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		EventManager.DoFireDoneClickedEvent ();
	}

	public void NameChanged()
	{
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		EventManager.DoFireNameChangedEvent ();
	}

	public void CharacterSelected(int characterNumber)
	{
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		EventManager.DoFireCharacterChangedEvent (characterNumber);
	}
}
