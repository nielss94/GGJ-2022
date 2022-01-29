using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour, IPickUp
{
    public List<GemSlot> gemSlots = new List<GemSlot>();

    public DoorGem doorGem;
    
    public void PlaceGems(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var gemSlot = GetFirstOpenSlot();

            if (gemSlot)
            {
                gemSlot.Fill();
                Instantiate(doorGem, gemSlot.transform.position, Quaternion.identity);
            }
            else
            {
                OpenDoor();
                return;
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
