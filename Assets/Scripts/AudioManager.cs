using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class AudioManager : MonoBehaviour
{
    public AudioClipPlayer audioClipPlayer = null;

    public void PlayOneShot(AudioClip clip, Vector3 pos = default, float spacial = 0)
    {
        AudioClipPlayer newAudioClipPlayer = Instantiate(audioClipPlayer, pos, Quaternion.identity);
        newAudioClipPlayer.audioSource.spatialBlend = spacial;
        newAudioClipPlayer.PlayOneShot(clip);
    }
}