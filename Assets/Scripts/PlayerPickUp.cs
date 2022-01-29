using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public Transform hover;
    public float pickUpDistance;

    private PlayerInventory _playerInventory;
    private void Awake()
    {
        _playerInventory = GetComponent<PlayerInventory>();
    }

    public void Hover(Transform up)
    {
        if (Vector3.Distance(transform.position, up.transform.position) < pickUpDistance)
        {
            hover = up;
        }
    }

    public void UnHover()
    {
        hover = null;
    }

    private void Update()
    {
        if (Input.GetButtonDown("PickUp") && hover)
        {
            if (hover.TryGetComponent(out Gem gem))
            {
                if (Vector3.Distance(transform.position, hover.transform.position) < pickUpDistance)
                {
                    gem.PickUp();
                    Destroy(gem.gameObject);
                    _playerInventory.gems++;
                    hover = null;
                }
            }
            else if (hover.TryGetComponent(out EndDoor endDoor))
            {
                if (Vector3.Distance(transform.position, hover.transform.position) < pickUpDistance)
                {
                    endDoor.PlaceGems(_playerInventory.gems);
                    _playerInventory.gems = 0;
                    hover = null;
                }
            }
            
        }
    }
}
