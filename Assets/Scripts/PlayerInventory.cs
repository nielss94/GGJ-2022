using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int gems = 0;
    public int totalGems;

    public void AddToTotalGems(int playerInventoryGems)
    {
        totalGems += playerInventoryGems;

        FindObjectOfType<AudioManager>().CheckForMusicChange(totalGems);
    }

}
