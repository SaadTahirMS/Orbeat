using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundController : Singleton<SoundController> {

    public AudioSource mainMenuAudioSource;
    public AudioSource gameplayAudioSource;
    public AudioSource sfxAudioSource;

    public AudioClip perfectHitSound;
    public AudioClip hurdleHitSound;
    public AudioClip playerBlastSound;
    
    public void SetMainMenuMusic(bool value){
        if(value){
            mainMenuAudioSource.Play();
        }
        else{
            mainMenuAudioSource.Stop();
        }
    }

    public void SetGamePlayMusic(bool value){
        if (value)
        {
            gameplayAudioSource.Play();
        }
        else
        {
            gameplayAudioSource.Stop();
        }
    }

    public void PlaySFXSound(SFX state){
        switch(state){
            case SFX.TargetHit:
                sfxAudioSource.PlayOneShot(hurdleHitSound);
                break;
            case SFX.PerfectHit:
                sfxAudioSource.PlayOneShot(perfectHitSound);
                break;
            case SFX.PlayerBlast:
                sfxAudioSource.PlayOneShot(playerBlastSound);
                break;
        }
    }

}
