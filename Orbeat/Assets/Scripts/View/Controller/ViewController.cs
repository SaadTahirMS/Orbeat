using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController {

	#region Variables

	private ViewRefs refs;

	private Dictionary<Views,RectTransform> viewDict;
	private Stack<BaseController> viewStack;

	private bool isMainMenuOpen = true;
	private bool restrictBackButton = false;

	private SettingsController settingsController;
	private GamePauseController gamePauseController;
	private MainMenuController mainMenuController;
	private GameQuitController gameQuitController;
	private ConfirmationDialogController confirmationDialogController;
	private RateUsController rateUsController;
	private CharacterSelectionController characterSelectionController;

	#endregion Variables

	#region Initialization

	public ViewController(ViewRefs viewRefs)
	{
		refs = viewRefs;
		Initialize ();
	}

	private void Initialize()
	{
		viewDict = new Dictionary<Views,RectTransform> ();
		viewStack = new Stack<BaseController> ();

		InitializeEvents ();
	}

	private void InitializeEvents()
	{
		EventManager.OnOpenView += OnOpenView;
		EventManager.OnCloseView += OnCloseView;
		EventManager.OnBackButtonPressed += BackButtonPressed;
	}

	#endregion Initialization

	#region Create/Open/Close

	private void OnOpenView(Views id, object viewModel = null)
	{
		RectTransform view;
		if (viewDict.ContainsKey (id))
			view = viewDict [id];
		else {
			view = (Object.Instantiate<RectTransform> (Resources.Load<RectTransform> ("Views/" + id.ToString ())));
			view.gameObject.SetActive (false);
			view.transform.SetParent (refs.viewContainer);
			view.transform.localScale = Vector3.one;
			view.offsetMin = view.offsetMax = Vector2.zero ;
			viewDict.Add (id, view);
		}
		view.SetAsLastSibling ();
		BaseController baseController = GetController (id);
		baseController.Open (view.gameObject, viewModel);
		viewStack.Push (baseController);
	}

	private BaseController GetController(Views id)
	{
		switch (id) {
		case Views.Settings:
			if (settingsController == null)
				settingsController = new SettingsController ();
			return settingsController;
		case Views.Pause:
			if (gamePauseController == null)
				gamePauseController = new GamePauseController ();
			return gamePauseController;
		case Views.MainMenu:
			if (mainMenuController == null)
				mainMenuController = new MainMenuController ();
			return mainMenuController;
		case Views.ConfirmationDialog:
			if (confirmationDialogController == null)
				confirmationDialogController = new ConfirmationDialogController ();
			return confirmationDialogController;
		case Views.RateUs:
			if (rateUsController == null)
				rateUsController = new RateUsController ();
			return rateUsController;
		case Views.CharacterSelection:
			if (characterSelectionController == null)
				characterSelectionController = new CharacterSelectionController ();
			return characterSelectionController;
		default:
			if (mainMenuController == null)
				mainMenuController = new MainMenuController ();
			return mainMenuController;
		}
	}

	private void OnCloseView()
	{
		if (viewStack.Count != 0) {
			restrictBackButton = false;
			BaseController baseController = viewStack.Pop ();
			baseController.Close ();
		}
	}

	public void CloseAllViews()
	{
		isMainMenuOpen = false;
		while (viewStack.Count != 0) {
			OnCloseView ();
		}
	}

	public void OpenView(Views id)
	{
		isMainMenuOpen = true;
		OnOpenView (id);
	}

	private void BackButtonPressed()
	{
		if (!restrictBackButton) {
			if (viewStack.Count == 1 && isMainMenuOpen) {
				ShowGameQuitView ();
			} else if (viewStack.Count == 0 && !isMainMenuOpen) {
				EventManager.DoFirePauseGameEvent ();
			} else {
				OnCloseView ();
			}
		}
	}

	#endregion Create/Open/Close

	#region Rate Us View

	#endregion Rate Us View

	#region Game Quit View

	private void ShowGameQuitView()
	{
		if (gameQuitController == null)
			gameQuitController = new GameQuitController ();

		gameQuitController.ShowGameQuit ();
	}

	#endregion Game Quit View

}