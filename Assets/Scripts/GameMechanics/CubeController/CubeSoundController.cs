using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string cubeFallSoundName;
    [SerializeField] private string cubeInstantiateSoundName;
    [SerializeField] private string cubeCollisionSoundName;

    public void PlayCubeFallSound()
    {
        GameController.Instance.SoundController.PlaySound(cubeFallSoundName, audioSource);
    }

    public void PlayCubeInstantiateSound()
    {
        GameController.Instance.SoundController.PlaySound(cubeInstantiateSoundName, audioSource);
    }

    public void PlayCubeCollisionSound()
    {
        GameController.Instance.SoundController.PlaySound(cubeCollisionSoundName, audioSource);
    }
}
