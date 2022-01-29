using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLamp : MonoBehaviour
{
    public float floatSpeed;
    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3( _player.lampPos.position.x, _player.lampPos.position.y - .1f, _player.lampPos.position.z), floatSpeed * Time.deltaTime);
        transform.rotation = _player.lampPos.rotation;
    }
}
