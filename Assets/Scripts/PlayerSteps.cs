using System;
using System.Collections;
using System.Collections.Generic;
using ECM.Components;
using ECM.Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

public enum StepType
{
    SAND,
    STONE
}

public class PlayerSteps : MonoBehaviour
{
    public List<AudioClip> sandSteps = new List<AudioClip>();
    public List<AudioClip> stoneSteps = new List<AudioClip>();

    public float walkInterval = .5f;
    public float runInterval = .3f;

    private float lastStep = 0;

    
    public BaseFirstPersonController characterMovement;
    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            //TODO: Get type of floor
            
            if (characterMovement.run)
            {
                if (Time.time > lastStep + runInterval)
                {
                    PlayOneShot(StepType.STONE, .8f);
                    float t = Random.Range(0.2f, 0.3f);
                    StartCoroutine(PlayLamp(t, true));
                }
            }
            else
            {
                if (Time.time > lastStep + walkInterval)
                {
                    PlayOneShot(StepType.STONE, 0.5f);
                    float t = Random.Range(0.25f, 0.45f);
                    StartCoroutine(PlayLamp(t, false));
                }
            }
        }
    }

    private void PlayOneShot(StepType type, float volume = 1)
    {
        lastStep = Time.time;
        switch (type)
        {
            case StepType.SAND:
                _audioManager.PlayOneShot(sandSteps[Random.Range(0, sandSteps.Count)], transform.position, 1, volume);
                break;
            case StepType.STONE:
                _audioManager.PlayOneShot(stoneSteps[Random.Range(0, stoneSteps.Count)], transform.position, 1, volume);
                break;
        }
    }

    private IEnumerator PlayLamp(float t, bool run = false)
    {
        var playerLamp = FindObjectOfType<PlayerLamp>();
        
        yield return new WaitForSeconds(t);
        
        playerLamp.PlayLampSound(run);
    }
}
