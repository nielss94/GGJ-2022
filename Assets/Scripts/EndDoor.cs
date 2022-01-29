using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour, IPickUp
{
    public List<GemSlot> gemSlots = new List<GemSlot>();
    public AudioClip placeGemSound;
    public DoorGem doorGem;
    private AudioManager _audioManager;

    private bool doorOpened = false;
    
    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void PlaceGems(int amount)
    {
        if (!doorOpened)
        {
            StartCoroutine(PlaceGemsWithInterval(amount, 1));
        }
    }

    IEnumerator PlaceGemsWithInterval(int amount, float interval)
    {
        for (int i = 0; i < amount; i++)
        {
            var gemSlot = GetFirstOpenSlot();

            if (gemSlot)
            {
                yield return new WaitForSeconds(interval);
                gemSlot.Fill();
                Instantiate(doorGem, gemSlot.transform.position, Quaternion.identity);
                _audioManager.PlayOneShot(placeGemSound, gemSlot.transform.position, 1, 1f);
            }
            else
            {
                OpenDoor();
            }
        }
        
        
        if (!GetFirstOpenSlot())
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        doorOpened = true;
        Debug.Log("Door is opening");
    }

    GemSlot GetFirstOpenSlot()
    {
        foreach (var gemSlot in gemSlots)
        {
            if (!gemSlot.filled)
            {
                return gemSlot;
            }
        }

        return null;
    }
}
