using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public Transform hover;
    
    private PlayerInventory _playerInventory;
    private void Awake()
    {
        _playerInventory = GetComponent<PlayerInventory>();
    }

    public void Hover(Transform up)
    {
        hover = up;
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
                gem.PickUp();
                Destroy(gem.gameObject);
                _playerInventory.gems++;
                hover = null;
                
            }
        }
    }
}
