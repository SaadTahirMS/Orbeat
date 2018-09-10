using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundController : Singleton<SoundController> {

    public AudioSource audioSource;
    public AudioSource sfxAudioSource;

    public AudioClip perfectHitSound;
    public AudioClip hurdleHitSound;
    public AudioClip playerBlastSound;

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
