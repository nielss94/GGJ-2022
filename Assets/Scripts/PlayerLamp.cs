using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerLamp : MonoBehaviour
{
    public float floatSpeed;
    public List<AudioClip> lampChainSounds = new List<AudioClip>();


    private int curIndex = 0;
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

    public void PlayLampSound()
    {
        if (lampChainSounds.Count <= 0) return;

        if (r.NextDouble() < .9f)
        {
            _audioManager.PlayOneShot(lampChainSounds[curIndex], transform.position, 1, .8f);
            
        } 

        curIndex++;

        if (curIndex > lampChainSounds.Count - 1)
        {
            curIndex = 0;
        }
    }
}
