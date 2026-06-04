using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private GameSoundDataSO soundDataSO;
    [SerializeField] private string buttonClickSound;
    [SerializeField] private string gameOverSound;
    [SerializeField] private string backGroundMusicHomeSound;
    [SerializeField] private string backGroundMusicGamePlaySound;
    [SerializeField] private AudioSource gameAudioSource;
    [SerializeField] private AudioSource backgroundMusicAudioSource;

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

    public void PlayGameOverSound()
    {
        PlaySound(gameOverSound, gameAudioSource);
    }

    public void PlayBackgroundMusicHome()
    {
        PlaySound(backGroundMusicHomeSound, backgroundMusicAudioSource);
    }

    public void PlayBackgroundMusicGamePlay()
    {
        PlaySound(backGroundMusicGamePlaySound, backgroundMusicAudioSource);
    }

    public void PlayButtonClickSound()
    {
        PlaySound(buttonClickSound, gameAudioSource);
    }
}
