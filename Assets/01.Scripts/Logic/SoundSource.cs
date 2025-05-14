using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource _audioSource;
    
    public void Play(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance)
    {
        CancelInvoke();
        _audioSource.clip = clip;
        _audioSource.volume = soundEffectVolume;
        _audioSource.Play();
        _audioSource.pitch = 1f + soundEffectPitchVariance;
    }
}
