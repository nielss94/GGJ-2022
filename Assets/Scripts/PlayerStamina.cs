using System;
using System.Collections;
using System.Collections.Generic;
using ECM.Components;
using ECM.Controllers;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public int maxStamina = 100;
    public float stamina = 0;

    public int staminaPerSecond;
    public int regenStaminaAfterSeconds;
    public int regenStaminaPerSeconds;
    public StaminaStage currentStaminaStage;
    public List<StaminaStage> staminaStages = new List<StaminaStage>();


    private float lastStopRun = 0;
    private AudioSource _audioSource;
    private BaseFirstPersonController _baseFirstPersonController;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _baseFirstPersonController = GetComponent<BaseFirstPersonController>();
    }

    private void Start()
    {
        staminaStages.Sort((a, b) =>
        {
            if (a.staminaLevel > b.staminaLevel) return 1;
            if (a.staminaLevel < b.staminaLevel) return -1;
            return 0;
        });
    }

    private void Update()
    {

        if (Time.time > lastStopRun + regenStaminaAfterSeconds && !_baseFirstPersonController.run)
        {
            stamina = Mathf.Clamp(stamina -= Time.deltaTime * regenStaminaPerSeconds, 0, maxStamina);
        }

        if (_baseFirstPersonController.run)
        {
            stamina = Mathf.Clamp(stamina += Time.deltaTime * staminaPerSecond, 0, maxStamina);

            for (int i = staminaStages.Count - 1; i >= 0; i--)
            {
                var staminaStage = staminaStages[i];
                if (stamina >= staminaStage.staminaLevel && staminaStage != currentStaminaStage)
                {
                    SetStaminaStage(staminaStage);
                    break;
                }

                if (staminaStage == currentStaminaStage)
                {
                    break;
                }
            }
        }
        
        if (stamina >= 100 || Input.GetButtonUp("Run"))
        {
            lastStopRun = Time.time;
            SetStaminaStage(null);
        }
    }

    private void SetStaminaStage(StaminaStage stage)
    {
        if (stage == null)
        {
            _audioSource.loop = false;
            currentStaminaStage = null;

            return;
        }

        currentStaminaStage = stage;
        _audioSource.Stop();
        _audioSource.clip = stage.audioClip;
        _audioSource.loop = stage.loop;
        _audioSource.Play();
    }
}

[System.Serializable]
public class StaminaStage
{
    [Range(0, 100)] public int staminaLevel;
    public AudioClip audioClip;
    public bool loop;
}
