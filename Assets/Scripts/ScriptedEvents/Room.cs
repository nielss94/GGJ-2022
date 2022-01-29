using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomEvent
{
    ENTER,
    LEAVE
}

public class Room : MonoBehaviour
{
    public List<TriggerEvent> triggers = new List<TriggerEvent>();

    public RoomEvent eventType;
    [SerializeField] private bool inside;

    [Header("Move Object")] 
    public Transform moveObject;

    public float moveSpeed;
    public Vector3 moveAmount;
    private Vector3 moveDestination;

    private bool active;
    private bool wasActivated;

    [Header("Spawn Object")] 
    public GameObject spawnObject;
    public Transform spawnPosition;
    
    private void Awake()
    {
        foreach (var trigger in triggers)
        {
            trigger.onTrigger += ToggleInside;
        }
    }

    private void OnDestroy()
    {
        foreach (var trigger in triggers)
        {
            trigger.onTrigger -= ToggleInside;
        }
    }

    private void Update()
    {
        if (active)
        {
            if (moveObject)
            {
                moveObject.position = Vector3.Lerp(moveObject.position,moveDestination, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(moveObject.position, moveDestination) < .1f)
                {
                    active = false;
                }
            }
        }
    }

    private void ToggleInside()
    {
        inside = !inside;

        if (wasActivated) return;

        if (inside && eventType == RoomEvent.ENTER || !inside && eventType == RoomEvent.LEAVE)
        {
            wasActivated = true;
            active = true;

            if (moveObject)
            {
                moveDestination = new Vector3(moveObject.position.x + moveAmount.x, moveObject.position.y + moveAmount.y,
                    moveObject.position.z + moveAmount.z);
            }

            if (spawnObject)
            {
                Instantiate(spawnObject, spawnPosition.position, Quaternion.identity);
            }
        }
    }

}
