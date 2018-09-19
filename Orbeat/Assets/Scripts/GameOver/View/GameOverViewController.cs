﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverViewController : MonoBehaviour {

	#region Variables

	private GameOverRefs refs;

	#endregion Variables

	#region Life Cycle Methods

	public void Open(GameObject obj, object viewModel = null)
	{
		if (refs == null) {
			refs = obj.GetComponent<GameOverRefs> ();
			SetTexts ();
		}
		AnimationHandler.SlideInFromRight (refs.transform);
		ShowCurrentScore ();
		SetGameOverButtons (false);
		SetReviveFillAmount (1);
		SetReviveButton (true);
		SetState (true);
		SetHighScoreBanner (false);
	}

	public void Close()
	{
		AnimationHandler.SlideOutToRight (refs.transform, SlideComplete);
	}

	#endregion #region Life Cycle Methods

	#region Texts Initialization

	private void SetTexts()
	{
		refs.reviveText.text = TextConstants.Revive;
		refs.headerText.text = TextConstants.GameOverHeader;
		refs.restartText.text = TextConstants.Restart;
		refs.highScore.text = TextConstants.HighScore;
	}

	#endregion Texts Initialization

	#region State Handling

	private void SetState(bool state)
	{
		refs.gameObject.SetActive (state);
	}

	#endregion State Handling

	#region View Handling

	private void ShowCurrentScore()
	{
		refs.currentScoreText.text = TextConstants.Score + PlayerData.CurrentScore.ToString ();
	}

	public void SetReviveButton(bool state)
	{
		refs.reviveObj.SetActive (state);
	}

	public void SetGameOverButtons(bool state)
	{
		refs.restartObj.SetActive (state);
		refs.homeObj.SetActive (state);
	}

	public void SetReviveFillAmount(float value)
	{
		refs.reviveFiller.fillAmount = value;
	}

	public void SetHighScoreBanner(bool state)
	{
		refs.highScoreBannerObj.SetActive (state);
	}

	#endregion View Handling

	#region Tween Call Back

	private void SlideComplete()
	{
		SetState (false);
	}

	#endregion Tween Call Back

	#region Warning

	public void ShowWarning(WarningType type)
	{
		string warning = GetWarningText (type);

		refs.warningText.text = warning;

		AnimationHandler.PlayWarningAnimation(refs.warningText,450);
	}

	private string GetWarningText(WarningType type)
	{
		switch (type) {
		case WarningType.NoVideoAvailable:
			return TextConstants.noVideoWarningText;
		case WarningType.InAppPurchaseFailed:
			return TextConstants.iAppFailedWarning;
		case WarningType.NotEnoughCash:
			return TextConstants.NotEnoughCash;
		default:
			return TextConstants.NotEnoughCash;
		}
	}

	#endregion Warning

	#region LeaderBoard

	public void SetLeaderBoardStrip(int index, CharacterModel model)
	{
		refs.playerName [index].text = model.name;
		refs.score [index].text = model.score.ToString ();

		refs.playerName [index].color = model.fontColor;
		refs.score [index].color = model.fontColor;

		refs.stripeImages [index].color = model.barColor;

		refs.playerIcons [index].sprite = model.playerSprite;
	}

	#endregion LeaderBoard
}
