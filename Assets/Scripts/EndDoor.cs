using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EndDoor : MonoBehaviour, IPickUp
{
    public List<GemSlot> gemSlots = new List<GemSlot>();
    public AudioClip placeGemSound;
    public DoorGem doorGem;
    private AudioManager _audioManager;

    public AudioClip openDoorClip;
    public Collider doorCollider;
    public Transform doorL;
    public Transform doorR;
    private bool doorOpened = false;
    private Sequence _sequence;
    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _sequence = DOTween.Sequence();
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
                var newGem = Instantiate(doorGem, gemSlot.transform.position, gemSlot.transform.rotation);
                newGem.transform.SetParent(gemSlot.transform);
                _audioManager.PlayOneShot(placeGemSound, gemSlot.transform.position, 1, 1f);
                var newGemChild = newGem.transform.GetChild(0);
                _sequence.Join(newGemChild.DOMove(
                    new Vector3(newGemChild.position.x + 0.04f, newGemChild.position.y, newGemChild.position.z), .6f));
                _sequence.Join(newGemChild.DOLocalRotate(
                    new Vector3(0, 0, 45), 1f, RotateMode.LocalAxisAdd));
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
        DOTween.KillAll();
        Destroy(doorCollider);
        _audioManager.PlayOneShot(openDoorClip, transform.position, 1, 1);
        doorL.DOMove(new Vector3(doorL.position.x, doorL.position.y, doorL.position.z + 1.3f), 1f);
        doorR.DOMove(new Vector3(doorR.position.x, doorR.position.y, doorR.position.z - 1.3f), 1f).OnComplete(() =>
        {
            FindObjectOfType<DarthFader>().FadeOutAndLoadCutScene();
        });
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

    public string GetInteractImageString()
    {
        var amountOfControllers = Input.GetJoystickNames().Length;

        return amountOfControllers > 0 ? "A" : "F";
    }

    public bool CanInteract()
    {
        return FindObjectOfType<PlayerInventory>().gems > 0;
    }

    public Sprite InteractSprite { get; set; }

    public string GetInteractString()
    {
        return "to place gems";
    }
}
