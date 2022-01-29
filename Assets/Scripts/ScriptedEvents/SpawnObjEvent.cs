using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjEvent : ScriptedEvent
{
    private bool wasActivated = false;
    
    public GameObject spawnObject;
    public Transform spawnPosition;

    protected override void Activate()
    {
        if (!wasActivated)
        {
            if (spawnObject)
            {
                Instantiate(spawnObject, spawnPosition.position, Quaternion.identity);
            }
        }
    }
}
