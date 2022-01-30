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

    public AudioClip onMoveAudio;
    public float moveSpeed;
    public Vector3 moveAmount;
    private Vector3 moveDestination;
    private Vector3 origin;
    private bool active;
    private bool wasActivated;
    public bool returnToOriginAfterSeconds = false;
    public float returnAfterSeconds = 2f;
    private bool moveBackToOrigin = false;
    [Header("Spawn Object")] 
    public GameObject spawnObject;
    public AudioClip onSpawnAudio;
    public Transform spawnPosition;

    private AudioManager _audioManager;
    
    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();

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
                    if (returnToOriginAfterSeconds)
                    {
                        StartCoroutine(ReturnAfterSeconds(returnAfterSeconds));
                    }
                }
            }
        }

        if (moveBackToOrigin)
        {
            moveObject.position = Vector3.Lerp(moveObject.position,origin, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(moveObject.position, origin) < .1f)
            {
                moveBackToOrigin = false;
            }
        }
    }

    private IEnumerator ReturnAfterSeconds(float t)
    {
        yield return new WaitForSeconds(t);
        moveBackToOrigin = true;
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
                _audioManager.PlayOneShot(onMoveAudio, moveObject.transform.position, 1);
                moveDestination = new Vector3(moveObject.position.x + moveAmount.x, moveObject.position.y + moveAmount.y,
                    moveObject.position.z + moveAmount.z);
                origin = new Vector3(moveObject.position.x, moveObject.position.y,
                    moveObject.position.z);
            }

            if (spawnObject)
            {
                Instantiate(spawnObject, spawnPosition.position, Quaternion.identity);
                _audioManager.PlayOneShot(onSpawnAudio, spawnObject.transform.position, 1);

            }
        }
    }

}
