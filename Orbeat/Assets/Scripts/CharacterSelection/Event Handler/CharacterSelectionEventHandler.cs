using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionEventHandler : MonoBehaviour {

	public void DoneClicked()
	{
		EventManager.DoFireDoneClickedEvent ();
	}

	public void NameChanged()
	{
		EventManager.DoFireNameChangedEvent ();
	}

	public void CharacterSelected(int characterNumber)
	{
		EventManager.DoFireCharacterChangedEvent (characterNumber);
	}
}
