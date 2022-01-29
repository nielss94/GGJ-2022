using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour, IPickUp
{
    public List<GemSlot> gemSlots = new List<GemSlot>();

    public DoorGem doorGem;
    
    public void PlaceGems(int amount)
    {
        StartCoroutine(PlaceGemsWithInterval(amount, 1));
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
