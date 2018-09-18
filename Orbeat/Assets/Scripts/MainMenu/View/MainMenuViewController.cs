using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuViewController : BaseController {

	#region Variables

	private MainMenuRefs refs;

	#endregion Variables

	#region Life Cycle Methods

	public void Open(GameObject obj, object viewModel = null)
	{
		if (refs == null) {
			refs = obj.GetComponent<MainMenuRefs> ();
		}

		SetState (true);
	}

	public void Close()
	{
		SetState (false);
	}

	#endregion Life Cycle Methods

	#region State Handling

	private void SetState(bool state)
	{
		refs.gameObject.SetActive (state);
		refs.warningText.gameObject.SetActive (false);
	}

	public void SetNoAdsButton(bool state)
	{
		refs.noAdsObj.SetActive (state);
	}

	#endregion State Handling

	#region Texts


	#endregion

	#region Warning

	public void ShowWarning(WarningType type)
	{
		Text warningText = refs.warningText;

		string warning = GetWarningText (type).ToUpper ();

		warningText.text = warning;

		AnimationHandler.PlayWarningAnimation(warningText);
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

		refs.playerImages [index].sprite = model.playerSprite;
	}

	#endregion LeaderBoard

}
