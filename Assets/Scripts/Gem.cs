using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IPickUp
{
    public AudioClip pickUpSound;

    public void PickUp()
    {
        FindObjectOfType<AudioManager>().PlayOneShot(pickUpSound, transform.position);
    }
}