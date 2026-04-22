using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private GameSoundDataSO soundDataSO;

    public void PlaySound(string soundName, AudioSource audioSource)    
    {
        if (soundDataSO.TryGetSound(soundName, out var soundData))
        {
            audioSource.clip = soundData.soundClip;

            audioSource.volume = soundData.soundVolume;

            if(audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            audioSource.Play();
        }
    }
}
