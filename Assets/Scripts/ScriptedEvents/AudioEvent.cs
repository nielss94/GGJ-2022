using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvent : ScriptedEvent
{
    public AudioClip audioClip;
    public Transform position;

    protected override void Activate()
    {
        if (audioClip)
        {
            FindObjectOfType<AudioManager>().PlayOneShot(audioClip, position ? position.position : Vector3.zero, position ? 1 : 0, 1);
        }
    }
}
