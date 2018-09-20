using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : BaseController {

	#region Variables

	private SettingsViewController settingsViewController;

	private bool isMusicOn = true;
	private bool isSoundOn = true;
	private bool isNotificationsOn = true;

	#endregion Variables

	#region Life Cycle Methods

	public SettingsController()
	{
		LoadState ();
		settingsViewController = new SettingsViewController ();
	}

	public void Open(GameObject obj, object viewModel = null)
	{
		InitializeEvents ();
		settingsViewController.Open (obj);
		UpdateMusicView ();
		UpdateSoundView ();
		UpdateNotificationsView ();
	}

	public void Close()
	{
		UnInitializeEvents ();
		settingsViewController.Close ();
	}

	private void InitializeEvents()
	{
		EventManager.OnChangeMusic += OnChangeMusic;
		EventManager.OnChangeSound += OnChangeSound;
		EventManager.OnChangeNotificaitons += OnChangeNotificaitons;
	}

	private void UnInitializeEvents()
	{
		EventManager.OnChangeMusic -= OnChangeMusic;
		EventManager.OnChangeSound -= OnChangeSound;
		EventManager.OnChangeNotificaitons -= OnChangeNotificaitons;
	}

	#endregion Life Cycle Methods

	#region Save/Load State

	public void LoadState()
	{
		isSoundOn = SoundController.Instance.SoundState;
		isMusicOn = SoundController.Instance.MusicState;
		isNotificationsOn = PlayerData.IsNotificationsOn;
	}

	public void SaveNotification()
	{
		PlayerData.IsNotificationsOn = isNotificationsOn;
	}

    public void SaveMusic()
    {
        SoundController.Instance.MusicState = isMusicOn;
    }

    public void SaveSound()
    {
        SoundController.Instance.SoundState = isSoundOn;
    }

	#endregion Save/Load State

	#region Event Call Back

	private void OnChangeMusic()
	{
		isMusicOn = !isMusicOn;
		UpdateMusicView ();
        SaveMusic ();
	}

	private void OnChangeSound()
	{
		isSoundOn = !isSoundOn;
		UpdateSoundView ();
        SaveSound ();
	}

	private void OnChangeNotificaitons()
	{
		isNotificationsOn = !isNotificationsOn;
		UpdateNotificationsView ();
        SaveNotification ();
	}

	#endregion Event Call Back

	#region View Handling

	private void UpdateMusicView()
	{
		settingsViewController.UpdateMusicView (isMusicOn);
	}

	private void UpdateSoundView()
	{
		settingsViewController.UpdateSoundView (isSoundOn);
	}

	private void UpdateNotificationsView()
	{
		settingsViewController.UpdateNotificationsView (isNotificationsOn);
	}

	#endregion View Handling
}
