using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public Transform hover;
    public float pickUpDistance;

    private PlayerInventory _playerInventory;
    private InteractUI _interactUI;
    private void Awake()
    {
        _interactUI = FindObjectOfType<InteractUI>();
        _playerInventory = GetComponent<PlayerInventory>();
    }

    public void Hover(Transform up)
    {
        if (Vector3.Distance(transform.position, up.transform.position) < pickUpDistance)
        {
            if (up.TryGetComponent(out IPickUp pickUp))
            {
                if (!pickUp.CanInteract()) return;

                string interactString = pickUp.GetInteractString();
                string interactImageString = pickUp.GetInteractImageString();
                _interactUI.SetInteractUI(interactImageString, interactString);
            }

            hover = up;
        }
        else
        {
            _interactUI.Disable();
        }
    }

    public void UnHover()
    {
        hover = null;
        _interactUI.Disable();
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
                    _playerInventory.AddToTotalGems(_playerInventory.gems);
                    hover = null;
                    _interactUI.Disable();

                }
            }
            else if (hover.TryGetComponent(out EndDoor endDoor))
            {
                if (Vector3.Distance(transform.position, hover.transform.position) < pickUpDistance)
                {
                    endDoor.PlaceGems(_playerInventory.gems);
                    _playerInventory.gems = 0;
                    hover = null;
                    _interactUI.Disable();
                }
            }
            
        }
    }
}
