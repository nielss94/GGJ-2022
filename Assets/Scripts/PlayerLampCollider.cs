using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLampCollider : MonoBehaviour
{
    private PlayerPickUp _playerPickUp;

    private void Awake()
    {
        _playerPickUp = FindObjectOfType<PlayerPickUp>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other && other.TryGetComponent(out IPickUp pickup))
        {
            _playerPickUp.Hover(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other && other.TryGetComponent(out IPickUp pickup))
        {
            _playerPickUp.UnHover();
        }
    }
}
