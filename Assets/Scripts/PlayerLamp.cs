using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerLamp : MonoBehaviour
{
    public float floatSpeed;
    public List<AudioClip> lampChainSounds = new List<AudioClip>();
    public List<AudioClip> lampSqueekSounds = new List<AudioClip>();

    public Animator lampAnimator;
    public Light light;
    public GameObject smallCone;
    public GameObject bigCone;
    private int curChainIndex = 0;
    private int curSqueekIndex = 0;
    private Player _player;
    private AudioManager _audioManager;
    private Random r = new Random();
    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3( _player.lampPos.position.x, _player.lampPos.position.y - .1f, _player.lampPos.position.z), floatSpeed * Time.deltaTime);
        transform.rotation = _player.lampPos.rotation;

        bool lifting = Input.GetButton("Lift") || Input.GetAxisRaw("Lift") != 0; 
        lampAnimator.SetBool("Lift", lifting);
        if (lifting)
        {
            light.range = 20;
            light.intensity = 50;
            bigCone.SetActive(true);
            smallCone.SetActive(false);
        }
        else
        {
            bigCone.SetActive(false);
            smallCone.SetActive(true);
            light.intensity = 25;
            light.range = 10;
        }
    }

    public void PlayLampSound(bool run)
    {
        if (lampChainSounds.Count <= 0) return;

        if (run)
        {
            if (r.NextDouble() < .7f)
            {
                _audioManager.PlayOneShot(lampChainSounds[curChainIndex], transform.position, 1, .5f);
            }
        }
        
        
        if (r.NextDouble() < .6f)
        {
            _audioManager.PlayOneShot(lampSqueekSounds[curSqueekIndex], transform.position, 1, .4f);
        }
        
        curChainIndex++;
        curSqueekIndex++;

        if (curChainIndex > lampChainSounds.Count - 1)
        {
            curChainIndex = 0;
        }
        
        if (curSqueekIndex > lampSqueekSounds.Count - 1)
        {
            curSqueekIndex = 0;
        }
    }
}
