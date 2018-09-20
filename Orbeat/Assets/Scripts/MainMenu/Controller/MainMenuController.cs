using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class MainMenuController : BaseController {

	#region Variables

	private MainMenuViewController mainMenuViewController;

	private WaitForSeconds popUpCrWait;
	private float popUpCrDelay = 2;

	#endregion Variables

	#region Life Cycle Methods

	public MainMenuController()
	{
		popUpCrWait = new WaitForSeconds (popUpCrDelay);
		mainMenuViewController = new MainMenuViewController ();
	}

	public void Open(GameObject obj, object viewModel = null)
	{
		InitializeEvents ();
		mainMenuViewController.Open (obj);
		SetNoAdsButton ();
		ShowLeaderBoard ();
	}

	public void Close()
	{
		UnInitializeEvents ();
		mainMenuViewController.Close ();
	}

	private void InitializeEvents()
	{
		EventManager.OnCharacterClicked += CharacterClicked;
		EventManager.OnPlayerProfileChanged += PlayerProfileChanged;
		ThirdPartyEventManager.OnPurchaseSuccessfull += OnPurchaseSuccessfull;
		ThirdPartyEventManager.OnPurchaseFail += OnPurchaseFail;
	}

	private void UnInitializeEvents()
	{
		EventManager.OnCharacterClicked -= CharacterClicked;
		EventManager.OnPlayerProfileChanged -= PlayerProfileChanged;
		ThirdPartyEventManager.OnPurchaseSuccessfull -= OnPurchaseSuccessfull;
		ThirdPartyEventManager.OnPurchaseFail -= OnPurchaseFail;
	}

	#endregion Life Cycle Methods

	#region Event CallBacks

	private void OnPurchaseFail(string id,PurchaseFailureReason failureReason)
	{
		mainMenuViewController.ShowWarning (WarningType.InAppPurchaseFailed);
	}

	private void OnPurchaseSuccessfull(string id)
	{
		if (string.Compare (id, ThirdPartyConstants.NonConsumableIApps [0]) == 0) {
			PlayerData.NoAdsPurchased ();
			SetNoAdsButton ();
		}
	}

	private void PlayerProfileChanged()
	{
		int playerIndex = LeaderBoardController.Instance.PlayerIndex;
		mainMenuViewController.SetLeaderBoardStrip (playerIndex, PlayerData.PlayerModel);
	}

	private void CharacterClicked(int index)
	{
		int playerIndex = LeaderBoardController.Instance.PlayerIndex;
		if (index != playerIndex)
			return;
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		EventManager.DoFireOpenViewEvent (Views.CharacterSelection);
	}

	#endregion Event CallBacks

	#region Texts Update

	#endregion Texts Update

	#region No Ads Button

	private void SetNoAdsButton()
	{
		mainMenuViewController.SetNoAdsButton (!PlayerData.IsNoAdsPurchased);
	}

	#endregion No Ads Button

	#region LeaderBoard

	private void ShowLeaderBoard()
	{
		if (PlayerData.IsScoreChanged) {
			PlayerData.IsScoreChanged = false;
			List<CharacterModel> charactersList = LeaderBoardController.Instance.GetPlayersToShowList (5);
			for (int i = 0; i < charactersList.Count; i++) {
				mainMenuViewController.SetLeaderBoardStrip (i, charactersList [charactersList.Count - i - 1]);
			}
			GameStateController.Instance.StartCoroutine (PopPlayerStripe ());
		}
	}

	private IEnumerator PopPlayerStripe()
	{
		yield return popUpCrWait;
		SoundController.Instance.PlaySFXSound (SFX.ButtonClick);
		mainMenuViewController.PlayPlayerPopAnim (LeaderBoardController.Instance.PlayerIndex);
	}

	#endregion LeaderBoard


}
