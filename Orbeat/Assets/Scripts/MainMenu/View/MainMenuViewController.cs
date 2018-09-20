using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuViewController : BaseController {

	#region Variables

	private MainMenuRefs refs;

	float posX;

	#endregion Variables

	#region Life Cycle Methods

	public void Open(GameObject obj, object viewModel = null)
	{
		if (refs == null) {
			refs = obj.GetComponent<MainMenuRefs> ();
			posX = refs.playerObj [0].transform.localPosition.x;
		}

		StartAnimation ();
		SetState (true);
	}

	public void Close()
	{
		SetState (false);
		StopAnimation ();
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

		string warning = GetWarningText (type);

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

	public void PlayPlayerPopAnim(int index)
	{
		AnimationHandler.PlaySelectAnimation (refs.playerObj [index].transform);
	}

	#endregion LeaderBoard

	public void StartAnimation()
	{
		
		refs.circle1.DORotate (Vector3.forward * 360, 2,RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops (-1,LoopType.Incremental);
		refs.circle2.DORotate (Vector3.back * 360, 2,RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops (-1,LoopType.Incremental);


		for (int i = 0; i < refs.playerObj.Length; i++) 
		{
			refs.playerObj [i].transform.localPosition = new Vector3 (-1200, refs.playerObj [i].transform.localPosition.y, 0);
		}

		Sequence seq = DOTween.Sequence ();

		for (int i = 0; i < refs.playerObj.Length; i++) 
		{
			seq.Insert (0.2f * (i + 1), refs.playerObj [i].transform.DOLocalMoveX (posX, 0.3f).SetEase (Ease.OutSine));
		}

		seq.Play ();
	}

	public void StopAnimation()
	{
		refs.circle1.DOKill ();
		refs.circle2.DOKill ();
	}

}
