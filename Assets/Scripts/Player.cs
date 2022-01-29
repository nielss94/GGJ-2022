using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform lampPos;
    public PlayerLamp playerLamp;

    private void Start()
    {
        Instantiate(playerLamp, lampPos.position, lampPos.rotation);
    }
}
