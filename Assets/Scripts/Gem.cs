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
        FindObjectOfType<GemUI>().AddGem();
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

    public Sprite InteractSprite { get; set; }

    public string GetInteractString()
    {
        return "to pick up gem";
    }

    public string GetInteractImageString()
    {
        var amountOfControllers = Input.GetJoystickNames().Length;

        return amountOfControllers > 0 ? "A" : "F";
    }

    public bool CanInteract()
    {
        return true;
    }
}