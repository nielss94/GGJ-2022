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
                    PlayOneShot(StepType.SAND);
                }
            }
            else
            {
                if (Time.time > lastStep + walkInterval)
                {
                    PlayOneShot(StepType.SAND);
                }
            }
        }
    }

    private void PlayOneShot(StepType type)
    {
        lastStep = Time.time;
        switch (type)
        {
            case StepType.SAND:
                _audioManager.PlayOneShot(sandSteps[Random.Range(0, sandSteps.Count)], transform.position, 1);
                break;
            case StepType.STONE:
                _audioManager.PlayOneShot(stoneSteps[Random.Range(0, stoneSteps.Count)], transform.position, 1);
                break;
        }
    }
}
