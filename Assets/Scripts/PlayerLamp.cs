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
    }

    public void PlayLampSound(bool run)
    {
        if (lampChainSounds.Count <= 0) return;

        if (run)
        {
            if (r.NextDouble() < .7f)
            {
                _audioManager.PlayOneShot(lampChainSounds[curChainIndex], transform.position, 1, .8f);
            }
        }
        
        
        if (r.NextDouble() < .6f)
        {
            _audioManager.PlayOneShot(lampSqueekSounds[curSqueekIndex], transform.position, 1, .8f);
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
