using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverSound : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    public AudioClip hoverSound;
    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Button>().Select();
    }

    public void OnSelect(BaseEventData eventData)
    {
        _audioManager.PlayOneShot(hoverSound, Vector3.zero, 0, .2f);
    }
}
