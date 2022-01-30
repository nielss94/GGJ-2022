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

    public Transform doorCollider;
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
        Destroy(doorCollider.gameObject);
        doorL.DOMove(new Vector3(doorL.position.x, doorL.position.y, doorL.position.z + 1.3f), 1f);
        doorR.DOMove(new Vector3(doorR.position.x, doorR.position.y, doorR.position.z - 1.3f), 1f).OnComplete(() =>
        {
            Debug.Log("Start cutscene");
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
}
