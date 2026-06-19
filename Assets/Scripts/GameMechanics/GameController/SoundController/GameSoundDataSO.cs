using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSound", menuName = "GameSoundAssets")]
public class GameSoundDataSO : ScriptableObject
{
    [SerializeField] private List<SoundData> cubeSounds;

    [SerializeField] private List<SoundData> gameFeelSounds;

    private Dictionary<string, SoundData> soundDataDictionary = new Dictionary<string, SoundData>();



    private void OnEnable()
    {
        soundDataDictionary.Clear();

        InitializeDictionary(soundDataDictionary, cubeSounds);

        InitializeDictionary(soundDataDictionary, gameFeelSounds);
    }

    private void InitializeDictionary(Dictionary<string, SoundData> _soundDataType, List<SoundData> _soundDataList)
    {
        foreach (var sound in _soundDataList)
        {
            if (!_soundDataType.ContainsKey(sound.soundName))
            {
                _soundDataType.Add(sound.soundName, sound);
            }
        }
    }

    public bool TryGetSound(string soundName, out SoundData soundData)
    {
        return soundDataDictionary.TryGetValue(soundName, out soundData);
    }


    [Serializable]
    public class SoundData
    {
        public string soundName;
        public AudioClip soundClip;
        [Range(0,1)] public float soundVolume;
    }
}
