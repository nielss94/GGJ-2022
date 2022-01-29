using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Gem : MonoBehaviour, IPickUp
{
    public AudioClip pickUpSound;
    public ParticleSystem particleSystem;
    public void PickUp()
    {
        FindObjectOfType<AudioManager>().PlayOneShot(pickUpSound, transform.position);
        StartCoroutine(RemoveParticles());
    }

    private IEnumerator RemoveParticles()
    {
        var emission = particleSystem.emission; // Stores the module in a local variable
        emission.enabled = false; 
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}