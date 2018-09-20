using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionController : BaseController {

	#region Variables

	private CharacterSelectionViewController characterSelectionViewController;

	private WaitForSeconds randomSelectionCr;

	private float randomSelectionCrDelay = 0.1f;

	#endregion Variables

	#region Life Cycle Methods

	public CharacterSelectionController()
	{
		randomSelectionCr = new WaitForSeconds (randomSelectionCrDelay);
		LoadState ();
		characterSelectionViewController = new CharacterSelectionViewController ();
	}

	public void Open(GameObject obj, object viewModel = null)
	{
		characterSelectionViewController.Open (obj);
		InitializeEvents ();
		SetInitialCharacter ();
	}

	public void Close()
	{
		UnInitializeEvents ();
		characterSelectionViewController.Close ();
	}

	private void InitializeEvents()
	{
		EventManager.OnDoneClicked += DoneClicked;
		EventManager.OnNameChanged += NameChanged;
		EventManager.OnCharacterChanged += CharacterChanged;
	}

	private void UnInitializeEvents()
	{
		EventManager.OnDoneClicked -= DoneClicked;
		EventManager.OnNameChanged -= NameChanged;
		EventManager.OnCharacterChanged -= CharacterChanged;
	}

	#endregion Life Cycle Methods

	#region Save/Load State

	public void LoadState()
	{
	}

	public void SaveState()
	{
	}

	#endregion Save/Load State

	#region Event Call Backs

	private void DoneClicked()
	{
		characterSelectionViewController.NameChanged ();
		EventManager.DoFirePlayerProfileChangedEvent ();
		EventManager.DoFireCloseViewEvent ();
	}

	private void NameChanged()
	{
		characterSelectionViewController.SetChangeField (TextConstants.Change);
	}

	private void CharacterChanged(int characterNumber)
	{
		characterSelectionViewController.CharacterChanged (characterNumber);
		PlayerData.PlayerIconId = characterNumber;
	}

	#endregion Event Call Backs

	#region Initial Character Selection

	private void SetInitialCharacter()
	{
		if (!PlayerData.IsFirstSession) {
			characterSelectionViewController.SetHighLighter (PlayerData.PlayerIconId,true);
		} else {
			GameStateController.Instance.StartCoroutine (SelectRandomCharacter ());
			PlayerData.IsFirstSession = false;
		}
		characterSelectionViewController.SetChangeField (TextConstants.Ok);
	}

	IEnumerator SelectRandomCharacter()
	{
		int randomCharacterIndex = 0;
		int numberOfIterations = 7;
		yield return randomSelectionCr;
		for (int i = 0; i < numberOfIterations; i++) {
			SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
			randomCharacterIndex = Random.Range (0, characterSelectionViewController.GetTotalIconsCount ());
			characterSelectionViewController.SetHighLighter (randomCharacterIndex, true);
			yield return randomSelectionCr;
			characterSelectionViewController.SetHighLighter (randomCharacterIndex, false);
		}
	
		characterSelectionViewController.RandomCharacterSelected (randomCharacterIndex);
		PlayerData.PlayerIconId = randomCharacterIndex;
	}

	#endregion Initial Character Selection
}
