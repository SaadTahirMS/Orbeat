using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionBarController {

	#region Variables

	private ProgressionBarRefs refs;

	private int scoreToBeat;

	private int opponentToBeat;

	private int totalOpponents;

	private bool isOpponentLeft;

	private WaitForSeconds popUpCrWait;
	private float popUpCrDelay = 1;

	#endregion Variables

	#region Initialization

	public ProgressionBarController(ProgressionBarRefs progressionRefs)
	{
		popUpCrWait = new WaitForSeconds (popUpCrDelay);

		isOpponentLeft = true;
		refs = progressionRefs;
		InitializeEvents ();
	}

	private void InitializeEvents()
	{
		EventManager.OnScoreUpdated += UpdateFillBar;
		EventManager.OnUpdateFillBarColor += UpdateFillBarColor;
	}

	#endregion Initialization

	#region Start Game Settings

	public void Open()
	{
		SetState (true);
		SetPlayer ();
		totalOpponents = LeaderBoardController.Instance.TotalOpponents;
		UpdateOpponent (false);
	}

	private void SetPlayer()
	{
		if (PlayerData.PlayerIconId == -1)
			refs.playerImage.sprite = UIIconsData.Instance.playerDefaultIcon;
		else
			refs.playerImage.sprite = UIIconsData.Instance.playerIcons [PlayerData.PlayerIconId];
	}

	#endregion Start Game Settings

	#region State Handling

	private void SetState(bool state)
	{
		refs.gameObject.SetActive (state);
	}

	#endregion

	#region Progression Handling

	private void UpdateOpponent(bool notFirstOpponent)
	{
		opponentToBeat = LeaderBoardController.Instance.CalulatePlayerPosition (notFirstOpponent);
		if (opponentToBeat > totalOpponents) {
			isOpponentLeft = false;
			opponentToBeat -= 1;
		}

		refs.opponentImage.sprite = UIIconsData.Instance.opponentIcons [opponentToBeat - 1];
		GameStateController.Instance.StartCoroutine (PopUpOpponentCr ());

		if (isOpponentLeft) {
			scoreToBeat = LeaderBoardController.Instance.GetCharacterModel (opponentToBeat).score;
			UpdateFillBar ();
		} else {
			refs.fillerImage.fillAmount = 1;
		}

	}

	private void UpdateFillBar()
	{
		if (isOpponentLeft) {
			float fillAmount = (float)PlayerData.CurrentScore / scoreToBeat;
			refs.fillerImage.fillAmount = fillAmount;

			if (fillAmount > 0.99f) {
				SoundController.Instance.PlayDialogSound (SFX.LevelUp);
				UpdateOpponent (true);
			}
		}
	}

	private void UpdateFillBarColor(Color32 barColor, Color32 fillerColor)
	{
		refs.barImage.color = barColor;
		refs.fillerImage.color = fillerColor;
	}

	private IEnumerator PopUpOpponentCr()
	{
		yield return popUpCrWait;
		AnimationHandler.PlaySelectAnimation (refs.opponentObj, 0.2f);
	}

	#endregion Progression Handling
}
