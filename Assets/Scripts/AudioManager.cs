using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class AudioManager : MonoBehaviour
{
    public AudioClipPlayer audioClipPlayer = null;

    public AudioSource environmentAudio;
    public AudioClip firstDrone;
    public AudioClip secondDrone;
    public AudioClip thirdDrone;
    public void PlayOneShot(AudioClip clip, Vector3 pos = default, float spacial = 0, float volume = 1, Transform parent = null)
    {
        AudioClipPlayer newAudioClipPlayer = Instantiate(audioClipPlayer, pos, Quaternion.identity);
        newAudioClipPlayer.audioSource.spatialBlend = spacial;
        newAudioClipPlayer.audioSource.volume = volume;
        newAudioClipPlayer.transform.SetParent(parent);
        newAudioClipPlayer.PlayOneShot(clip);
    }

    public void CheckForMusicChange(int totalGems)
    {
        if (totalGems >= 5 && environmentAudio.clip != thirdDrone)
        {
            environmentAudio.clip = thirdDrone;
        }
        else if (totalGems >= 2 && environmentAudio.clip != secondDrone)
        {
            environmentAudio.clip = secondDrone;
        }
        else if (environmentAudio.clip != firstDrone)
        {
            environmentAudio.clip = firstDrone;
        }
    }
}