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

	#endregion Variables

	#region Initialization

	public ProgressionBarController(ProgressionBarRefs progressionRefs)
	{
		isOpponentLeft = true;
		refs = progressionRefs;
		InitializeEvents ();
	}

	private void InitializeEvents()
	{
		EventManager.OnScoreUpdated += UpdateFillBar;
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
		}

		if (isOpponentLeft) {
			scoreToBeat = LeaderBoardController.Instance.GetCharacterModel (opponentToBeat).score;
			refs.opponentImage.sprite = UIIconsData.Instance.opponentIcons [opponentToBeat - 1];

			UpdateFillBar ();
		} else {
			refs.fillerImage.fillAmount = 1;
		}

	}

	private void UpdateFillBar()
	{
		if (isOpponentLeft) 
        {
			float fillAmount = (float)PlayerData.CurrentScore / scoreToBeat;

			refs.fillerImage.fillAmount = fillAmount;

			if (fillAmount >= 1f) {
				UpdateOpponent (true);
			}
		}
	}

	#endregion Progression Handling
}
