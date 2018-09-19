using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundController : Singleton<SoundController> {

	#region Variables And Properties

	private bool musicState = true;
	private bool soundState = true;

	public bool MusicState
	{
		get
		{
			return musicState;
		}
		set
		{
			musicState = value;
			SaveState ();
		}
	}

	public bool SoundState
	{
		get
		{
			return soundState;
		}
		set
		{
			soundState = value;
			SaveState ();
		}
	}

    public AudioSource audioSource;
    public AudioSource sfxAudioSource;

    public AudioClip perfectHitSound;
    public AudioClip hurdleHitSound;
    public AudioClip playerBlastSound;

	#endregion Variables And Properties

	#region Initialization

	public void Initialize()
	{
		LoadState ();
	}

	#endregion Initialization

	#region Save/Load State

	public void SaveState()
	{
		DatabaseManager.SetBool (Constants.MusicState, musicState);
		DatabaseManager.SetBool (Constants.SoundState, soundState);
	}

	public void LoadState()
	{
		musicState = DatabaseManager.GetBool (Constants.MusicState, true);
		soundState = DatabaseManager.GetBool (Constants.SoundState, true);
	}

	#endregion Save/Load State

    public void PlaySFXSound(SFX state){
        switch(state){
            case SFX.PlayerBlast:
                sfxAudioSource.PlayOneShot(playerBlastSound);
                break;
        }
    }

    public void SetPitch(float value,bool tween){
        if(tween)
            audioSource.DOPitch(value, Constants.pitchTime);
        else
            audioSource.pitch = value;
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }

}
