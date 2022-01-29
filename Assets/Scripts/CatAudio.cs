using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatAudio : MonoBehaviour
{
    public List<AudioClip> chaseStartedSounds = new List<AudioClip>();
    
    private CatAggro _catAggro;
    private AudioManager _audioManager;

    private bool chasing = false;
    public AudioClip jumpScare;    
    [Header("Chase options")]
    public List<AudioClip> chasingSounds = new List<AudioClip>();

    public float chaseCooldownMin;
    public float chaseCooldownMax;
    private float chaseCooldown;
    private float lastChaseSound;
    
    [Header("Breath options")]
    public List<AudioClip> breathSounds = new List<AudioClip>();
    public float breathCooldownMin;
    public float breathCooldownMax;
    private float breathCooldown;
    private float lastBreathSound;
    
    
    private void Awake()
    {
        _catAggro = GetComponent<CatAggro>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        _catAggro.onChaseStarted += ChaseStarted;
        _catAggro.onChaseEnded += ChaseEnded;
        
        chaseCooldown = Random.Range(chaseCooldownMin, chaseCooldownMax);
        breathCooldown = Random.Range(breathCooldownMin, breathCooldownMax);
    }

    private void Update()
    {
        if (chasing)
        {
            var player = FindObjectOfType<Player>();
            if (Vector3.Distance(player.transform.position, transform.position) < 1 && !player.dead)
            {
                Kill(player);
                chasing = false;
            }
            if (Time.time > lastChaseSound + chaseCooldown)
            {
                _audioManager.PlayOneShot(chasingSounds[Random.Range(0, chasingSounds.Count)], transform.position, 1, 1, transform);
                chaseCooldown = Random.Range(chaseCooldownMin, chaseCooldownMax);
                lastChaseSound = Time.time;
            }
        }
        else
        {
            if (Time.time > lastBreathSound + breathCooldown)
            {
                var player = FindObjectOfType<Player>();
                if (Vector3.Distance(player.transform.position, transform.position) < 1)
                {
                    _audioManager.PlayOneShot(breathSounds[0], transform.position, 1, 1, transform);
                }
                else if (Vector3.Distance(player.transform.position, transform.position) < 2)
                {
                    _audioManager.PlayOneShot(breathSounds[1], transform.position, 1, 1, transform);
                }
                else if (Vector3.Distance(player.transform.position, transform.position) < 3)
                {
                    _audioManager.PlayOneShot(breathSounds[2], transform.position, 1, 1, transform);
                }

                breathCooldown = Random.Range(breathCooldownMin, breathCooldownMax);
                lastBreathSound = Time.deltaTime;
            }
        }
    }

    private void Kill(Player player)
    {
        _audioManager.PlayOneShot(jumpScare, transform.position, 1, 1, transform);
        player.Die();
    }

    private void ChaseStarted()
    {
        _audioManager.PlayOneShot(chaseStartedSounds[Random.Range(0, chaseStartedSounds.Count)], transform.position, 1, 1, transform);
        chasing = true;
    }

    private void ChaseEnded()
    {
        chasing = false;
    }
}
