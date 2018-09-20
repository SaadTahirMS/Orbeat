using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager {

	#region Views

	public delegate void OpenView(Views id,object viewModel = null);
	public static OpenView OnOpenView;

	public delegate void CloseView();
	public static CloseView OnCloseView;

	public delegate void CloseAllViews();
	public static CloseAllViews OnCloseAllViews;

	public delegate void BackButtonPressed();
	public static BackButtonPressed OnBackButtonPressed;

	public static void DoFireOpenViewEvent(Views id,object viewModel = null)
	{
		if (OnOpenView != null)
			OnOpenView (id, viewModel);
	}

	public static void DoFireCloseViewEvent()
	{
		if (OnCloseView != null)
			OnCloseView ();
	}

	public static void DoFireBackButtonEvent()
	{
		if (OnBackButtonPressed != null)
			OnBackButtonPressed ();
	}

	public static void DoFireCloseAllViewsEvent()
	{
		if (OnCloseAllViews != null)
			OnCloseAllViews ();
	}

	#endregion Views

	#region Settings

	public delegate void ChangeSound();
	public static ChangeSound OnChangeSound;

	public delegate void ChangeMusic();
	public static ChangeMusic OnChangeMusic;

	public delegate void ChangeNotificaitons();
	public static ChangeNotificaitons OnChangeNotificaitons;

	public static void DoFireChangeSoundEvent()
	{
		if (OnChangeSound != null)
			OnChangeSound ();
	}

	public static void DoFireChangeMusicEvent()
	{
		if (OnChangeMusic != null)
			OnChangeMusic ();
	}

	public static void DoFireChangeNotificaitonsEvent()
	{
		if (OnChangeNotificaitons != null)
			OnChangeNotificaitons ();
	}

	#endregion Settings

	#region Character Selection Events

	public delegate void PlayerProfileChanged();
	public static PlayerProfileChanged OnPlayerProfileChanged;

	public delegate void DoneClicked();
	public static DoneClicked OnDoneClicked;

	public delegate void NameChanged();
	public static NameChanged OnNameChanged;

	public delegate void CharacterChanged(int characterNumber);
	public static CharacterChanged OnCharacterChanged;

	public static void DoFireNameChangedEvent()
	{
		if (OnNameChanged != null)
			OnNameChanged ();
	}

	public static void DoFireCharacterChangedEvent(int characterNumber)
	{
		if (OnCharacterChanged != null)
			OnCharacterChanged (characterNumber);
	}

	public static void DoFireDoneClickedEvent()
	{
		if (OnDoneClicked != null)
			OnDoneClicked ();
	}

	public static void DoFirePlayerProfileChangedEvent()
	{
		if (OnPlayerProfileChanged != null)
			OnPlayerProfileChanged ();
	}

	#endregion Character Selection Events

	#region Game Play Events

	public delegate void StartGame();
	public static StartGame OnStartGame;

	public delegate void PauseGame();
	public static PauseGame OnPauseGame;

	public delegate void ResumeGame();
	public static ResumeGame OnResumeGame;

	public delegate void GameOver();
	public static GameOver OnGameOver;

	public static void DoFireStartGameEvent()
	{
		if (OnStartGame != null)
			OnStartGame ();
	}

	public static void DoFirePauseGameEvent()
	{
		if (OnPauseGame != null)
			OnPauseGame ();
	}

	public static void DoFireResumeGameEvent()
	{
		if (OnResumeGame != null)
			OnResumeGame ();
	}

	public static void DoFireGameOverEvent()
	{
		if (OnGameOver != null)
			OnGameOver ();	
	}
		
	#endregion Game Play Events

	#region Main Menu

	public delegate void CharacterClicked(int index);
	public static CharacterClicked OnCharacterClicked;

	public static void DoFireCharacterClickedEvent(int index)
	{
		if (OnCharacterClicked != null)
			OnCharacterClicked (index);	
	}

	#endregion Main Menu

	#region Confirmation Dialog

	public delegate void NoPressed();
	public static NoPressed OnNoPressed;

	public delegate void YesPressed();
	public static YesPressed OnYesPressed;

	public static void DoFireNoPressedEvent()
	{
		if (OnNoPressed != null)
			OnNoPressed ();	
	}

	public static void DoFireYesPressedEvent()
	{
		if (OnYesPressed != null)
			OnYesPressed ();	
	}

	#endregion Confirmation Dialog

	#region Progression Bar

	public delegate void ScoreUpdated();
	public static ScoreUpdated OnScoreUpdated;

	public delegate void UpdateFillBarColor(Color32 barColor, Color32 fillerColor);
	public static UpdateFillBarColor OnUpdateFillBarColor;

	public static void DoFireScoreUpdatedEvent()
	{
		if (OnScoreUpdated != null)
			OnScoreUpdated ();	
	}

	public static void DoFireUpdateFillBarColorEvent(Color32 barColor, Color32 fillerColor)
	{
		if (OnUpdateFillBarColor != null)
			OnUpdateFillBarColor (barColor, fillerColor);	
	}

	#endregion Progression Bar

	#region UnAssignEvents

	public static void UnAssignAllEvents()
	{
		OnOpenView = null;
		OnCloseView = null;
		OnBackButtonPressed = null;
		OnChangeSound = null;
		OnChangeMusic = null;
		OnChangeNotificaitons = null;
		OnStartGame = null;
		OnPauseGame = null;
		OnResumeGame = null;
		OnGameOver = null;
	}

	#endregion UnAssignEvents

}
